using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class CutSolver {
        public static CutSolution Cut(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            if (navigator.isEqual) {
                return new CutSolution(new PlainShape(allocator), new PlainShape(allocator), SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                return new CutSolution(new PlainShape(allocator), new PlainShape(allocator), SubtractSolution.Nature.notOverlap);
            }

            var restPathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);

            filterNavigator.Reset();
            var bitePathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);

            var nature = restPathList.layouts.Length > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new CutSolution(restPathList, bitePathList, nature);
        }
    }

}