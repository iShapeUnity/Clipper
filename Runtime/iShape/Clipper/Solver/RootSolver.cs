using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Shape;
using iShape.Clipper.Util;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class RootSolver {
        
        public static SubtractSolution Intersect(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            if (navigator.isEqual) {
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.notOverlap);
            }

            var pathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);

            navigator.Dispose();

            var nature = pathList.layouts.Length > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new SubtractSolution(pathList, nature);
        }
        
        public static SubtractSolution Subtract(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            if (navigator.isEqual) {
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.notOverlap);
            }

            var pathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);

            navigator.Dispose();

            var nature = pathList.layouts.Length > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new SubtractSolution(pathList, nature);
        }
        
                public static UnionSolution Union(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.out_in);

            if (navigator.isEqual) {
                return new UnionSolution(new PlainShape(master, true, allocator), UnionSolution.Nature.overlap);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.outside, PinPoint.PinType.in_out, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                if (navigator.hasContacts) {
                    if (master.IsOverlap(slave)) {
                        return new UnionSolution(new PlainShape(master, true, allocator), UnionSolution.Nature.masterIncludeSlave);
                    } else if (slave.IsOverlap(slave)) {
                        return new UnionSolution(new PlainShape(slave, true, allocator), UnionSolution.Nature.slaveIncludeMaster);
                    } else {
                        return new UnionSolution(new PlainShape(allocator), UnionSolution.Nature.notOverlap);
                    }
                } else {
                    if (master.IsContain(slave.Any())) {
                        return new UnionSolution(new PlainShape(master, true, allocator), UnionSolution.Nature.masterIncludeSlave);
                    } else if (slave.IsContain(master.Any())) {
                        return new UnionSolution(new PlainShape(slave, true, allocator), UnionSolution.Nature.slaveIncludeMaster);
                    } else {
                        return new UnionSolution(new PlainShape(allocator), UnionSolution.Nature.notOverlap);
                    }
                }
            }

            var pathList = UnionSolver.Union(filterNavigator, master, slave, allocator);
            
            navigator.Dispose();

            var nature = pathList.layouts.Length > 0 ? UnionSolution.Nature.overlap : UnionSolution.Nature.notOverlap;

            return new UnionSolution(pathList, nature);
        }
        
    }

}