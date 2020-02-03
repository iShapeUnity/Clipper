using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class CutSolver {
        public static CutSolution Cut(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            PlainPathList restPathList;
            PlainPathList bitePathList;
            if (navigator.isEqual) {
                restPathList = new PlainPathList(0, allocator);
                bitePathList = new PlainPathList(0, allocator);
                return new CutSolution(restPathList, bitePathList, SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                restPathList = new PlainPathList(0, allocator);
                bitePathList = new PlainPathList(0, allocator);
                return new CutSolution(restPathList, bitePathList, SubtractSolution.Nature.notOverlap);
            }

            restPathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);

            filterNavigator.Reset();
            bitePathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);

            var nature = restPathList.Count > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new CutSolution(restPathList, bitePathList, nature);
        }
    }

}