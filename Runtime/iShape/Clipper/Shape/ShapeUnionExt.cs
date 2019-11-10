using iShape.Clipper.Intersection;
using iShape.Clipper.Intersection.Navigation;
using iShape.Clipper.Intersection.Primitive;
using iShape.Collections;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Shape {
    
    public static class ShapeUnionExt {
        
        public static UnionSolution Union(
            this NativeArray<IntVector> master, NativeArray<IntVector> slave, IntGeom iGeom, Allocator allocator
        ) {
            var navigator = Intersector.FindPins(master, slave, iGeom, PinPoint.PinType.out_in);

            var pathList = new PlainPathList(0, allocator);
            if (navigator.isEqual) {
                pathList.Add(master, true);
                return new UnionSolution(pathList, UnionSolution.Nature.overlap);
            }

            var cursor = navigator.NextUnion();

            if (cursor.isEmpty) {
                if (navigator.hasContacts) {
                    if (master.IsOverlap(slave)) {
                        pathList.Add(master, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else if (slave.IsOverlap(slave)) {
                        pathList.Add(slave, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else {
                        return new UnionSolution(pathList, UnionSolution.Nature.notOverlap);
                    }
                } else {
                    if (master.IsContain(slave.Any())) {
                        pathList.Add(master, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else if (slave.IsContain(point: master.Any())) {
                        pathList.Add(slave, true);
                        return new UnionSolution(pathList, UnionSolution.Nature.overlap);
                    } else {
                        return new UnionSolution(pathList, UnionSolution.Nature.notOverlap);
                    }
                }
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

                    var outSlaveEnd = navigator.SlaveEndStone(outCursor);

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

                bool isClockWise = path.IsClockWise();
                pathList.Add(path.slice, isClockWise);

                path.RemoveAll();

                cursor = navigator.NextUnion();
            }

            path.Dispose();

            var nature = pathList.Count > 0 ? UnionSolution.Nature.overlap : UnionSolution.Nature.notOverlap;

            var solution = new UnionSolution(pathList, nature);
            return solution;
        }

        private static Cursor NextUnion(ref this PinNavigator self) {
            var cursor = self.Next();

            while (cursor.isNotEmpty && cursor.type != PinPoint.PinType.outside &&
                   cursor.type != PinPoint.PinType.in_out) {
                self.Mark(cursor);
                cursor = self.Next();
            }

            return cursor;
        }

        private static Cursor NextSlaveOut(ref this PinNavigator self, Cursor start, Cursor stop) {
            var prev = start;
            var cursor = self.NextSlave(start);

            while (start != cursor && stop != cursor && cursor.type == PinPoint.PinType.in_out) {
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

        private static IntVector Any(ref this NativeArray<IntVector> self) {
            var a = self[0];
            var b = self[1];
            return new IntVector((a.x + b.x) >> 1, (a.y + b.y) >> 1);
        }

        private static bool IsContain(ref this NativeArray<IntVector> self, IntVector point) {
            int n = self.Length;
            var isContain = false;
            var p2 = self[n - 1];
            for (int i = 0; i < n; ++i) {
                var p1 = self[i];
                if (p1.y > point.y != p2.y > point.y &&
                    point.x < (p2.x - p1.x) * (point.y - p1.y) / (p2.y - p1.y) + p1.x) {
                    isContain = !isContain;
                }

                p2 = p1;
            }

            return isContain;
        }

        private static bool IsOverlap(ref this NativeArray<IntVector> self, NativeArray<IntVector> points) {
            int n = points.Length;
            var a = points[n - 1];
            for (int i = 0; i < n; ++i) {
                var b = points[i];
                var c = new IntVector((a.x + b.x) >> 1, (a.y + b.y) >> 1);
                if (self.IsContain(c)) {
                    return true;
                }

                a = b;
            }

            return false;
        }
    }
}