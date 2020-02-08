using iShape.Clipper.Util;
using iShape.Collections;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class CutSolver {
        private const Allocator tempAllocator = Allocator.TempJob;

        public static BiteSolution Bite(this PlainShape self, NativeArray<IntVector> path, IntGeom iGeom, Allocator allocator) {
            var hull = self.Get(0, Allocator.Temp);

            var cutSolution = hull.Cut(path, iGeom, tempAllocator);
            BiteSolution biteSolution;

            switch (cutSolution.nature) {
                case SubtractSolution.Nature.notOverlap:
                    biteSolution = new BiteSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
                    break;
                case SubtractSolution.Nature.empty:
                    biteSolution = new BiteSolution(new PlainShapeList(allocator), new PlainShapeList(self, allocator), true);
                    break;
                case SubtractSolution.Nature.hole:
                    biteSolution = self.HoleCase(path, iGeom, allocator);
                    break;
                case SubtractSolution.Nature.overlap:
                    biteSolution = self.OverlapCase(cutSolution, iGeom, allocator);
                    break;
                default:
                    biteSolution = new BiteSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
                    break;
            }

            cutSolution.Dispose();

            // impossible case
            return biteSolution;
        }

        // case where hole is completely inside
        private static BiteSolution HoleCase(this PlainShape self, NativeArray<IntVector> cutPath, IntGeom iGeom, Allocator allocator) {
            int n = self.layouts.Length;
            PlainShapeList mainList;
            PlainShapeList biteList;
            if (n == 1) {
                // original shape does not have holes 
                var shape = new DynamicPlainShape(self, allocator);
                shape.Add(cutPath, false);

                mainList = shape.ToShapeList(allocator);
                biteList = new PlainShapeList(cutPath, true, allocator);
            } else {
                mainList = self.HoleCaseMainList(cutPath, iGeom, allocator);
                biteList = self.HoleCaseBiteList(cutPath, iGeom, allocator);
            }

            return new BiteSolution(mainList, biteList, true);
        }

        private static BiteSolution OverlapCase(this PlainShape self, CutSolution cutHullSolution, IntGeom iGeom, Allocator allocator) {
            var mainList = self.OverlapCaseMainList(cutHullSolution.restPathList, iGeom, allocator);
            var biteList = self.OverlapCaseBiteList(cutHullSolution.bitePathList, iGeom, allocator);

            return new BiteSolution(mainList, biteList, true);
        }

        private static PlainShapeList OverlapCaseMainList(this PlainShape self, PlainShape restPathList, IntGeom iGeom, Allocator allocator) {
            int n = self.layouts.Length;
            if (n == 1) {
                return new PlainShapeList(restPathList, allocator);
            }

            var shapePaths = new DynamicPlainShape(restPathList, tempAllocator);
            var result = new DynamicPlainShapeList(restPathList.points.Length, 2 * restPathList.layouts.Length, shapePaths.layouts.Count, allocator);

            var holes = new NativeArray<int>(n - 1, tempAllocator);

            int i = 0;
            while (i < n) {
                holes[i - 1] = i++;
            }

            i = 0;
            nextIsland:
            while (i < shapePaths.layouts.Count) {
                var island = shapePaths.Get(i, tempAllocator);
                var usedHoles = new DynamicArray<int>();
                for (int j = 0; j < holes.Length; ++j) {
                    int holeIndex = holes[j];
                    var hole = self.Get(holeIndex, tempAllocator);
                    var subtractSolution = island.Subtract(hole, iGeom, tempAllocator);
                    switch (subtractSolution.nature) {
                        case SubtractSolution.Nature.empty:
                            // island is equal to hole
                            shapePaths.RemoveAt(i);

                            // goto nextIsland
                            hole.Dispose();
                            subtractSolution.Dispose();
                            island.Dispose();
                            goto nextIsland;
                            
                        case SubtractSolution.Nature.notOverlap:
                            continue;
                        case SubtractSolution.Nature.overlap:
                            int islandsCount = 0;
                            for (int k = 0; k < subtractSolution.pathList.layouts.Length; ++k) {
                                if (subtractSolution.pathList.layouts[k].isClockWise) {
                                    islandsCount += 1;
                                }
                            }

                            if (islandsCount == 1) {
                                island.Dispose();
                                island = subtractSolution.pathList.Get(0, tempAllocator);
                            } else {
                                shapePaths.RemoveAt(i);
                                for (int k = 0; k < subtractSolution.pathList.layouts.Length; ++k) {
                                    if (subtractSolution.pathList.layouts[k].isClockWise) {
                                        var newIsland = subtractSolution.pathList.Get(i);
                                        shapePaths.Add(newIsland, true);
                                    }
                                }

                                // goto nextIsland
                                hole.Dispose();
                                subtractSolution.Dispose();
                                island.Dispose();
                                goto nextIsland;
                            }

                            break;
                        case SubtractSolution.Nature.hole:
                            usedHoles.Add(j);
                            break;
                    }
                    
                    hole.Dispose();
                    subtractSolution.Dispose();
                }
                
                var islandShape = island.ToDynamicShape(true, tempAllocator);

                if (usedHoles.Count > 0) {
                    for (int k = 0; k < usedHoles.Count; ++k) {
                        int index = usedHoles[k];
                        int holeIndex = holes[index];
                        var hole = self.Get(holeIndex, tempAllocator);
                        islandShape.Add(hole, false);
                    }
                }
                usedHoles.Dispose();
                holes.Dispose();
                
                result.Add(islandShape);
                
                islandShape.Dispose();

                i += 1;
            }
            
            shapePaths.Dispose();

            return result.Convert();
        }

        private static PlainShapeList OverlapCaseBiteList(this PlainShape self, PlainShape bitePathList, IntGeom iGeom, Allocator allocator) {
            var subPaths = new DynamicPlainShape(bitePathList.points.Length, bitePathList.layouts.Length, allocator);
            for (int i = 0; i < bitePathList.layouts.Length; ++i) {
                var subPath = bitePathList.Get(i, tempAllocator).Reverse();
                subPaths.Add(subPath, true);
                subPath.Dispose();
            }
            
            var result = self.BiteList(ref subPaths, iGeom, allocator);
            subPaths.Dispose();
            
            return result;
        }

        private static PlainShapeList HoleCaseMainList(this PlainShape self, NativeArray<IntVector> cutPath, IntGeom iGeom, Allocator allocator) {
            int n = self.layouts.Length;

            // new hole
            var rootHole = new NativeArray<IntVector>(cutPath, tempAllocator);
            rootHole.Reverse();

            // holes which are not intersected with new one
            var notInteractedHoles = new DynamicArray<int>(tempAllocator);
            var interactedHoles = new DynamicArray<int>(tempAllocator);

            // the pieces which are inside of new hole
            var islands = new DynamicPlainShape(tempAllocator);

            int i = 1;
            // let union new hole with the others
            while (i < n) {
                var nextHole = self.Get(i, tempAllocator).Reverse();

                var unionSolution = rootHole.Union(nextHole, iGeom, tempAllocator);

                switch (unionSolution.nature) {
                    case UnionSolution.Nature.notOverlap:
                        notInteractedHoles.Add(i);
                        break;
                    case UnionSolution.Nature.masterIncludeSlave:
                        interactedHoles.Add(i);
                        break;
                    case UnionSolution.Nature.overlap:
                    case UnionSolution.Nature.slaveIncludeMaster:
                        interactedHoles.Add(i);
                        var uShape = unionSolution.pathList;
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

                        break;
                }

                unionSolution.Dispose();

                nextHole.Dispose();

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
                mainShape.Add(rootHole, false);
                i = 0;
                while (i < notInteractedHoles.Count) {
                    int index = notInteractedHoles[i];
                    var hole = self.Get(index, tempAllocator);
                    mainShape.Add(hole, false);
                }
                
                // return
                rootHole.Dispose();
                islands.Dispose();
                notInteractedHoles.Dispose();
                interactedHoles.Dispose();

                return mainShape.ToShapeList(allocator);
            }

            // subtract from inside pieces the holes which are inside or touch the new hole
            var shapeParts = new DynamicPlainShapeList(islands.points.Count, 2 * islands.layouts.Count, islands.layouts.Count, allocator);

            i = 0;
            nextIsland:
            do {
                // must be direct in a clockwise direction
                var island = islands.Get(i, tempAllocator);
                var islandHoles = new DynamicArray<int>(Allocator.Temp);
                for (int j = 0; j < interactedHoles.Count; ++j) {
                    int index = interactedHoles[j];
                    var hole = self.Get(index, tempAllocator);

                    var subtractSolution = island.Subtract(hole, iGeom, tempAllocator);
                    hole.Dispose();
                    
                    switch (subtractSolution.nature) {
                        case SubtractSolution.Nature.empty:
                            // island is equal to hole
                            i += 1;
                            
                            // goto
                            island.Dispose();
                            islandHoles.Dispose();
                            subtractSolution.Dispose();
                            
                            goto nextIsland;
                        case SubtractSolution.Nature.notOverlap:
                            // not touche
                            // goto
                            subtractSolution.Dispose();
                            
                            goto endHoles;
                        case SubtractSolution.Nature.overlap:
                            island.Dispose();
                            island = subtractSolution.pathList.Get(0, tempAllocator);
                            if (subtractSolution.pathList.layouts.Length > 1) {
                                for (int k = 1; k < subtractSolution.pathList.layouts.Length; ++k) {
                                    var part = subtractSolution.pathList.Get(k);
                                    islands.Add(part, true);
                                }
                            }

                            break;
                        case SubtractSolution.Nature.hole:
                            islandHoles.Add(index);
                            break;
                    }
                    
                    subtractSolution.Dispose();
                    
                }

                endHoles:
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
                    var hole = self.Get(index, tempAllocator);
                    rootShape.Add(hole, false);
                }
            }

            notInteractedHoles.Dispose();

            shapeParts.Add(rootShape);
            
            rootShape.Dispose();

            return shapeParts.Convert();
        }

        private static PlainShapeList HoleCaseBiteList(this PlainShape self, NativeArray<IntVector> cutPath, IntGeom iGeom, Allocator allocator) {
            var subPaths = new DynamicPlainShape(cutPath.Reverse(), true, tempAllocator);
            var result = self.BiteList(ref subPaths, iGeom, allocator);
            subPaths.Dispose();
            
            return result;
        }

        private static PlainShapeList BiteList(this PlainShape self, ref DynamicPlainShape subPaths, IntGeom iGeom, Allocator allocator) {
            // bit from new hole all shape's holes
            int n = self.layouts.Length;

            var holes = new DynamicPlainShape(tempAllocator);

            for (int i = 1; i < n; ++i) {
                var nextHole = self.Get(i, tempAllocator);

                var j = 0;
                while (j < subPaths.layouts.Count) {
                    var bitPath = subPaths.Get(j, tempAllocator);
                    var subtractSolution = bitPath.Subtract(nextHole, iGeom, tempAllocator);
                    bitPath.Dispose();
                    
                    switch (subtractSolution.nature) {
                        case SubtractSolution.Nature.notOverlap:
                            j += 1;
                            break;
                        case SubtractSolution.Nature.overlap:
                            var newSubPathCount = 0;
                            for (int k = 0; k < subtractSolution.pathList.layouts.Length; ++k) {
                                if (subtractSolution.pathList.layouts[k].isClockWise) {
                                    var subPath = subtractSolution.pathList.Get(k);
                                    if (newSubPathCount == 0) {
                                        subPaths.ReplaceAt(j, subPath);
                                    } else {
                                        subPaths.Add(subPath, true);
                                    }

                                    newSubPathCount += 1;
                                } else {
                                    var holePath = subtractSolution.pathList.Get(k);
                                    holes.Add(holePath, false);
                                }
                            }

                            if (newSubPathCount > 1) {
                                j += newSubPathCount;
                            } else {
                                j += 1;
                            }

                            break;
                        case SubtractSolution.Nature.empty:
                            subPaths.RemoveAt(j);
                            break;
                        case SubtractSolution.Nature.hole:
                            holes.Add(nextHole, false);
                            j += 1;
                            break;
                    }
                    
                    subtractSolution.Dispose();
                }

                nextHole.Dispose();
            }

            if (subPaths.layouts.Count == 0) {
                return new PlainShapeList(allocator);
            }

            if (holes.layouts.Count == 0) {
                holes.Dispose();
                
                return new PlainShapeList(subPaths, allocator);
            }

            var biteList = new DynamicPlainShapeList(allocator);

            for (int i = 0; i < subPaths.layouts.Count; ++i) {
                var subPath = subPaths.Get(i, allocator);
                var subShape = new DynamicPlainShape(subPath, true, allocator);
                for (int j = 0; j < holes.layouts.Count; ++j) {
                    var hole = holes.Get(j, allocator);
                    if (subPath.IsContain(hole, false)) {
                        subShape.Add(hole, false);
                    }
                }

                biteList.Add(subShape.Convert());
            }
            
            holes.Dispose();

            return biteList.Convert();
        }
    }

}