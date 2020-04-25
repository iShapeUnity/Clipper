using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Primitive;
using iShape.Collections;
using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class UnionSolver {
        
        internal static PlainShape Union(
            FilterNavigator filterNavigator, NativeArray<IntVector> master,
            NativeArray<IntVector> slave, Allocator allocator
        ) {
            var pathList = new DynamicPlainShape(allocator);

            int masterCount = master.Length;
            int masterLastIndex = masterCount - 1;

            int slaveCount = slave.Length;
            int slaveLastIndex = slaveCount - 1;

            var cursor = filterNavigator.Next();

            var path = new DynamicArray<IntVector>(0, Allocator.Temp);
            
            while (cursor.isNotEmpty) {
                var start = cursor;

                do {
                    // in-out slave path

                    var outCursor = filterNavigator.navigator.nextSlaveOut(cursor);

                    var inSlaveStart = filterNavigator.navigator.SlaveStartStone(cursor);
                    var outSlaveEnd = filterNavigator.navigator.SlaveEndStone(outCursor);

                    var startPoint = filterNavigator.navigator.SlaveStartPoint(cursor);
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

                    var endPoint = filterNavigator.navigator.SlaveEndPoint(outCursor);
                    path.Add(endPoint);

                    cursor = filterNavigator.navigator.NextMaster(outCursor);
                    filterNavigator.navigator.Mark(cursor);

                    // out-in master path

                    var outMasterEnd = filterNavigator.navigator.MasterEndStone(outCursor);
                    var inMasterStart = filterNavigator.navigator.MasterStartStone(cursor);

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
                path.Simplify();
                pathList.Add(path.slice, isClockWise);
                path.RemoveAll();
                
                cursor = filterNavigator.Next();
            }
            
            path.Dispose();

            return pathList.Convert();
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