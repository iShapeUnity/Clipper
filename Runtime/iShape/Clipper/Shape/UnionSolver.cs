using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Shape {

    public static class UnionSolver {

        public static UnionSolution Union(this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            PlainPathList pathList;

            if (navigator.isEqual) {
                pathList = new PlainPathList(1, allocator);
                pathList.Add(master, true);
                return new UnionSolution(new PlainPathList(), UnionSolution.Nature.overlap);
            }

            var unionNavigator = new UnionNavigator(navigator, Allocator.Temp);

            var cursor = unionNavigator.First();

            if (cursor.isEmpty) {
                if (navigator.hasContacts) {
                    if (master.isOverlap(slave)) {
                        pathList = new PlainPathList(1, allocator);
                        pathList.Add(master, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else if (slave.isOverlap(points: slave)) {
                        pathList = new PlainPathList(1, allocator);
                        pathList.Add(slave, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else {
                        pathList = new PlainPathList(0, allocator);
                        return new UnionSolution(pathList, UnionSolution.Nature.notOverlap);
                    }
                } else {
                    if (master.isContain(slave.any())) {
                        pathList = new PlainPathList(1, allocator);
                        pathList.Add(master, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else if (slave.isContain(master.any())) {
                        pathList = new PlainPathList(1, allocator);
                        pathList.Add(slave, isClockWise: true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else {
                        pathList = new PlainPathList(0, allocator);
                        return new UnionSolution(pathList, UnionSolution.Nature.notOverlap);
                    }
                }
            }

            pathList = Union(unionNavigator, master, slave, iGeom, allocator);

            if (pathList.Count > 0) {
                return new UnionSolution(pathList, UnionSolution.Nature.overlap);
            }

            return new UnionSolution(pathList, UnionSolution.Nature.notOverlap);
        }



        private static IntVector any(this NativeArray<IntVector> self) {
            var a = self[0];
            var b = self[1];
            return new IntVector((a.x + b.x) >> 1, (a.y + b.y) >> 1);
        }

        private static bool isContain(this NativeArray<IntVector> self, IntVector point) {
            int n = self.Length;
            var isContain = false;
            var p2 = self[n - 1];
            for (int i = 0; i < n; ++i) {
                var p1 = self[i];
                if (((p1.y > point.y) != (p2.y > point.y)) &&
                    point.x < ((p2.x - p1.x) * (point.y - p1.y) / (p2.y - p1.y) + p1.x)) {
                    isContain = !isContain;
                }

                p2 = p1;
            }

            return isContain;
        }

        private static bool isOverlap(this NativeArray<IntVector> self, NativeArray<IntVector> points) {
            int n = points.Length;
            var a = points[n - 1];
            for (int i = 0; i < n; ++i) {
                var b = points[i];
                var c = new IntVector((a.x + b.x) >> 1, (a.y + b.y) >> 1);
                if (self.isContain(c)) {
                    return true;
                }

                a = b;
            }

            return false;
        }
    }

}