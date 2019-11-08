using iShape.Clipper.Intersection.Navigation;
using iShape.Clipper.Intersection.Primitive;
using iShape.Geometry;
using iShape.Collections;
using Unity.Collections;
using UnityEngine;

namespace iShape.Clipper.Intersection {
    public struct Intersector {
        public static PinNavigator FindPins(NativeArray<IntVector> iMaster, NativeArray<IntVector> iSlave, IntGeom iGeom, PinPoint.PinType exclusionPinType) {
            var posMatrix = CreatePossibilityMatrix(iMaster, iSlave, Allocator.Temp);

            var masterIndices = posMatrix.masterIndices.ToArray(Allocator.Temp);
            var slaveIndices = posMatrix.slaveIndices.ToArray(Allocator.Temp);

            posMatrix.Dispose();

            var pinPoints = new DynamicArray<PinPoint>(0, Allocator.Temp);
            var pinEdges = new DynamicArray<PinEdge>(0, Allocator.Temp);

            int masterCount = iMaster.Length;
            int slaveCount = iSlave.Length;

            int n = masterIndices.Length;
            int i = 0;

            int msLastIx = iMaster.Length - 1;
            int slLastIx = iSlave.Length - 1;

            bool hasExclusion = false;
            
            while (i < n) {
                int msIx0 = masterIndices[i];
                int msIx1 = msIx0 < msLastIx ? msIx0 + 1 : 0;

                var ms0 = iMaster[msIx0];
                var ms1 = iMaster[msIx1];

                int j = i;

                do {
                    int slIx0 = slaveIndices[j];
                    int slIx1 = slIx0 < slLastIx ? slIx0 + 1 : 0;

                    var sl0 = iSlave[slIx0];
                    var sl1 = iSlave[slIx1];

                    var intersectionTest = CrossResolver.DefineType(ms0, ms1, sl0, sl1);

                    // -1, 1 are the most possible cases (more then 99%)
                    // -1 - no intersections
                    //  1 - simple intersection with no overlaps


                    // 0, 2 are very specific, but still possible cases
                    // 0 - same line
                    // 2 - one of the end is lying on others edge

                    // case when one on of slave ends is overlapped by on of the master ends
                    // can conflict with possible edge case
                    j += 1;

                    switch (intersectionTest) {
                        case CrossType.not_cross:
                            continue;
                        case CrossType.pure: {
                            var point = Cross(ms0, ms1, sl0, sl1);
                            // simple intersection and most common case

                            var pinPointDef = new PinPoint.Def(
                                point,
                                ms0,
                                ms1,
                                sl0,
                                sl1,
                                new PathMileStone(msIx0, ms0.SqrDistance(point)),
                                new PathMileStone(slIx0, sl0.SqrDistance(point))
                            );

                            var pinPoint = PinPoint.BuildSimple(pinPointDef);
                            pinPoints.Add(pinPoint);

                            continue;
                        }
                        case CrossType.edge_cross: {
                            // one of the end is lying on others edge

                            var point = EndCross(ms0, ms1, sl0, sl1);

                            bool isMsEnd = ms0 == point || ms1 == point;
                            bool isSlEnd = sl0 == point || sl1 == point;

                            // skip case when on of the slave end is equal to one of the master end
                            if (!(isMsEnd && isSlEnd)) {
                                int prevMs = msIx0;
                                int nextMs = msIx1;

                                int prevSl = slIx0;
                                int nextSl = slIx1;

                                int masterEdge = msIx0;
                                long masterOffset = 0;

                                var slaveEdge = slIx0;
                                long slaveOffset = 0;

                                if (isMsEnd) {
                                    if (ms0 == point) {
                                        prevMs = (msIx0 - 1 + masterCount) % masterCount;
                                    }
                                    else {
                                        nextMs = (msIx1 + 1) % masterCount;
                                        masterEdge = msIx1;
                                    }

                                    slaveOffset = sl0.SqrDistance(point);
                                }

                                if (isSlEnd) {
                                    if (sl0 == point) {
                                        prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                                    }
                                    else {
                                        slaveEdge = slIx1;
                                        nextSl = (slIx1 + 1) % slaveCount;
                                    }

                                    masterOffset = ms0.SqrDistance(point);
                                }

                                var pinPointDef = new PinPoint.Def(
                                    point,
                                    iMaster[prevMs],
                                    iMaster[nextMs],
                                    iSlave[prevSl],
                                    iSlave[nextSl],
                                    new PathMileStone(masterEdge, masterOffset),
                                    new PathMileStone(slaveEdge, slaveOffset));

                                if (isMsEnd) {
                                    // pin point is on slave
                                    var pinPoint = PinPoint.BuildOnSlave(pinPointDef);
                                    if (pinPoint.type != exclusionPinType) {
                                        pinPoints.Add(pinPoint);
                                    }
                                }
                                else if (isSlEnd) {
                                    // pin point is on master
                                    var pinPoint = PinPoint.BuildOnMaster(pinPointDef);
                                    if (pinPoint.type != exclusionPinType) {
                                        pinPoints.Add(pinPoint);
                                    } else {
                                        hasExclusion = true;
                                    }
                                }

                                continue;
                            }

                            break;
                        }
                        case CrossType.same_line: {
                            // possible edge case

                            var ms0Pt = new Vertex(msIx0, ms0);
                            var ms1Pt = new Vertex(msIx1, ms1);
                            var sl0Pt = new Vertex(slIx0, sl0);
                            var sl1Pt = new Vertex(slIx1, sl1);

                            var pinEdge = new PinEdge(ms0Pt, ms1Pt, sl0Pt, sl1Pt);
                            if (!pinEdge.isZeroLength) {
                                pinEdges.Add(pinEdge);
                                continue;
                            }

                            break;
                        }
                        case CrossType.common_end:
                            break;
                    }


                    // only 0, 2, 3 cases are possible here

                    // lets ignore case for second end (it just add double)
                    bool isFirstPointCross = ms0 == sl0 || ms0 == sl1;

                    if (isFirstPointCross) {
                        var point = ms0;

                        int masterIndex = msIx0;
                        int slaveIndex;

                        int prevMs = (msIx0 - 1 + masterCount) % masterCount;
                        int nextMs = msIx1;

                        int prevSl = slIx0;
                        int nextSl = slIx1;

                        if (sl0 == point) {
                            slaveIndex = slIx0;
                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                        }
                        else {
                            slaveIndex = slIx1;
                            nextSl = (slIx1 + 1) % slaveCount;
                        }

                        var pinPointDef = new PinPoint.Def(
                            point,
                            iMaster[prevMs],
                            iMaster[nextMs],
                            iSlave[prevSl],
                            iSlave[nextSl],
                            new PathMileStone(masterIndex),
                            new PathMileStone(slaveIndex)
                        );

                        var pinPoint = PinPoint.BuildOnCross(pinPointDef, iGeom);
                        if (pinPoint.type != exclusionPinType) {
                            pinPoints.Add(pinPoint);
                        }
                    }
                } while (j < n && msIx0 == masterIndices[j]);

                i = j;
            }

            if (iMaster.Length == iSlave.Length && iMaster.Length == pinEdges.Count) {
                if (iMaster == iSlave) {
                    masterIndices.Dispose();
                    slaveIndices.Dispose();
                    return new PinNavigator();
                }
            }
            
            masterIndices.Dispose();
            slaveIndices.Dispose();

            // merge all edges
            var builder = new PinPathBuilder(pinEdges.Convert(), iGeom);

            // build pin paths from edges
            var result = builder.Build(iMaster, iSlave, Allocator.Temp);
            builder.Dispose();
            
            if (result.pathType == PinPathBuilder.Result.PathType.equal) {
                return new PinNavigator();
            }

            // combine pin points and paths
            var sequence = new PinSequence(pinPoints.Convert(), result.pinPath, iMaster.Length, Allocator.Temp);

            // remove doubles and organize data
            var navigator = sequence.Convert(exclusionPinType, hasExclusion);

            return navigator;
        }

