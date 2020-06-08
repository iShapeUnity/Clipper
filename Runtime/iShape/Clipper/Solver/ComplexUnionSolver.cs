using iShape.Collections;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class ComplexUnionSolver {
        private const Allocator tempAllocator = Allocator.Temp;

        public static Solution ComplexUnion(this PlainShape self, NativeArray<IntVector> path, Allocator allocator) {
            var hull = self.Get(0, tempAllocator);

            var solution = hull.Union(path, tempAllocator);
            hull.Dispose();
            
            switch (solution.nature) {
                case Solution.Nature.notOverlap:
                case Solution.Nature.equal:
                case Solution.Nature.slaveIncludeMaster:
                    solution.Dispose();
                    return new Solution(new PlainShape(allocator), solution.nature);
                case Solution.Nature.masterIncludeSlave:
                    solution.Dispose();
                    return self.InnerCase(path, allocator);
                case Solution.Nature.overlap:
                    var result = self.OverlapCase(solution.pathList, path, allocator);
                    solution.Dispose();
                    return result;
                }

            return new Solution(new PlainShape(allocator), Solution.Nature.notOverlap);
        }

        private static Solution InnerCase(this PlainShape self, NativeArray<IntVector> unionPath, Allocator allocator) {
            int n = self.layouts.Length;
            if (n == 1) {
                return new Solution(new PlainShape(allocator), Solution.Nature.masterIncludeSlave);    
            }
            
            var subtractPatch = new NativeArray<IntVector>(unionPath, tempAllocator).Reverse();

            var notInteractedHoles = new DynamicArray<int>(tempAllocator);
            var newShape = new DynamicPlainShape(self.Get(0), true, allocator);
            var isInteract = false;

            for (int i = 1; i < n; ++i) {
                var nextHole = self.Get(i, tempAllocator).Reverse();
                var subtract = nextHole.Subtract(subtractPatch, tempAllocator);
                nextHole.Dispose();

                switch (subtract.nature) {
                    case Solution.Nature.notOverlap:
                        notInteractedHoles.Add(i);
                        subtract.Dispose();
                        break;
                    case Solution.Nature.masterIncludeSlave:
                    case Solution.Nature.equal:
                        notInteractedHoles.Dispose();
                        subtractPatch.Dispose();
                        newShape.Dispose();
                        subtract.Dispose();
                        return new Solution(new PlainShape(allocator), Solution.Nature.notOverlap);
                    case Solution.Nature.slaveIncludeMaster:
                        isInteract = true;
                        subtract.Dispose();
                        break;
                    case Solution.Nature.overlap:
                        isInteract = true;
                        var shape = subtract.pathList;
                        for (int j = 0; j < shape.layouts.Length; ++j) {
                            var hole = shape.Get(j, tempAllocator).Reverse();
                            newShape.Add(hole, false);
                        }
                        subtract.Dispose();
                        break;
                }
            }

            subtractPatch.Dispose();
        
            if (isInteract) {
                for (int i = 0; i < notInteractedHoles.Count; ++i) {
                    int index = notInteractedHoles[i];
                    var nextHole = self.Get(index);
                    newShape.Add(nextHole, false);
                }
                notInteractedHoles.Dispose();
                return new Solution(newShape.Convert(), Solution.Nature.overlap);
            } else {
                notInteractedHoles.Dispose();
                newShape.Dispose();
                return new Solution(new PlainShape(allocator), Solution.Nature.masterIncludeSlave);
            }
        }
        
        private static Solution OverlapCase(this PlainShape self, PlainShape plainShape, NativeArray<IntVector> unionPath, Allocator allocator) {
            int n = self.layouts.Length;
            if (n == 1) {
                return new Solution(new PlainShape(plainShape, allocator), Solution.Nature.overlap);    
            }
            
            var subtractPatch = new NativeArray<IntVector>(unionPath, tempAllocator).Reverse();

            var notInteractedHoles = new DynamicArray<int>(tempAllocator);
            var newShape = new DynamicPlainShape(plainShape, allocator);

            for (int i = 1; i < n; ++i) {
                var nextHole = self.Get(i, tempAllocator).Reverse();
                var subtract = nextHole.Subtract(subtractPatch, tempAllocator);
                nextHole.Dispose();

                switch (subtract.nature) {
                    case Solution.Nature.notOverlap:
                        notInteractedHoles.Add(i);
                        subtract.Dispose();
                        break;
                    case Solution.Nature.masterIncludeSlave:
                    case Solution.Nature.equal:
                        notInteractedHoles.Dispose();
                        subtractPatch.Dispose();
                        newShape.Dispose();
                        subtract.Dispose();
                        return new Solution(new PlainShape(allocator), Solution.Nature.notOverlap);
                    case Solution.Nature.slaveIncludeMaster:
                        subtract.Dispose();
                        break;
                    case Solution.Nature.overlap:
                        var shape = subtract.pathList;
                        for (int j = 0; j < shape.layouts.Length; ++j) {
                            var hole = shape.Get(j, tempAllocator).Reverse();
                            newShape.Add(hole, false);
                        }
                        subtract.Dispose();
                        break;
                }
            }

            subtractPatch.Dispose();
        
            for (int i = 0; i < notInteractedHoles.Count; ++i) {
                int index = notInteractedHoles[i];
                var nextHole = self.Get(index);
                newShape.Add(nextHole, false);
            }
            notInteractedHoles.Dispose();
            return new Solution(newShape.Convert(), Solution.Nature.overlap);
        }
    }

}