using iShape.Clipper.Intersection;
using iShape.Clipper.Intersection.Navigation;
using iShape.Clipper.Intersection.Primitive;
using iShape.Collections;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Shape {

    public static class ShapeSubtractExt {

        public static SubtractSolution Subtract(this NativeArray<IntVector> master, NativeArray<IntVector> slave,
            IntGeom iGeom, Allocator allocator) {
            var navigator = Intersector.FindPins(master, slave, iGeom, PinPoint.PinType.in_out);

            var pathList = new PlainPathList(0, allocator);
            if (navigator.isEqual) {
                return new SubtractSolution(pathList, SubtractSolution.Nature.empty);
            }

            var cursor = navigator.NextSub();

            if (cursor.isEmpty) {
                return new SubtractSolution(pathList, SubtractSolution.Nature.notOverlap);
            }

            int masterCount = master.Length;
            int masterLastIndex = masterCount - 1;

            int slaveCount = slave.Length;
            int slaveLastIndex = slaveCount - 1;

            var path = new DynamicArray<IntVector>(0, Allocator.Temp);
            
            while (cursor.isNotEmpty) {
                navigator.Mark(cursor);

                var start = cursor;

                do {
                    // in-out slave path

                    var outCursor = navigator.NextSlaveOut(cursor, start);

                    var inSlaveStart = navigator.SlaveStartStone(cursor);

                    PathMileStone outSlaveEnd;

                    bool isOutInStart = outCursor == start && path.Count > 0;

                    if (!isOutInStart) {
                        outSlaveEnd = navigator.SlaveEndStone(outCursor);
                    } else {
                        // possible if we start with out-in
                        outSlaveEnd = navigator.SlaveStartStone(outCursor);
                    }

                    var startPoint = navigator.SlaveStartPoint(cursor);
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

                    if (isOutInStart) {
                        // possible if we start with out-in
                        break;
                    }

                    var endPoint = navigator.SlaveEndPoint(outCursor);
                    path.Add(endPoint);

                    cursor = navigator.NextMaster(outCursor);
                    navigator.Mark(cursor);

                    // out-in master path

                    var outMasterEnd = navigator.MasterEndStone(outCursor);
                    var inMasterStart = navigator.MasterStartStone(cursor);


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

                cursor = navigator.NextSub();
            }
            
            path.Dispose();

            var nature = pathList.Count > 0
                ? SubtractSolution.Nature.overlap
                : SubtractSolution.Nature.notOverlap;
            var solution = new SubtractSolution(pathList, nature);

            return solution;
        }


        private static Cursor NextSub(ref this PinNavigator self) {
            var cursor = self.Next();

            while (cursor.isNotEmpty && cursor.type != PinPoint.PinType.inside &&
                   cursor.type != PinPoint.PinType.out_in) {
                self.Mark(cursor);
                cursor = self.Next();
            }

            return cursor;
        }

        private static Cursor NextSlaveOut(ref this PinNavigator self, Cursor start, Cursor stop) {
            var prev = start;
            var cursor = self.NextSlave(start);

            while (start != cursor && stop != cursor && cursor.type == PinPoint.PinType.out_in) {
                var nextMaster = self.NextMaster(cursor);

                if (nextMaster == start) {
                    return cursor;
                }

                var nextSlave = self.NextSlave(cursor);

                var isCanSkip = self.IsCanSkip(prev, cursor, nextSlave);
                if (!isCanSkip) {
                    return cursor;
                }

                self.Mark(cursor);
                prev = cursor;
                cursor = nextSlave;
            }

            return cursor;
        }

        private static bool IsCanSkip(ref this PinNavigator self, Cursor prev, Cursor cursor, Cursor nextSlave) {
            var nextMaster = cursor;
            bool isFoundMaster;
            bool isFoundStart;
            do {
                nextMaster = self.NextMaster(nextMaster);
                isFoundMaster = nextMaster == nextSlave;
                isFoundStart = nextMaster == prev;
            } while (!(isFoundMaster || isFoundStart));

            return isFoundMaster;
        }
    }

}