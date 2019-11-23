using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Primitive;
using iShape.Collections;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class UnionSolver {
        public static UnionSolution Union(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.out_in);

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

            pathList = Union(unionNavigator, master, slave, allocator);
            
            navigator.Dispose();

            var nature = pathList.Count > 0 ? UnionSolution.Nature.overlap : UnionSolution.Nature.notOverlap;

            return new UnionSolution(pathList, nature);
        }

        internal static PlainPathList Union(
            UnionNavigator navigator, NativeArray<IntVector> master,
            NativeArray<IntVector> slave, Allocator allocator
        ) {
            var unionNavigator = navigator;

            var pathList = new PlainPathList(1, allocator);

            int masterCount = master.Length;
            int masterLastIndex = masterCount - 1;

            int slaveCount = slave.Length;
            int slaveLastIndex = slaveCount - 1;

            var cursor = unionNavigator.Next();

            var path = new DynamicArray<IntVector>(0, Allocator.Temp);
            
            while (cursor.isNotEmpty) {
                var start = cursor;

                do {
                    // in-out slave path

                    var outCursor = unionNavigator.navigator.nextSlaveOut(cursor);

                    var inSlaveStart = unionNavigator.navigator.SlaveStartStone(cursor);
                    var outSlaveEnd = unionNavigator.navigator.SlaveEndStone(outCursor);

                    var startPoint = unionNavigator.navigator.SlaveStartPoint(cursor);
                    path.Add(startPoint);

                    bool isInSlaveNotOverflow;
                    int inSlaveIndex;
                    if (inSlaveStart.index + 1 < slaveCount) {
                        isInSlaveNotOverflow = true;
                        inSlaveIndex = inSlaveStart.index + 1;
                    } else {
                        isInSlaveNotOverflow = false;
                        inSlaveIndex = 0;
                    }


                    bool isOutSlaveNotOverflow;
                    int outSlaveIndex;

                    if (outSlaveEnd.offset != 0) {
                        isOutSlaveNotOverflow = true;
                        outSlaveIndex = outSlaveEnd.index;
                    } else {
                        if (outSlaveEnd.index != 0) {
                            isOutSlaveNotOverflow = true;
                            outSlaveIndex = outSlaveEnd.index - 1;
                        } else {
                            isOutSlaveNotOverflow = false;
                            outSlaveIndex = slaveCount - 1;
                        }
                    }

                    if (inSlaveStart >= outSlaveEnd) {
                        // a > b
                        if (isInSlaveNotOverflow) {
                            var sliceA = slave.Slice(inSlaveIndex, slaveLastIndex - inSlaveIndex + 1);
                            path.Add(sliceA);
                        }

                        if (isOutSlaveNotOverflow) {
                            var sliceB = slave.Slice(0, outSlaveIndex + 1);
                            path.Add(sliceB);
                        }
                    } else {
                        // a < b
                        if (isInSlaveNotOverflow && isOutSlaveNotOverflow && inSlaveIndex <= outSlaveIndex) {
                            var slice = slave.Slice(inSlaveIndex, outSlaveIndex - inSlaveIndex + 1);
                            path.Add(slice);
                        }
                    }

                    var endPoint = unionNavigator.navigator.SlaveEndPoint(outCursor);
                    path.Add(endPoint);

                    cursor = unionNavigator.navigator.NextMaster(outCursor);
                    unionNavigator.navigator.Mark(cursor);

                    // out-in master path

                    var outMasterEnd = unionNavigator.navigator.MasterEndStone(outCursor);
                    var inMasterStart = unionNavigator.navigator.MasterStartStone(cursor);

                    bool isOutMasterNotOverflow;
                    int outMasterIndex;
                    if (outMasterEnd.index + 1 < masterCount) {
                        outMasterIndex = outMasterEnd.index + 1;
                        isOutMasterNotOverflow = true;
                    } else {
                        outMasterIndex = 0;
                        isOutMasterNotOverflow = false;
                    }


                    bool isInMasterNotOverflow;
                    int inMasterIndex;
                    if (inMasterStart.offset != 0) {
                        inMasterIndex = inMasterStart.index;
                        isInMasterNotOverflow = true;
                    } else {
                        if (inMasterStart.index != 0) {
                            inMasterIndex = inMasterStart.index - 1;
                            isInMasterNotOverflow = true;
                        } else {
                            inMasterIndex = masterCount - 1;
                            isInMasterNotOverflow = false;
                        }
                    }

                    if (outMasterEnd >= inMasterStart) {
                        // a > b
                        if (isOutMasterNotOverflow) {
                            var sliceA = master.Slice(outMasterIndex, masterLastIndex - outMasterIndex + 1);
                            path.Add(sliceA);
                        }

                        if (isInMasterNotOverflow) {
                            var sliceB = master.Slice(0, inMasterIndex + 1);
                            path.Add(sliceB);
                        }
                    } else {
                        // a < b
                        if (isInMasterNotOverflow && isOutMasterNotOverflow && outMasterIndex <= inMasterIndex) {
                            var slice = master.Slice(outMasterIndex, inMasterIndex - outMasterIndex + 1);
                            path.Add(slice);
                        }
                    }
                } while (cursor != start);

                bool isClockWise = path.IsClockWise();
                pathList.Add(path.slice, isClockWise);
                path.RemoveAll();
                
                cursor = unionNavigator.Next();
            }
            
            path.Dispose();

            return pathList;
        }

        private static Cursor nextSlaveOut(this ref PinNavigator self, Cursor cursor) {
            // keep in mind Test 11, 27
            var start = cursor;

            var next = self.NextSlave(cursor);

            if (start.type == PinPoint.PinType.in_out) {
                return next;
            }

            while (start != next) {
                if (next.type == PinPoint.PinType.inside) {
                    break;
                }

                // only .out_in is possible here

                var nextNext = self.NextSlave(next);

                // try to find next cursor going by master
                var masterCursor = start;

                do {
                    masterCursor = self.NextMaster(masterCursor);
                } while (masterCursor != next && masterCursor != nextNext);

                if (masterCursor != next) {
                    return next;
                }

                // it's inner cursor, skip it
                self.Mark(next);

                next = nextNext;
            }

            return next;
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

        private static bool IsClockWise(ref this DynamicArray<IntVector> self) {
            long sum = 0;
            int n = self.Count;
            var p1 = self[n - 1];
            for (int i = 0; i < n; ++i) {
                var p2 = self[i];
                var difX = p2.x - p1.x;
                var sumY = p2.y + p1.y;
                sum += difX * sumY;
                p1 = p2;
            }

            return sum >= 0;
        }
    }

}