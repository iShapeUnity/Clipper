using iShape.Clipper.Intersection.Primitive;
using iShape.Clipper.Util;
using iShape.Geometry;
using iShape.Support;
using Unity.Collections;

namespace iShape.Clipper.Intersection {
    internal struct PinPathBuilder {
        internal struct Result {
            internal enum PathType {
                noEdges,
                hasEdges,
                equal
            }

            internal readonly PathType pathType;
            internal readonly NativeArray<PinPath> pinPath;

            internal Result(PathType pathType, NativeArray<PinPath> pinPath) {
                this.pathType = pathType;
                this.pinPath = pinPath;
            }

        }

        private NativeArray<PinEdge> pinEdges;
        private readonly IntGeom iGeom;

        internal PinPathBuilder(NativeArray<PinEdge> edges, IntGeom iGeom) {
            this.pinEdges = edges;
            this.iGeom = iGeom;
        }


        internal Result Build(NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            int n = pinEdges.Length;
            if (n == 0) {
                return new Result(Result.PathType.noEdges, new NativeArray<PinPath>(0, allocator));
            }

            sort();

            var mergedEdges = new DynamicArray<PinEdge>(n, allocator);

            var i = 0;
            do {
                var edge = pinEdges[i];

                var v1 = edge.v1;

                int j = i + 1;

                while (j < n) {
                    var next = pinEdges[j];

                    // must be same or next edge
                    if (v1.masterMileStone == next.v0.masterMileStone) {
                        j += 1;
                        v1 = next.v1;
                    }
                    else {
                        break;
                    }
                }

                var path = new PinEdge(edge.v0, v1, edge.interposition);

                mergedEdges.Add(path);

                i = j;
            } while (i < n);

            if (mergedEdges.Count > 1) {
                var first = mergedEdges[0];
                var last = mergedEdges[mergedEdges.Count - 1];

                if (first.v0.masterMileStone == last.v1.masterMileStone) {
                    mergedEdges[0] = new PinEdge(last.v0, first.v1, first.interposition);
                    mergedEdges.RemoveLast();
                }
            }

            var pathList = CreatePath(mergedEdges.Convert(), master, slave, allocator);

            return new Result(Result.PathType.hasEdges, pathList);
        }

        internal void Dispose() {
            pinEdges.Dispose();
        }

        private void sort() {
            // this array is already sorted by edge index

            int n = pinEdges.Length;

            bool isNotSorted;

            var m = n;

            do {
                isNotSorted = false;
                var a = pinEdges[0];
                var i = 1;
                while (i < m) {
                    var b = pinEdges[i];
                    if (PathMileStone.Compare(a.v0.masterMileStone, b.v0.masterMileStone)) {
                        pinEdges[i - 1] = b;
                        isNotSorted = true;
                    }
                    else {
                        pinEdges[i - 1] = a;
                        a = b;
                    }

                    i += 1;
                }

                m -= 1;
                pinEdges[m] = a;
            } while (isNotSorted);
        }


        private NativeArray<PinPath> CreatePath(NativeArray<PinEdge> edges, NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            int n = edges.Length;
            var pathList = new NativeArray<PinPath>(n, allocator);
            for (int i = 0; i < n; ++i) {
                var edge = edges[i];
                var type = GetType(edge, master, slave);
                var path = new PinPath(edge.v0, edge.v1, type);
                pathList[i] = path;
            }

            return pathList;
        }

        private PinPoint.PinType GetType(PinEdge edge, NativeArray<IntVector> master, NativeArray<IntVector> slave) {
            var type0 = GetStartDisposition(edge.v0, master, slave, edge.interposition);
            var type1 = GetEndDisposition(edge.v1, master, slave, edge.interposition);

            if (type0 == PinPoint.PinType.nil || type1 == PinPoint.PinType.nil) {
                if (type0 > 0 || type1 > 0) {
                    return PinPoint.PinType.inside;
                }

                if (type0 < 0 || type1 < 0) {
                    return PinPoint.PinType.outside;
                }

                return PinPoint.PinType.nil;
            }


            if (type0 == PinPoint.PinType.in_null && type1 == PinPoint.PinType.null_in ||
                type1 == PinPoint.PinType.in_null && type0 == PinPoint.PinType.null_in) {
                return PinPoint.PinType.inside;
            }

            if (type0 == PinPoint.PinType.out_null && type1 == PinPoint.PinType.null_out ||
                type1 == PinPoint.PinType.out_null && type0 == PinPoint.PinType.null_out) {
                return PinPoint.PinType.outside;
            }

            if (type0 == PinPoint.PinType.out_null && type1 == PinPoint.PinType.null_in ||
                type1 == PinPoint.PinType.out_null && type0 == PinPoint.PinType.null_in) {
                return PinPoint.PinType.out_in;
            }

            if (type0 == PinPoint.PinType.in_null && type1 == PinPoint.PinType.null_out ||
                type1 == PinPoint.PinType.in_null && type0 == PinPoint.PinType.null_out) {
                return PinPoint.PinType.in_out;
            }

            // TODO assert fail
            return 0;
        }

