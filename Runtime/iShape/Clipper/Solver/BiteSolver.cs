using iShape.Clipper.Util;
using iShape.Collections;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class CutSolver {
        public static BiteSolution Cut(this PlainShape self, NativeArray<IntVector> path, IntGeom iGeom,
            Allocator allocator) {
            var hull = self.Get(0, Allocator.Temp);

            var cutHullSolution = hull.Cut(path, iGeom, allocator);

            switch (cutHullSolution.nature) {
                case SubtractSolution.Nature.notOverlap:
                    return new BiteSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
                case SubtractSolution.Nature.empty:
                    return new BiteSolution(new PlainShapeList(allocator), new PlainShapeList(self, allocator), true);
                case SubtractSolution.Nature.hole:
                    return self.HoleCase(path, iGeom, allocator);
                case SubtractSolution.Nature.overlap:
                    return self.OverlapCase(cutHullSolution, iGeom, allocator);
            }
            
            // impossible case
            return new BiteSolution(new PlainShapeList(allocator), new PlainShapeList(allocator), false);
        }

        // если дыра находится внутри полигона
        private static BiteSolution HoleCase(this PlainShape self, NativeArray<IntVector> cutPath, IntGeom iGeom,
            Allocator allocator) {
            int n = self.layouts.Length;
            PlainShapeList biteList;
            if (n == 1) {
                // у исходного полигона нету других дыр
                var shape = new DynamicPlainShape(self, allocator);
                shape.Add(cutPath, false);
                biteList = new PlainShapeList(new PlainShape(cutPath, true, allocator), allocator, true);
                return new BiteSolution(new PlainShapeList(shape, allocator, true), biteList, true);
            } else {
                var mainList = self.HoleCaseMainList(cutPath, iGeom, allocator);
                biteList = self.HoleCaseBiteList(cutPath, iGeom, allocator);

                return new BiteSolution(mainList, biteList, true);
            }
        }

        private static BiteSolution OverlapCase(this PlainShape self, CutSolution cutHullSolution, IntGeom iGeom,
            Allocator allocator) {
            var mainList = self.OverlapCaseMainList(cutHullSolution.restPathList, iGeom, allocator);
            var biteList = self.OverlapCaseBiteList(cutHullSolution.bitePathList, iGeom, allocator);

            return new BiteSolution(mainList, biteList, true);
        }

        private static PlainShapeList OverlapCaseMainList(this PlainShape self, PlainShape restPathList, IntGeom iGeom,
            Allocator allocator) {
            int n = self.layouts.Length;
            if (n == 1) {
                return new PlainShapeList(restPathList, allocator);
            }

            var shapePaths = new DynamicPlainShape(restPathList, allocator);
            var result = new DynamicPlainShapeList(shapePaths.points.Count, 2 * shapePaths.layouts.Count,
                shapePaths.layouts.Count, allocator);

            var holes = new NativeArray<int>(n - 1, Allocator.Temp);

            int i = 0;
            while (i < n) {
                holes[i - 1] = i++;
            }

            i = 0;
            nextIsland:
            while (i < shapePaths.layouts.Count) {
                var island = shapePaths.Get(i, allocator);
                var usedHoles = new DynamicArray<int>();
                for (int j = 0; j < holes.Length; ++j) {
                    int holeIndex = holes[j];
                    var hole = self.Get(holeIndex, allocator);
                    var diff = island.Subtract(hole, iGeom, allocator);
                    switch (diff.nature) {
                        case SubtractSolution.Nature.empty:
                            // остров совпал с дыркой
                            shapePaths.RemoveAt(i);
                            goto nextIsland;
                        case SubtractSolution.Nature.notOverlap:
                            continue;
                        case SubtractSolution.Nature.overlap:
                            int islandsCount = 0;
                            for (int k = 0; k < diff.pathList.layouts.Length; ++k) {
                                if (diff.pathList.layouts[k].isClockWise) {
                                    islandsCount += 1;
                                }
                            }

                            if (islandsCount == 1) {
                                island = diff.pathList.Get(0, allocator);
                            } else {
                                shapePaths.RemoveAt(i);
                                for (int k = 0; k < diff.pathList.layouts.Length; ++k) {
                                    if (diff.pathList.layouts[k].isClockWise) {
                                        var newIsland = diff.pathList.Get(i, allocator);
                                        shapePaths.Add(newIsland, true);
                                    }
                                }

                                goto nextIsland;
                            }

                            break;
                        case SubtractSolution.Nature.hole:
                            usedHoles.Add(j);
                            break;
                    }
                }

                var islandShape = new DynamicPlainShape(island, true, allocator, true);

                if (usedHoles.Count > 0) {
                    for (int k = 0; k < usedHoles.Count; ++k) {
                        int index = usedHoles[k];
                        int holeIndex = holes[index];
                        var hole = self.Get(holeIndex, allocator);
                        islandShape.Add(hole, true);
                    }
                }

                result.Add(islandShape.Convert());

                i += 1;
            }

            return result.Convert();
        }

        private static PlainShapeList OverlapCaseBiteList(this PlainShape self, PlainShape bitePathList, IntGeom iGeom, Allocator allocator) {
            var subPaths = new DynamicPlainShape(bitePathList.points.Length, bitePathList.layouts.Length, allocator);
            for (int i = 0; i < bitePathList.layouts.Length; ++i) {
                var subPath = bitePathList.Get(i, allocator).Reverse();
                subPaths.Add(subPath, true);
            }

            return self.BiteList(ref subPaths, iGeom, allocator);
        }

        private static PlainShapeList HoleCaseMainList(this PlainShape self, NativeArray<IntVector> cutPath, IntGeom iGeom, Allocator allocator) {
            int n = self.layouts.Length;

            // new hole
            var rootHole = cutPath;
            rootHole.Reverse();

            // holes which are not intersected with new one
            var notInteractedHoles = new DynamicArray<int>(Allocator.Temp);
            var iteractedHoles = new DynamicArray<int>(Allocator.Temp);

            // the pieces which are inside of new hole
            var islands = new DynamicPlainShape(Allocator.Temp);

            int i = 1;
            // let union new hole with the others
            while (i < n) {
                var nextHole = self.Get(i, allocator).Reverse();

                var unionSolution = rootHole.Union(nextHole, iGeom, allocator);

                switch (unionSolution.nature) {
                    case UnionSolution.Nature.notOverlap:
                        notInteractedHoles.Add(i);
                        break;
                    case UnionSolution.Nature.masterIncludeSlave:
                        iteractedHoles.Add(i);
                        break;
                    case UnionSolution.Nature.overlap:
                    case UnionSolution.Nature.slaveIncludeMaster:
                        iteractedHoles.Add(i);
                        var uShape = unionSolution.pathList;
                        for (int j = 0; j < uShape.layouts.Length; ++j) {
                            if (uShape.layouts[j].isClockWise) {
                                rootHole = uShape.Get(j, allocator);
                            } else {
                                var island = uShape.Get(j, allocator).Reverse();
                                islands.Add(island, true);
                            }
                        }

                        break;
                }

                ++i;
            }

            if (notInteractedHoles.Count > 0) {
                // some of this holes can be in new hole
                i = notInteractedHoles.Count - 1;
                while (i >= 0) {
                    int index = notInteractedHoles[i];
                    var nextHole = self.Get(index, allocator);
                    if (rootHole.IsContain(nextHole, false)) {
                        notInteractedHoles.RemoveAt(i);
                        iteractedHoles.Add(index);
                    }

                    i -= 1;
                }
            }

            if (islands.layouts.Count == 0) {
                var mainShape = new DynamicPlainShape(self.Get(0, allocator), true, allocator);
                mainShape.Add(rootHole, true);
                i = 0;
                while (i < notInteractedHoles.Count) {
                    int index = notInteractedHoles[i];
                    var hole = self.Get(index, allocator);
                    mainShape.Add(hole, true);
                }

                return new PlainShapeList(mainShape.Convert(), allocator);
            }

            // subtract from inside pieces the holes which are inside or touch the new hole
            var shapeParts = new DynamicPlainShapeList(islands.points.Count, 2 * islands.layouts.Count, islands.layouts.Count, allocator);

            i = 0;
            nextIsland:
            do {
                // must be direct in a clockwise direction
                var island = islands.Get(i, allocator);
                var islandHoles = new DynamicArray<int>(Allocator.Temp);
                for (int j = 0; j < iteractedHoles.Count; ++j) {
                    int index = iteractedHoles[j];
                    var hole = self.Get(index, allocator);

                    var diffSolution = island.Subtract(hole, iGeom, allocator);
                    switch (diffSolution.nature) {
                        case SubtractSolution.Nature.empty:
                            // island is equal to hole
                            i += 1;
                            goto nextIsland;
                        case SubtractSolution.Nature.notOverlap:
                            // not touche
                            goto endHoles;
                        case SubtractSolution.Nature.overlap:
                            island = diffSolution.pathList.Get(0, allocator);
                            if (diffSolution.pathList.layouts.Length > 1) {
                                for (int k = 1; k < diffSolution.pathList.layouts.Length; ++k) {
                                    var part = diffSolution.pathList.Get(k, allocator);
                                    islands.Add(part, true);
                                }
                            }

                            break;
                        case SubtractSolution.Nature.hole:
                            islandHoles.Add(index);
                            break;
                    }
                }

                endHoles:
                var islandShape = new DynamicPlainShape(island, true, allocator);
                if (islandHoles.Count > 0) {
                    for (int k = 0; k < islandHoles.Count; ++k) {
                        int index = islandHoles[k];
                        var hole = self.Get(index, allocator);
                        islandShape.Add(hole, false);
                    }
                }

                shapeParts.Add(islandShape);

                i += 1;
            } while (i < islands.layouts.Count);

            var rootShape = new DynamicPlainShape(self.Get(0, allocator), true, allocator);
            rootHole.Reverse();
            rootShape.Add(rootHole, false);

            if (notInteractedHoles.Count > 0) {
                for (int j = 0; j < notInteractedHoles.Count; ++j) {
                    int index = notInteractedHoles[j];
                    var hole = self.Get(index, allocator);
                    rootShape.Add(hole, true);
                }
            }

            shapeParts.Add(rootShape);

            return shapeParts.Convert();
        }

        private static PlainShapeList HoleCaseBiteList(this PlainShape self, NativeArray<IntVector> cutPath, IntGeom iGeom, Allocator allocator) {
            var subPaths = new DynamicPlainShape(cutPath.Reverse(), true, allocator);
            return self.BiteList(ref subPaths, iGeom, allocator);
        }

        private static PlainShapeList BiteList(this PlainShape self, ref DynamicPlainShape subPaths, IntGeom iGeom, Allocator allocator) {
            // откусим от новой дыры все имеющиеся дыры полигона

            int n = self.layouts.Length;

            var holes = new DynamicPlainShape(allocator);

            for (int i = 1; i < n; ++i) {
                var nextHole = self.Get(i, allocator);

                var j = 0;
                while (j < subPaths.layouts.Count) {
                    var bitPath = subPaths.Get(j, allocator);
                    var solution = bitPath.Subtract(nextHole, iGeom, allocator);
                    switch (solution.nature) {
                        case SubtractSolution.Nature.notOverlap:
                            j += 1;
                            break;
                        case SubtractSolution.Nature.overlap:
                            var newSubPathCount = 0;
                            for (int k = 0; k < solution.pathList.layouts.Length; ++k) {
                                if (solution.pathList.layouts[k].isClockWise) {
                                    var subPath = solution.pathList.Get(k, allocator);
                                    if (newSubPathCount == 0) {
                                        subPaths.ReplaceAt(j, subPath);
                                    } else {
                                        subPaths.Add(subPath, true);
                                    }

                                    newSubPathCount += 1;
                                } else {
                                    var holePath = solution.pathList.Get(k, allocator);
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
                }
            }

            if (subPaths.layouts.Count == 0) {
                return new PlainShapeList(allocator);
            }

            if (holes.layouts.Count == 0) {
                return new PlainShapeList(subPaths, allocator);
            }

            var biteList = new DynamicPlainShapeList(allocator);

            for (int i = 0; i < subPaths.layouts.Count; ++i) {
                var subPath = subPaths.Get(i, allocator);
                var subShape = new DynamicPlainShape(subPath, true, allocator);
                for (int j = 0; j < holes.layouts.Count; ++j) {
                    var hole = holes.Get(j, allocator);
                    if (subPath.IsContain(hole, false)) {
                        subShape.Add(hole, true);
                    }
                }

                biteList.Add(subShape.Convert());
            }

            return biteList.Convert();
        }
    }

}