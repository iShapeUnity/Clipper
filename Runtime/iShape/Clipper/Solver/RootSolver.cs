using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Util;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class RootSolver {
        
        public static CutSolution Cut(this NativeArray<IntVector> master, NativeArray<IntVector> slave, IntGeom iGeom, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out, allocator);

            if (navigator.isEqual) {
                navigator.Dispose();
                return new CutSolution(new PlainShape(allocator), new PlainShape(allocator), SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, allocator);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                filterNavigator.Dispose();
                if (master.IsContain(slave, false)) {
                    return new CutSolution(new PlainShape(allocator), new PlainShape(allocator), SubtractSolution.Nature.hole);
                } else if (slave.IsContain(master, true)) {
                    return new CutSolution(new PlainShape(allocator), new PlainShape(allocator), SubtractSolution.Nature.empty);
                } else {
                    return new CutSolution(new PlainShape(allocator), new PlainShape(allocator), SubtractSolution.Nature.notOverlap);
                }
            }

            var restPathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);
            
            filterNavigator.Reset();
            var bitePathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);
            
            filterNavigator.Dispose();

            if (restPathList.layouts.Length > 0) {
                return new CutSolution(restPathList, bitePathList, nature: SubtractSolution.Nature.overlap);
            } else {
                return new CutSolution(restPathList, bitePathList, nature: SubtractSolution.Nature.notOverlap);
            }
        }
        
        public static SubtractSolution Intersect(this NativeArray<IntVector> master, NativeArray<IntVector> slave, IntGeom iGeom, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out, allocator);

            if (navigator.isEqual) {
                navigator.Dispose();
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                filterNavigator.Dispose();
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.notOverlap);
            }

            var pathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);
            
            filterNavigator.Dispose();

            var nature = pathList.layouts.Length > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new SubtractSolution(pathList, nature);
        }

        public static SubtractSolution Subtract(this NativeArray<IntVector> master, NativeArray<IntVector> slave, IntGeom iGeom, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out, allocator);

            if (navigator.isEqual) {
                navigator.Dispose();
                return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.empty);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                filterNavigator.Dispose();
                bool isSlaveHole = master.IsContain(slave, false);
                // TODO add tests for this cases
                if (isSlaveHole) {
                    return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.hole);
                } else {
                    bool isMasterHole = slave.IsContain(master, true);
                    if (isMasterHole) {
                        return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.empty);
                    } else {
                        return new SubtractSolution(new PlainShape(allocator), SubtractSolution.Nature.notOverlap);
                    }
                }
            }

            var pathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);

            filterNavigator.Dispose();

            var nature = pathList.layouts.Length > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new SubtractSolution(pathList, nature);
        }

        public static UnionSolution Union(this NativeArray<IntVector> master, NativeArray<IntVector> slave, IntGeom iGeom, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.out_in, allocator);

            if (navigator.isEqual) {
                navigator.Dispose();
                return new UnionSolution(new PlainShape(master, true, allocator), UnionSolution.Nature.overlap);
            }

            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.outside, PinPoint.PinType.in_out, Allocator.Temp);

            var cursor = filterNavigator.First();

            if (cursor.isEmpty) {
                filterNavigator.Dispose();
                if (navigator.hasContacts) {
                    if (master.IsContain(slave.AnyInside(true))) {
                        return new UnionSolution(new PlainShape(master, true, allocator), UnionSolution.Nature.masterIncludeSlave);
                    }
                } else if (master.IsContain(slave.Any())) {
                    return new UnionSolution(new PlainShape(master, true, allocator), UnionSolution.Nature.masterIncludeSlave);
                } else if (slave.IsContain(master.Any())) {
                    return new UnionSolution(new PlainShape(slave, true, allocator), UnionSolution.Nature.slaveIncludeMaster);
                }
                
                return new UnionSolution(new PlainShape(allocator), UnionSolution.Nature.notOverlap);
            }

            if (cursor.type == PinPoint.PinType.in_out && slave.IsContain(master.AnyInside(true))) {
                filterNavigator.Dispose();
                return new UnionSolution(new PlainShape(slave, true, allocator), UnionSolution.Nature.slaveIncludeMaster);
            }


            var pathList = UnionSolver.Union(filterNavigator, master, slave, allocator);

            filterNavigator.Dispose();

            var nature = pathList.layouts.Length > 0 ? UnionSolution.Nature.overlap : UnionSolution.Nature.notOverlap;

            return new UnionSolution(pathList, nature);
        }
    }

}