using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Primitive;
using iShape.Collections;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    public static class SubtractSolver {
        public static SubtractSolution Subtract(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator
        ) {
            var navigator = CrossDetector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            PlainPathList pathList;
            if (navigator.isEqual) {
                pathList = new PlainPathList(0, allocator);
                return new SubtractSolution(pathList, SubtractSolution.Nature.empty);
            }

            var subNavigator = new InsideNavigator(navigator, Allocator.Temp);

            var cursor = subNavigator.First();

            if (cursor.isEmpty) {
                return new SubtractSolution(new PlainPathList(0, allocator), SubtractSolution.Nature.notOverlap);
            }

            pathList = Subtract(subNavigator, master, slave, allocator);

            navigator.Dispose();

            var nature = pathList.Count > 0 ? SubtractSolution.Nature.overlap : SubtractSolution.Nature.notOverlap;

            return new SubtractSolution(pathList, nature);
        }


        internal static PlainPathList Subtract(
            InsideNavigator navigator, NativeArray<IntVector> master,
            NativeArray<IntVector> slave, Allocator allocator
        ) {
            var subNavigator = navigator;

            var cursor = subNavigator.Next();
            var pathList = new PlainPathList(1, allocator);

            int masterCount = master.Length;
            int masterLastIndex = masterCount - 1;

            int slaveCount = slave.Length;
            int slaveLastIndex = slaveCount - 1;

            var path = new DynamicArray<IntVector>(0, Allocator.Temp);

            while (cursor.isNotEmpty) {
                var start = cursor;

                do {
                    // in-out slave path

                    var outCursor = subNavigator.navigator.nextSlaveOut(cursor);

                    var inSlaveStart = subNavigator.navigator.SlaveStartStone(cursor);
                    var outSlaveEnd = subNavigator.navigator.SlaveEndStone(outCursor);

                    var startPoint = subNavigator.navigator.SlaveStartPoint(cursor);
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

                    var endPoint = subNavigator.navigator.SlaveEndPoint(outCursor);
                    path.Add(endPoint);

                    cursor = subNavigator.navigator.NextMaster(outCursor);
                    subNavigator.navigator.Mark(cursor);

                    // out-in master path

                    var outMasterEnd = subNavigator.navigator.MasterEndStone(outCursor);
                    var inMasterStart = subNavigator.navigator.MasterStartStone(cursor);

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

                pathList.Add(path.slice, true);
                path.RemoveAll();

                cursor = subNavigator.Next();
            }

            return pathList;
        }

        private static Cursor nextSlaveOut(this ref PinNavigator self, Cursor cursor) {
            // keep in mind Test 11, 27
            var start = cursor;

            var next = self.NextSlave(cursor);

            if (start.type == PinPoint.PinType.out_in) {
                return next;
            }

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

                if (masterCursor != next) {
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