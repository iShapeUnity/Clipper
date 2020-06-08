using iShape.Clipper.Util;
using iShape.Collections;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class ComplexSubtractSolver {
        private const Allocator tempAllocator = Allocator.Temp;

        public static ComplexSubtractSolution ComplexSubtract(this PlainShape self, NativeArray<IntVector> path, Allocator allocator) {
            var hull = self.Get(0, tempAllocator);

            var solution = hull.Cut(path, tempAllocator);
            hull.Dispose();
            
            ComplexSubtractSolution complexSubtractSolution;

            switch (solution.nature) {
                case Solution.Nature.notOverlap:
                    complexSubtractSolution = new ComplexSubtractSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
                    break;
                case Solution.Nature.equal:
                case Solution.Nature.slaveIncludeMaster:
                    complexSubtractSolution = new ComplexSubtractSolution(new PlainShapeList(allocator), new PlainShapeList(self, allocator), true);
                    break;
                case Solution.Nature.masterIncludeSlave:
                    complexSubtractSolution = self.HoleCase(path, allocator);
                    break;
                case Solution.Nature.overlap:
                    complexSubtractSolution = self.OverlapCase(solution, allocator);
                    break;
                default:
                    complexSubtractSolution = new ComplexSubtractSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
                    break;
            }

            solution.Dispose();

            // impossible case
            return complexSubtractSolution;
        }

        // case where hole is completely inside
        private static ComplexSubtractSolution HoleCase(this PlainShape self, NativeArray<IntVector> cutPath, Allocator allocator) {
            int n = self.layouts.Length;
            if (n == 1) {
                // original shape does not have holes 
                var shape = new DynamicPlainShape(self, allocator);
                shape.Add(cutPath, false);

                var mainList = shape.ToShapeList(allocator);
                var partList = new PlainShapeList(cutPath.Reverse(), true, allocator);
                
                return new ComplexSubtractSolution(mainList, partList, true);
            } else if (self.HoleCaseMainList(cutPath, out var mainList, allocator)) {
                var partList = self.HoleCaseBiteList(cutPath, allocator);
                return new ComplexSubtractSolution(mainList, partList, true);
            }
            
            return new ComplexSubtractSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
        }

        private static ComplexSubtractSolution OverlapCase(this PlainShape self, DualSolution cutHullSolution, Allocator allocator) {
            var mainList = self.OverlapCaseMainList(cutHullSolution.mainPathList, allocator);
            var partList = self.OverlapCaseBiteList(cutHullSolution.partPathList, allocator);

            return new ComplexSubtractSolution(mainList, partList, true);
        }

        private static PlainShapeList OverlapCaseMainList(this PlainShape self, PlainShape restPathList, Allocator allocator) {
            int n = self.layouts.Length;
            if (n == 1) {
                int partsCount = restPathList.Count;
                var points = new NativeArray<IntVector>(restPathList.points, allocator);
                var layouts = new NativeArray<PathLayout>(partsCount, allocator);
                var segments = new NativeArray<Segment>(partsCount, allocator);
                for (int j = 0; j < partsCount; ++j) {
                    int length = restPathList.layouts[j].length;
                    layouts[j] = new PathLayout(0, length, true);
                    segments[j] = new Segment(j, 1);
                }

                return new PlainShapeList(points, layouts, segments);
            }

            var shapePaths = new DynamicPlainShape(restPathList, tempAllocator);
            var result = new DynamicPlainShapeList(restPathList.points.Length, 2 * restPathList.layouts.Length, shapePaths.layouts.Count, allocator);

            var holes = new NativeArray<int>(n - 1, tempAllocator);

            int i = 1;
            while (i < n) {
                holes[i - 1] = i++;
            }

            i = 0;
            nextIsland:
            while (i < shapePaths.layouts.Count) {
                var island = shapePaths.Get(i, tempAllocator);
                var usedHoles = new DynamicArray<int>(tempAllocator);
                for (int j = 0; j < holes.Length; ++j) {
                    int holeIndex = holes[j];
                    var hole = self.Get(holeIndex, tempAllocator);
                    var subtract = island.Subtract(hole, tempAllocator);
                    switch (subtract.nature) {
                        case Solution.Nature.equal:
                        case Solution.Nature.slaveIncludeMaster:
                            // island is equal to hole
                            shapePaths.RemoveAt(i);

                            // goto nextIsland
                            usedHoles.Dispose();
                            hole.Dispose();
                            subtract.Dispose();
                            island.Dispose();
                            goto nextIsland;
                            
                        case Solution.Nature.notOverlap:
                            hole.Dispose();
                            subtract.Dispose();
                            continue;
                        case Solution.Nature.overlap:
                            int islandsCount = 0;
                            for (int k = 0; k < subtract.pathList.layouts.Length; ++k) {
                                if (subtract.pathList.layouts[k].isClockWise) {
                                    islandsCount += 1;
                                }
                            }

                            if (islandsCount == 1) {
                                island.Dispose();
                                island = subtract.pathList.Get(0, tempAllocator);
                            } else {
                                shapePaths.RemoveAt(i);
                                for (int k = 0; k < subtract.pathList.layouts.Length; ++k) {
                                    if (subtract.pathList.layouts[k].isClockWise) {
                                        var newIsland = subtract.pathList.Get(i);
                                        shapePaths.Add(newIsland, true);
                                    }
                                }

                                // goto nextIsland
                                usedHoles.Dispose();
                                hole.Dispose();
                                subtract.Dispose();
                                island.Dispose();
                                goto nextIsland;
                            }

                            break;
                        case Solution.Nature.masterIncludeSlave:
                            usedHoles.Add(j);
                            break;
                    }
                    
                    hole.Dispose();
                    subtract.Dispose();
                }
                
                var islandShape = island.ToDynamicShape(true, tempAllocator);

                if (usedHoles.Count > 0) {
                    for (int k = 0; k < usedHoles.Count; ++k) {
                        int index = usedHoles[k];
                        int holeIndex = holes[index];
                        var hole = self.Get(holeIndex);
                        islandShape.Add(hole, false);
                    }
                }
                usedHoles.Dispose();
                
                
                result.Add(islandShape);
                
                islandShape.Dispose();

                i += 1;
            }
            
            holes.Dispose();
            
            shapePaths.Dispose();

            return result.Convert();
        }

        private static PlainShapeList OverlapCaseBiteList(this PlainShape self, PlainShape partPathList, Allocator allocator) {
            var subPaths = new DynamicPlainShape(partPathList.points.Length, partPathList.layouts.Length, allocator);
            for (int i = 0; i < partPathList.layouts.Length; ++i) {
                var subPath = partPathList.Get(i, tempAllocator).Reverse();
                subPaths.Add(subPath, true);
                subPath.Dispose();
            }
            
            var result = self.BiteList(ref subPaths, allocator);
            subPaths.Dispose();
            
            return result;
        }

        private static bool HoleCaseMainList(this PlainShape self, NativeArray<IntVector> cutPath, out PlainShapeList plainShapeList, Allocator allocator) {
            int n = self.layouts.Length;

            // new hole
            var rootHole = new NativeArray<IntVector>(cutPath, tempAllocator).Reverse();

            // holes which are not intersected with new one
            var notInteractedHoles = new DynamicArray<int>(tempAllocator);
            var interactedHoles = new DynamicArray<int>(tempAllocator);

            // the pieces which are inside of new hole
            var islands = new DynamicPlainShape(tempAllocator);

            int i = 1;
            // let union new hole with the others
            while (i < n) {
                var nextHole = self.Get(i, tempAllocator).Reverse();

                var union = rootHole.Union(nextHole, tempAllocator);

                switch (union.nature) {
                    case Solution.Nature.notOverlap:
                        notInteractedHoles.Add(i);
                        nextHole.Dispose();
                        break;
                    case Solution.Nature.equal:
                    case Solution.Nature.masterIncludeSlave:
                        interactedHoles.Add(i);
                        nextHole.Dispose();
                        break;
                    case Solution.Nature.slaveIncludeMaster:
                        // new hole is almost inside from one of the hole
                        
                        rootHole.Dispose();
                        nextHole.Dispose();
                        union.Dispose();
                        islands.Dispose();
                        notInteractedHoles.Dispose();
                        interactedHoles.Dispose();
                        
                        plainShapeList = new PlainShapeList(allocator);
                        return false;
                    case Solution.Nature.overlap:
                        interactedHoles.Add(i);
                        var uShape = union.pathList;
                        for (int j = 0; j < uShape.layouts.Length; ++j) {
                            if (uShape.layouts[j].isClockWise) {
                                rootHole.Dispose();
                                rootHole = uShape.Get(j, tempAllocator);
                            } else {
                                var island = uShape.Get(j, tempAllocator).Reverse();
                                islands.Add(island, true);
                                island.Dispose();
                            }
                        }

                        nextHole.Dispose();
                        break;
                }

                union.Dispose();
                ++i;
            }

            if (notInteractedHoles.Count > 0) {
                // some of this holes can be in new hole
                i = notInteractedHoles.Count - 1;
                while (i >= 0) {
                    int index = notInteractedHoles[i];
                    var nextHole = self.Get(index, tempAllocator);
                    if (rootHole.IsContain(nextHole, false)) {
                        notInteractedHoles.RemoveAt(i);
                        interactedHoles.Add(index);
                    }

                    nextHole.Dispose();

                    i -= 1;
                }
            }

            if (islands.layouts.Count == 0) {
                var mainShape = self.Get(0, allocator).ToDynamicShape(true, allocator);
                mainShape.Add(rootHole.Reverse(), false);
                for (int j = 0; j < notInteractedHoles.Count; ++j) {
                    int index = notInteractedHoles[j];
                    var hole = self.Get(index, tempAllocator);
                    mainShape.Add(hole, false);                    
                }
                
                // return
                rootHole.Dispose();
                islands.Dispose();
                notInteractedHoles.Dispose();
                interactedHoles.Dispose();

                plainShapeList = mainShape.ToShapeList(allocator); 
                return true;
            }

            // subtract from inside pieces the holes which are inside or touch the new hole
            var shapeParts = new DynamicPlainShapeList(islands.points.Count, 2 * islands.layouts.Count, islands.layouts.Count, allocator);

            i = 0;
            nextIsland:
            do {
                // must be direct in a clockwise direction
                var island = islands.Get(i, tempAllocator);
                var islandHoles = new DynamicArray<int>(tempAllocator);
                for (int j = 0; j < interactedHoles.Count; ++j) {
                    int index = interactedHoles[j];
                    var hole = self.Get(index, tempAllocator);

                    var subtract = island.Subtract(hole, tempAllocator);
                    hole.Dispose();
                    
                    switch (subtract.nature) {
                        case Solution.Nature.equal:
                        case Solution.Nature.slaveIncludeMaster:
                            // island is equal to hole
                            i += 1;
                            
                            // goto
                            island.Dispose();
                            islandHoles.Dispose();
                            subtract.Dispose();
                            
                            goto nextIsland;
                        case Solution.Nature.notOverlap:
                            // not touche
                            // goto
                            subtract.Dispose();
                            continue;
                        case Solution.Nature.overlap:
                            island.Dispose();
                            island = subtract.pathList.Get(0, tempAllocator);
                            if (subtract.pathList.layouts.Length > 1) {
                                for (int k = 1; k < subtract.pathList.layouts.Length; ++k) {
                                    var part = subtract.pathList.Get(k);
                                    islands.Add(part, true);
                                }
                            }

                            break;
                        case Solution.Nature.masterIncludeSlave:
                            islandHoles.Add(index);
                            break;
                    }
                    
                    subtract.Dispose();
                    
                }

                var islandShape = island.ToDynamicShape(true, tempAllocator);
                if (islandHoles.Count > 0) {
                    for (int k = 0; k < islandHoles.Count; ++k) {
                        int index = islandHoles[k];
                        var hole = self.Get(index);
                        islandShape.Add(hole, false);
                    }
                }
                
                islandHoles.Dispose();

                shapeParts.Add(islandShape);
                
                islandShape.Dispose();

                i += 1;
            } while (i < islands.layouts.Count);

            
            islands.Dispose();
            interactedHoles.Dispose();

            var rootShape = self.Get(0, tempAllocator).ToDynamicShape(true, tempAllocator);
            rootHole.Reverse();
            rootShape.Add(rootHole, false);
            
            rootHole.Dispose();

            if (notInteractedHoles.Count > 0) {
                for (int j = 0; j < notInteractedHoles.Count; ++j) {
                    int index = notInteractedHoles[j];
                    var hole = self.Get(index);
                    rootShape.Add(hole, false);
                }
            }

            notInteractedHoles.Dispose();

            shapeParts.Add(rootShape);
            
            rootShape.Dispose();

            plainShapeList = shapeParts.Convert();

            return true;
        }

        private static PlainShapeList HoleCaseBiteList(this PlainShape self, NativeArray<IntVector> cutPath, Allocator allocator) {
            var subPaths = new DynamicPlainShape(cutPath.Reverse(), true, tempAllocator);
            var result = self.BiteList(ref subPaths, allocator);
            subPaths.Dispose();
            
            return result;
        }

        private static PlainShapeList BiteList(this PlainShape self, ref DynamicPlainShape subPaths, Allocator allocator) {
            // bit from new hole all shape's holes
            int n = self.layouts.Length;

            var holes = new DynamicPlainShape(tempAllocator);

            for (int i = 1; i < n; ++i) {
                var nextHole = self.Get(i, tempAllocator);

                var j = 0;
                while (j < subPaths.layouts.Count) {
                    var bitPath = subPaths.Get(j, tempAllocator);
                    var subtract = bitPath.Subtract(nextHole, tempAllocator);
                    bitPath.Dispose();
                    
                    switch (subtract.nature) {
                        case Solution.Nature.notOverlap:
                            j += 1;
                            break;
                        case Solution.Nature.overlap:
                            var newSubPathCount = 0;
                            for (int k = 0; k < subtract.pathList.layouts.Length; ++k) {
                                if (subtract.pathList.layouts[k].isClockWise) {
                                    var subPath = subtract.pathList.Get(k);
                                    if (newSubPathCount == 0) {
                                        subPaths.ReplaceAt(j, subPath);
                                    } else {
                                        subPaths.Add(subPath, true);
                                    }

                                    newSubPathCount += 1;
                                } else {
                                    var holePath = subtract.pathList.Get(k);
                                    holes.Add(holePath, false);
                                }
                            }

                            if (newSubPathCount > 1) {
                                j += newSubPathCount;
                            } else {
                                j += 1;
                            }

                            break;
                        case Solution.Nature.equal:
                        case Solution.Nature.slaveIncludeMaster:
                            subPaths.RemoveAt(j);
                            break;
                        case Solution.Nature.masterIncludeSlave:
                            holes.Add(nextHole, false);
                            j += 1;
                            break;
                    }
                    
                    subtract.Dispose();
                }

                nextHole.Dispose();
            }

            if (subPaths.layouts.Count == 0) {
                holes.Dispose();
                return new PlainShapeList(allocator);
            }

            var partList = new DynamicPlainShapeList(allocator);
            
            if (holes.layouts.Count == 0) {
                holes.Dispose();
                for (int i = 0; i < subPaths.layouts.Count; ++i) {
                    partList.Add(subPaths.Get(i));
                }
                return partList.Convert();
            }

            for (int i = 0; i < subPaths.layouts.Count; ++i) {
                var subPath = subPaths.Get(i, allocator);
                var subShape = new DynamicPlainShape(subPath, true, allocator);
                for (int j = 0; j < holes.layouts.Count; ++j) {
                    var hole = holes.Get(j, allocator);
                    if (subPath.IsContain(hole, false)) {
                        subShape.Add(hole, false);
                    }
                }

                partList.Add(subShape.Convert());
            }
            
            holes.Dispose();

            return partList.Convert();
        }
    }

}