        private static AdjacencyMatrix CreatePossibilityMatrix(NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var slaveBoxArea = new iShape.Clipper.Util.Rect(long.MaxValue, long.MaxValue, long.MinValue, long.MinValue);

            var slaveSegmentsBoxAreas = new DynamicArray<iShape.Clipper.Util.Rect>(8, Allocator.Temp);

            int lastSlaveIndex = slave.Length - 1;

            for (int i = 0; i <= lastSlaveIndex; ++i) {
                var a = slave[i];
                var b = slave[i != lastSlaveIndex ? i + 1 : 0];

                slaveBoxArea.Assimilate(a);
                slaveSegmentsBoxAreas.Add(new iShape.Clipper.Util.Rect(a, b));
            }

            var posMatrix = new AdjacencyMatrix(0, allocator);

            int lastMasterIndex = master.Length - 1;

            for (int i = 0; i <= lastMasterIndex; ++i) {
                var master_0 = master[i];
                var master_1 = master[i != lastMasterIndex ? i + 1 : 0];

                bool isIntersectionImpossible = slaveBoxArea.IsNotIntersecting(master_0, master_1);

                if (isIntersectionImpossible) {
                    continue;
                }

                var segmentBoxArea = new iShape.Clipper.Util.Rect(master_0, master_1);
                for (int j = 0; j <= lastSlaveIndex; ++j) {
                    bool isIntersectionPossible = slaveSegmentsBoxAreas[j].IsIntersecting(segmentBoxArea);

                    if (isIntersectionPossible) {
                        posMatrix.AddMate(i, j);
                    }
                }
            }

            slaveSegmentsBoxAreas.Dispose();

            return posMatrix;
        }


