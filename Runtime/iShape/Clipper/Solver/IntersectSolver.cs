using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Primitive;
using iShape.Collections;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class IntersectSolver {
        internal static PlainShape Intersect(FilterNavigator filterNavigator, NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var cursor = filterNavigator.Next();

            if (cursor.isEmpty || cursor.type != PinPoint.PinType.inside) {
                return new PlainShape(slave, false, allocator);
            }

            var pathList = new DynamicPlainShape(allocator);

            int masterCount = master.Length;
            int masterLastIndex = masterCount - 1;

            int slaveCount = slave.Length;
            int slaveLastIndex = slaveCount - 1;

            var path = new DynamicArray<IntVector>(0, Allocator.Temp);

            while (cursor.isNotEmpty) {
                var start = cursor;

                do {
                    // in-out slave path

                    var outCursor = filterNavigator.navigator.nextSlaveOut(cursor);

                    var inSlaveStart = filterNavigator.navigator.SlaveEndStone(cursor);
                    var outSlaveEnd = filterNavigator.navigator.SlaveStartStone(outCursor);

                    var startPoint = filterNavigator.navigator.SlaveEndPoint(cursor);
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

                    var endPoint = filterNavigator.navigator.SlaveStartPoint(outCursor);
                    path.Add(endPoint);

                    cursor = filterNavigator.navigator.PrevMaster(outCursor);
                    filterNavigator.navigator.Mark(cursor);

                    // out-in master path

                    var outMasterEnd = filterNavigator.navigator.MasterStartStone(outCursor);
                    var inMasterStart = filterNavigator.navigator.MasterEndStone(cursor);

                    bool isOutMasterNotOverflow;
                    int outMasterIndex;

                    if (outMasterEnd.offset != 0) {
                        isOutMasterNotOverflow = true;
                        outMasterIndex = outMasterEnd.index;
                    } else {
                        if (outMasterEnd.index != 0) {
                            isOutMasterNotOverflow = true;
                            outMasterIndex = outMasterEnd.index - 1;
                        } else {
                            isOutMasterNotOverflow = false;
                            outMasterIndex = masterCount - 1;
                        }
                    }

                    bool isInMasterNotOverflow;
                    int inMasterIndex;

                    if (inMasterStart.index + 1 < masterCount) {
                        isInMasterNotOverflow = true;
                        inMasterIndex = inMasterStart.index + 1;
                    } else {
                        isInMasterNotOverflow = false;
                        inMasterIndex = 0;
                    }

                    if (inMasterStart >= outMasterEnd) {
                        // a > b
                        if (isOutMasterNotOverflow) {
                            var sliceB = master.Slice(0, outMasterIndex + 1).Reversed(Allocator.Temp);
                            path.Add(sliceB);
                            sliceB.Dispose();
                        }

                        if (isInMasterNotOverflow) {
                            var sliceA = master.Slice(inMasterIndex, masterLastIndex - inMasterIndex + 1).Reversed(Allocator.Temp);
                            path.Add(sliceA);
                            sliceA.Dispose();
                        }
                    } else {
                        // a < b
                        if (isInMasterNotOverflow && isOutMasterNotOverflow && inMasterIndex <= outMasterIndex) {
                            var slice = master.Slice(inMasterIndex, outMasterIndex - inMasterIndex + 1).Reversed(Allocator.Temp);
                            path.Add(slice);
                            slice.Dispose();
                        }
                    }
                } while (cursor != start);

                pathList.Add(path.slice, false);
                path.RemoveAll();

                cursor = filterNavigator.Next();
            }

            return pathList.Convert();
        }

        private static Cursor nextSlaveOut(this ref PinNavigator self, Cursor cursor) {
            // keep in mind Test 11, 27
            var start = cursor;

            var next = self.NextSlave(cursor);

            while (start != next) {
                if (next.type == PinPoint.PinType.outside) {
                    break;
                }

                // only .out_in is possible here

                var nextNext = self.NextSlave(next);

                // try to find next cursor going by master
                var masterCursor = start;

                do {
                    masterCursor = self.NextMaster(masterCursor);
                } while (masterCursor != next && masterCursor != nextNext);

                if (masterCursor == next) {
                    return next;
                }

                // it's inner cursor, skip it
                self.Mark(next);

                next = nextNext;
            }

            return next;
        }
    }

}