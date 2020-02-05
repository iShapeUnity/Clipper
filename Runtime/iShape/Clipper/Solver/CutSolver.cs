using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class CutSolver {
        
        public static CutSolution Cut(this PlainShape self, NativeArray<IntVector> path, IntGeom iGeom, Allocator allocator) {
            var hull = self.Get(0, Allocator.Temp);

            var cutHullSolution = hull.Cut(path, iGeom, allocator);

            switch (cutHullSolution.nature) {
 
                case .notOverlap:
                return new SolutionResult(false, mainList: .empty, bitList: .empty)
                case .empty:
                return SolutionResult(isInteract: true, mainList: .empty, bitList: PlainShapeList(plainShape: self))
                case .hole:
                return self.holeCase(cutPath: path, iGeom: iGeom)
                case .overlap:
                return self.overlapCase(cutHullSolution: cutHullSolution, iGeom: iGeom)
            }
        }
    }

}