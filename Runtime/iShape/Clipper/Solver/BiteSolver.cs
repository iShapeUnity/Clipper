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
                    return self.HoleCase(path, iGeom);
                case SubtractSolution.Nature.overlap:
                    return self.OverlapCase(cutHullSolution, iGeom);
            }
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
                var subPath = bitePathList.Get(i, allocator).Reversed();
                subPaths.Add(subPath, true);
            }

            return self.BiteList(subPaths: &subPaths, iGeom);
        }

    }

}