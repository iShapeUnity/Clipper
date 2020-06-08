using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Util;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class RootSolver {
        public static DualSolution Cut(this NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave,  PinPoint.PinType.in_out, allocator);
            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);
            var nature = filterNavigator.Nature(master, slave, false);
            switch (nature) {
                case Solution.Nature.notOverlap:
                case Solution.Nature.equal:
                case Solution.Nature.masterIncludeSlave:
                case Solution.Nature.slaveIncludeMaster:
                    filterNavigator.Dispose();
                    return new DualSolution(new PlainShape(allocator), new PlainShape(allocator), nature);
                case Solution.Nature.overlap:
                    var cursor = filterNavigator.First;
                    if (cursor.type == PinPoint.PinType.out_in && !filterNavigator.HasEdge) {
                        if (master.IsContain(slave)) {
                            filterNavigator.Dispose();
                            return new DualSolution(new PlainShape(allocator), new PlainShape(allocator), Solution.Nature.masterIncludeSlave);
                        }
                        if (slave.IsContain(master)) {
                            filterNavigator.Dispose();
                            return new DualSolution(new PlainShape(allocator), new PlainShape(allocator), Solution.Nature.slaveIncludeMaster);
                        }
                    }
                    var restPathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);
                    filterNavigator.Reset();
                    var bitePathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);
                    
                    filterNavigator.Dispose();
                    return new DualSolution(restPathList, bitePathList, Solution.Nature.overlap);
            }
            
            filterNavigator.Dispose();
            return new DualSolution(new PlainShape(allocator), new PlainShape(allocator), Solution.Nature.notOverlap);
        }

        public static Solution Intersect(this NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave,  PinPoint.PinType.in_out, allocator);
            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);
            var nature = filterNavigator.Nature(master, slave, false);
            switch (nature) {
                case Solution.Nature.notOverlap:
                case Solution.Nature.equal:
                case Solution.Nature.masterIncludeSlave:
                case Solution.Nature.slaveIncludeMaster:
                    filterNavigator.Dispose();
                    return new Solution(new PlainShape(allocator), nature);
                case Solution.Nature.overlap:
                    var cursor = filterNavigator.First;
                    if (cursor.type == PinPoint.PinType.out_in && !filterNavigator.HasEdge) {
                        if (master.IsContain(slave)) {
                            filterNavigator.Dispose();
                            return new Solution(new PlainShape(allocator), Solution.Nature.masterIncludeSlave);
                        }
                        if (slave.IsContain(master)) {
                            filterNavigator.Dispose();
                            return new Solution(new PlainShape(allocator), Solution.Nature.slaveIncludeMaster);
                        }
                    }
                    var pathList = IntersectSolver.Intersect(filterNavigator, master, slave, allocator);
                    filterNavigator.Dispose();
                    return new Solution(pathList, Solution.Nature.overlap);
            }
            
            filterNavigator.Dispose();
            return new Solution(new PlainShape(allocator), Solution.Nature.notOverlap);
        }

        public static Solution Subtract(this NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave,  PinPoint.PinType.in_out, allocator);
            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.inside, PinPoint.PinType.out_in, Allocator.Temp);
            var nature = filterNavigator.Nature(master, slave, false);
            switch (nature) {
                case Solution.Nature.notOverlap:
                case Solution.Nature.equal:
                case Solution.Nature.masterIncludeSlave:
                case Solution.Nature.slaveIncludeMaster:
                    filterNavigator.Dispose();
                    return new Solution(new PlainShape(allocator), nature);
                case Solution.Nature.overlap:
                    var cursor = filterNavigator.First;
                    if (cursor.type == PinPoint.PinType.out_in && !filterNavigator.HasEdge) {
                        if (master.IsContain(slave)) {
                            filterNavigator.Dispose();
                            return new Solution(new PlainShape(allocator), Solution.Nature.masterIncludeSlave);
                        }
                        if (slave.IsContain(master)) {
                            filterNavigator.Dispose();
                            return new Solution(new PlainShape(allocator), Solution.Nature.slaveIncludeMaster);
                        }
                    }
                    var pathList = SubtractSolver.Subtract(filterNavigator, master, slave, allocator);
                    
                    filterNavigator.Dispose();
                    return new Solution(pathList, Solution.Nature.overlap);
            }
            
            filterNavigator.Dispose();
            return new Solution(new PlainShape(allocator), Solution.Nature.notOverlap);
        }

        public static Solution Union(this NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave, PinPoint.PinType.out_in, allocator);
            var filterNavigator = new FilterNavigator(navigator, PinPoint.PinType.outside, PinPoint.PinType.in_out, Allocator.Temp);
            var nature = filterNavigator.Nature(master, slave, true);
            switch (nature) {
                case Solution.Nature.notOverlap:
                case Solution.Nature.equal:
                case Solution.Nature.masterIncludeSlave:
                case Solution.Nature.slaveIncludeMaster:
                    filterNavigator.Dispose();
                    return new Solution(new PlainShape(allocator), nature);
                case Solution.Nature.overlap:
                    var cursor = filterNavigator.First;
                    if (cursor.type == PinPoint.PinType.in_out) {
                        if (master.IsContain(slave)) {
                            filterNavigator.Dispose();
                            return new Solution(new PlainShape(allocator), Solution.Nature.masterIncludeSlave);
                        }
                        if (slave.IsContain(master)) {
                            filterNavigator.Dispose();
                            return new Solution(new PlainShape(allocator), Solution.Nature.slaveIncludeMaster);
                        }
                    }
                    var pathList = UnionSolver.Union(filterNavigator, master, slave, allocator);
                    
                    filterNavigator.Dispose();
                    return new Solution(pathList, Solution.Nature.overlap);
            }

            filterNavigator.Dispose();
            return new Solution(new PlainShape(allocator), Solution.Nature.notOverlap);
        }
    }

}