        private static IntVector Cross(IntVector a0, IntVector a1, IntVector b0, IntVector b1) {
            long dxA = a0.x - a1.x;
            long dyB = b0.y - b1.y;
            long dyA = a0.y - a1.y;
            long dxB = b0.x - b1.x;

            float divider = dxA * dyB - dyA * dxB;

            float xyA = a0.x * a1.y - a0.y * a1.x;
            float xyB = b0.x * b1.y - b0.y * b1.x;

            float invertDivider = 1.0f / divider;

            float x = xyA * (b0.x - b1.x) - (a0.x - a1.x) * xyB;
            float y = xyA * (b0.y - b1.y) - (a0.y - a1.y) * xyB;

            return new IntVector((long) Mathf.Round(x * invertDivider), (long) Mathf.Round(y * invertDivider));
        }
        
        private static IntVector EndCross(IntVector a0, IntVector a1, IntVector b0, IntVector b1) {
            var p = Cross(a0, a1, b0, b1);
        
            if (a0 == p || a1 == p || b0 == p || b1 == p) {
                return p;
            }

            var dx = a0.x - p.x;
            var dy = a0.y - p.y;
            var dl = dx * dx + dy * dy;
            var minP = a0;
            var minL = dl;

            dx = a1.x - p.x;
            dy = a1.y - p.y;
            dl = dx * dx + dy * dy;
        
            if (minL > dl) {
                minP = a1;
                minL = dl;
            }

            dx = b0.x - p.x;
            dy = b0.y - p.y;
            dl = dx * dx + dy * dy;
        
            if (minL > dl) {
                minP = b0;
                minL = dl;
            }

            dx = b1.x - p.x;
            dy = b1.y - p.y;
            dl = dx * dx + dy * dy;
        
            if (minL > dl) {
                minP = b1;
            }

            return minP;
        }
    }
}