        private PinPoint.PinType GetStartDisposition(PinPoint vertex, NativeArray<IntVector> master, NativeArray<IntVector> slave, int iterposition) {
            var corner = BuildMasterCorner(vertex, master, iGeom);

            int si = vertex.slaveMileStone.index;
            int sn = slave.Length;
            IntVector s;

            if (iterposition == -1) {
                s = slave[(si + 1) % sn];
            }
            else {
                s = vertex.slaveMileStone.offset != 0 ? slave[si] : slave[(si - 1 + sn) % sn];
            }

            PinPoint.PinType type;

            if (corner.IsOnBorder(s)) {
                var slaveCorner = BuildSlaveCorner(vertex, slave, iGeom);

                int mi = vertex.masterMileStone.index;
                int mn = master.Length;
                IntVector m;

                if (iterposition == -1) {
                    m = master[(mi + 1) % mn];
                }
                else {
                    m = vertex.masterMileStone.offset != 0 ? master[mi] : slave[(mi - 1 + mn) % mn];
                }

                bool isBetween = slaveCorner.IsBetween(m, true);

                if (iterposition == 1) {
                    type = isBetween ? PinPoint.PinType.in_null : PinPoint.PinType.out_null;
                }
                else {
                    type = isBetween ? PinPoint.PinType.null_out : PinPoint.PinType.null_in;
                }
            }
            else {
                bool isBetween = corner.IsBetween(s, true);

                if (iterposition == 1) {
                    type = isBetween ? PinPoint.PinType.in_null : PinPoint.PinType.out_null;
                }
                else {
                    type = isBetween ? PinPoint.PinType.null_out : PinPoint.PinType.null_in;
                }
            }

            return type;
        }


        private PinPoint.PinType GetEndDisposition(PinPoint vertex, NativeArray<IntVector> master, NativeArray<IntVector> slave, int iterposition) {
            int i = vertex.slaveMileStone.index;
            int n = slave.Length;
            IntVector s;

            if (iterposition != 1) {
                s = vertex.slaveMileStone.offset != 0 ? slave[i] : slave[(i - 1 + n) % n];
            }
            else {
                s = slave[(i + 1) % n];
            }

            var corner = BuildMasterCorner(vertex, master, iGeom);

            if (corner.IsOnBorder(s)) {
                return PinPoint.PinType.nil;
            }

            bool isBetween = corner.IsBetween(s, true);

            PinPoint.PinType type;

            if (iterposition == 1) {
                type = isBetween ? PinPoint.PinType.null_out : PinPoint.PinType.null_in;
            }
            else {
                type = isBetween ? PinPoint.PinType.in_null : PinPoint.PinType.out_null;
            }

            return type;
        }


        private static Corner BuildMasterCorner(PinPoint vertex, NativeArray<IntVector> master, IntGeom iGeom) {
            int mi = vertex.masterMileStone.index;
            int mn = master.Length;
            var m1 = vertex.point;
            var m2 = master[(mi + 1) % mn];

            var m0 = vertex.masterMileStone.offset != 0 ? master[mi] : master[(mi - 1 + mn) % mn];

            return new Corner(m1, m0, m2, iGeom);
        }


        private static Corner BuildSlaveCorner(PinPoint vertex, NativeArray<IntVector> slave, IntGeom iGeom) {
            int si = vertex.slaveMileStone.index;
            int sn = slave.Length;

            var s1 = vertex.point;
            var s2 = slave[(si + 1) % sn];

            var s0 = vertex.slaveMileStone.offset != 0 ? slave[si] : slave[(si - 1 + sn) % sn];

            return new Corner(s1, s0, s2, iGeom);
        }
    }
}