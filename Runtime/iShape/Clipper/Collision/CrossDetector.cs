using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Sort;
using iShape.Geometry;
using iShape.Collections;
using Unity.Collections;

namespace iShape.Clipper.Collision {

    public struct CrossDetector {
        public static PinNavigator FindPins(NativeArray<IntVector> iMaster, NativeArray<IntVector> iSlave, IntGeom iGeom, PinPoint.PinType exclusionPinType) {
            var posMatrix = CreatePossibilityMatrix(iMaster, iSlave, Allocator.Temp);

            var masterIndices = posMatrix.masterIndices.ToArray(Allocator.Temp);
            var slaveIndices = posMatrix.slaveIndices.ToArray(Allocator.Temp);

            posMatrix.Dispose();

            var pinPoints = new DynamicArray<PinPoint>(0, Allocator.Temp);

            int masterCount = iMaster.Length;
            int slaveCount = iSlave.Length;

            int n = masterIndices.Length;
            int i = 0;

            int msLastIx = iMaster.Length - 1;
            int slLastIx = iSlave.Length - 1;
            var endsCount = 0;

            while (i < n) {
                int msIx0 = masterIndices[i];
                int msIx1 = msIx0 < msLastIx ? msIx0 + 1 : 0;

                var ms0 = iMaster[msIx0];
                var ms1 = iMaster[msIx1];

                var j = i;

                do {
                    int slIx0 = slaveIndices[j];
                    int slIx1 = slIx0 < slLastIx ? slIx0 + 1 : 0;

                    var sl0 = iSlave[slIx0];
                    var sl1 = iSlave[slIx1];

                    var crossType = CrossResolver.DefineType(ms0, ms1, sl0, sl1, out var point);

                    // .not_cross, .pure are the most possible cases (more then 99%)

                    j += 1;

                    int prevMs;
                    int nextMs;
                    int prevSl;
                    int nextSl;
                    PinPoint pinPoint;
                    PinPoint.Def pinPointDef;

                    switch (crossType) {
                        case CrossType.not_cross:
                        case CrossType.same_line:
                            continue;
                        case CrossType.pure:
                            // simple intersection and most common case

                            pinPointDef = new PinPoint.Def(
                                point,
                                ms0,
                                ms1,
                                sl0,
                                sl1,
                                new PathMileStone(msIx0, ms0.SqrDistance(point)),
                                new PathMileStone(slIx0, sl0.SqrDistance(point))
                            );

                            pinPoint = PinPoint.BuildSimple(pinPointDef);

                            pinPoints.Add(pinPoint);
                            break;
                        case CrossType.end_a0:
                            prevMs = (msIx0 - 1 + masterCount) % masterCount;
                            nextMs = msIx1;

                            prevSl = slIx0;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0),
                                new PathMileStone(slIx0, sl0.SqrDistance(point))
                            );

                            pinPoint = PinPoint.BuildOnSlave(pinPointDef, iGeom);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a1:
                            prevMs = msIx0;
                            nextMs = (msIx1 + 1) % masterCount;

                            prevSl = slIx0;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx1),
                                new PathMileStone(slIx0, sl0.SqrDistance(point))
                            );

                            pinPoint = PinPoint.BuildOnSlave(pinPointDef, iGeom);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_b0:
                            prevMs = msIx0;
                            nextMs = msIx1;

                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0, ms0.SqrDistance(point)),
                                new PathMileStone(slIx0)
                            );

                            pinPoint = PinPoint.BuildOnMaster(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_b1:
                            prevMs = msIx0;
                            nextMs = msIx1;

                            prevSl = slIx0;
                            nextSl = (slIx1 + 1) % slaveCount;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0, ms0.SqrDistance(point)),
                                new PathMileStone(slIx1)
                            );

                            pinPoint = PinPoint.BuildOnMaster(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a0_b0:
                            prevMs = (msIx0 - 1 + masterCount) % masterCount;
                            nextMs = msIx1;

                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0),
                                new PathMileStone(slIx0)
                            );

                            pinPoint = PinPoint.BuildOnCross(pinPointDef, iGeom);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a0_b1:
                            prevMs = (msIx0 - 1 + masterCount) % masterCount;
                            nextMs = msIx1;

                            prevSl = slIx0;
                            nextSl = (slIx1 + 1) % slaveCount;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0),
                                new PathMileStone(slIx1)
                            );

                            pinPoint = PinPoint.BuildOnCross(pinPointDef, iGeom);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a1_b0:
                            prevMs = msIx0;
                            nextMs = (msIx1 + 1) % masterCount;

                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx1),
                                new PathMileStone(slIx0)
                            );

                            pinPoint = PinPoint.BuildOnCross(pinPointDef, iGeom);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a1_b1:
                            prevMs = msIx0;
                            nextMs = (msIx1 + 1) % masterCount;

                            prevSl = slIx0;
                            nextSl = (slIx1 + 1) % slaveCount;

                            pinPointDef = new PinPoint.Def(
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx1),
                                new PathMileStone(slIx1)
                            );

                            pinPoint = PinPoint.BuildOnCross(pinPointDef, iGeom);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                    }
                } while (j < n && msIx0 == masterIndices[j]);

                i = j;
            }

            DynamicArray<PinPath> pinPaths;
            var hasExclusion = false;
            if (endsCount > 0) {
                pinPaths = Organize(ref pinPoints, masterCount, slaveCount, Allocator.Temp);
                if (pinPaths.Count > 0) {
                    // test for same shapes
                    if (pinPaths.Count == 1 && iMaster.Length == iSlave.Length && pinPaths[0].isClosed) {
                        return new PinNavigator(true);
                    }

                    hasExclusion = RemoveExclusion(ref pinPaths, exclusionPinType);
                }
            } else {
                pinPaths = new DynamicArray<PinPath>(0, Allocator.Temp);
            }

            if (pinPoints.Count > 0) {
                bool pinExclusion = RemoveExclusion(ref pinPoints, exclusionPinType);
                hasExclusion = pinExclusion || hasExclusion;
            }

            var navigator = BuildNavigator(ref pinPoints, ref pinPaths, iMaster.Length, hasExclusion);

            return navigator;
        }

        private static AdjacencyMatrix CreatePossibilityMatrix(NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var slaveBoxArea = new Util.Rect(long.MaxValue, long.MaxValue, long.MinValue, long.MinValue);

            var slaveSegmentsBoxAreas = new DynamicArray<Util.Rect>(8, Allocator.Temp);

            int lastSlaveIndex = slave.Length - 1;

            for (int i = 0; i <= lastSlaveIndex; ++i) {
                var a = slave[i];
                var b = slave[i != lastSlaveIndex ? i + 1 : 0];

                slaveBoxArea.Assimilate(a);
                slaveSegmentsBoxAreas.Add(new Util.Rect(a, b));
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

                var segmentBoxArea = new Util.Rect(master_0, master_1);
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


        private static DynamicArray<PinPath> Organize(ref DynamicArray<PinPoint> pinPoints, int masterCount, int slaveCount, Allocator allocator) {
            pinPoints.SortByMaster();
            
            RemoveDoubles(ref pinPoints);

            if (pinPoints.Count > 1) {
                return FindEdges(ref pinPoints, masterCount, slaveCount, allocator);
            }

            return new DynamicArray<PinPath>(0, allocator);
        }

        private static void RemoveDoubles(ref DynamicArray<PinPoint> pinPoints) {
            int n = pinPoints.Count;
            var a = pinPoints[0];
            var i = 1;
            var removeIndex = new DynamicArray<int>(n >> 1, Allocator.Temp);
            while (i < n) {
                var b = pinPoints[i];
                if (a == b) {
                    removeIndex.Add(i);
                }

                a = b;
                i += 1;
            }

            if (removeIndex.Count != 0) {
                var j = removeIndex.Count - 1;
                while (j >= 0) {
                    pinPoints.RemoveAt(removeIndex[j]);
                    j -= 1;
                }
            }
        }

        private static DynamicArray<PinPath> FindEdges(ref DynamicArray<PinPoint> pinPoints, int masterCount, int slaveCount, Allocator allocator) {
            var edges = new DynamicArray<PinEdge>(8, Allocator.Temp);
            int n = pinPoints.Count;

            var isPrevEdge = false;

            int i = 0;
            int j = n - 1;

            var a = pinPoints[j];
            var removeMark = new NativeArray<bool>(n, Allocator.Temp);

            while (i < n) {
                var b = pinPoints[i];

                int aMi = a.masterMileStone.index;
                int bMi = b.masterMileStone.index;

                bool isSameMaster = aMi == bMi || b.masterMileStone.offset == 0 && (aMi + 1) % masterCount == bMi;

                if (isSameMaster &&
                    CrossDetector.IsDirect(a.masterMileStone, b.masterMileStone, masterCount) &&
                    CrossDetector.Same(a.slaveMileStone, b.slaveMileStone, slaveCount)) {
                    if (isPrevEdge) {
                        var prevEdge = edges[edges.Count - 1];
                        prevEdge.v1 = b;
                        edges[edges.Count - 1] = prevEdge;
                    } else {
                        bool isDirectSlave =
                            CrossDetector.IsDirect(a.slaveMileStone, b.slaveMileStone, slaveCount);
                        edges.Add(new PinEdge(a, b, isDirectSlave));
                    }

                    removeMark[i] = true;
                    removeMark[j] = true;
                    isPrevEdge = true;
                } else {
                    isPrevEdge = false;
                }

                a = b;
                j = i;
                i += 1;
            }

            DynamicArray<PinPath> pinPaths;

            if (edges.Count > 0) {
                i = n - 1;
                while (i >= 0) {
                    if (removeMark[i]) {
                        pinPoints.RemoveAt(i);
                    }

                    i -= 1;
                }

                if (edges.Count > 1) {
                    var first = edges[0];
                    var last = edges[edges.Count - 1];
                    if (first.v0 == last.v1) {
                        first.v0 = last.v0;
                        edges[0] = first;
                        edges.RemoveLast();
                    }
                }

                pinPaths = new DynamicArray<PinPath>(edges.Count, allocator);

                for (int k = 0; k < edges.Count; ++k) {
                    pinPaths.Add(new PinPath(edges[k]));
                }
            } else {
                pinPaths = new DynamicArray<PinPath>(0, allocator);
            }

            return pinPaths;
        }

        private static bool Same(PathMileStone a, PathMileStone b, int module) {
            if (a.index == b.index) {
                return true;
            }

            if (b.offset == 0 && (a.index + 1) % module == b.index) {
                return true;
            }

            if (a.offset == 0 && (b.index + 1) % module == a.index) {
                return true;
            }

            return false;
        }

        private static bool IsDirect(PathMileStone a, PathMileStone b, int module) {
            if (a.index == b.index) {
                return a.offset < b.offset;
            }

            return b.offset == 0 && (a.index + 1) % module == b.index;
        }

        private static bool RemoveExclusion(ref DynamicArray<PinPoint> pinPoints, PinPoint.PinType exclusion) {
            var i = pinPoints.Count - 1;
            var result = false;
            do {
                var pin = pinPoints[i];
                if (pin.type == exclusion) {
                    pinPoints.RemoveAt(i);
                    result = true;
                }

                i -= 1;
            } while (i >= 0);

            return result;
        }

        private static bool RemoveExclusion(ref DynamicArray<PinPath> pinPaths, PinPoint.PinType exclusion) {
            var i = pinPaths.Count - 1;
            var result = false;
            do {
                var path = pinPaths[i];
                if (path.v0.type == exclusion) {
                    pinPaths.RemoveAt(i);
                    result = true;
                }

                i -= 1;
            } while (i >= 0);

            return result;
        }

        /// Build  Navigator section
        private static PinNavigator BuildNavigator(ref DynamicArray<PinPoint> pinPointArray, ref DynamicArray<PinPath> pinPathArray, int masterCount, bool hasExclusion) {
            var handlerArray = new DynamicArray<PinHandler>(pinPathArray.Count + pinPointArray.Count, Allocator.Temp);
            int i = 0;
            while (i < pinPathArray.Count) {
                var path = pinPathArray[i];
                var pathHandlers = path.Extract(i, masterCount, Allocator.Temp);
                handlerArray.Add(pathHandlers);

                i += 1;
            }

            i = 0;
            while (i < pinPointArray.Count) {
                handlerArray.Add(new PinHandler(pinPointArray[i], i));
                i += 1;
            }

            bool hasContacts = hasExclusion || handlerArray.Count > 0;

            if (handlerArray.Count == 0) {
                return new PinNavigator(
                    new NativeArray<int>(0, Allocator.Temp),
                    new NativeArray<PinPath>(0, Allocator.Temp),
                    new NativeArray<PinPoint>(0, Allocator.Temp),
                    new NativeArray<PinNode>(0, Allocator.Temp),
                    hasContacts
                );
            }

            if (pinPathArray.Count > 0) {
                Compact(ref handlerArray, ref pinPointArray, ref pinPathArray);
            }

            if (handlerArray.Count == 0) {
                return new PinNavigator(
                    new NativeArray<int>(0, Allocator.Temp),
                    new NativeArray<PinPath>(0, Allocator.Temp),
                    new NativeArray<PinPoint>(0, Allocator.Temp),
                    new NativeArray<PinNode>(0, Allocator.Temp),
                    hasContacts
                );
            }

            var slavePath = Streamline(ref handlerArray, pinPointArray, pinPathArray);

            int n = slavePath.Length;

            var nodes = new NativeArray<PinNode>(n, Allocator.Temp);
            for (int j = 0; j < n; ++j) {
                var node = nodes[j];
                var handler = handlerArray[j];
                node.isPinPath = handler.isPinPath;
                node.index = handler.index;
                node.masterIndex = j;
                nodes[j] = node;

                var slaveIndex = slavePath[j];
                node = nodes[slaveIndex];
                node.slaveIndex = j;
                nodes[slaveIndex] = node;
            }

            return new PinNavigator(slavePath, pinPathArray.Convert(), pinPointArray.Convert(), nodes, hasContacts);
        }

        private static void Compact(ref DynamicArray<PinHandler> handlerArray, ref DynamicArray<PinPoint> pinPointArray, ref DynamicArray<PinPath> pinPathArray) {
            
            var paths = new DynamicArray<PinPath>(pinPathArray.Count, Allocator.Temp);
            var points = new DynamicArray<PinPoint>(pinPointArray.Count, Allocator.Temp);
            var handlers = new DynamicArray<PinHandler>(Allocator.Temp);

            int n = handlerArray.Count;
            for (int i = 0; i < n; ++i) {
                var pinHandler = handlerArray[i];
                if (pinHandler.marker) {
                    int index = pinHandler.index;
                    if (pinHandler.isPinPath) {
                        var path = pinPathArray[index];
                        paths.Add(path);

                        var handler = new PinHandler(pinHandler.masterSortFactor, paths.Count - 1, true, pinHandler.type);
                        handlers.Add(handler);
                    } else {
                        var pin = pinPointArray[index];
                        points.Add(pin);

                        var handler = new PinHandler(pinHandler.masterSortFactor, points.Count - 1, false, pinHandler.type);
                        handlers.Add(handler);
                    }
                }
            }

            pinPathArray = paths;
            pinPointArray = points;
            handlerArray = handlers;
        }

        private static NativeArray<int> Streamline(ref DynamicArray<PinHandler> handlerArray, DynamicArray<PinPoint> pinPointArray, DynamicArray<PinPath> pinPathArray) {
            handlerArray.SortByMaster();
            
            int n = handlerArray.Count;

            var iStones = new NativeArray<IndexMileStone>(n, Allocator.Temp);

            for (int j = 0; j < n; ++j) {
                var handler = handlerArray[j];
                int index = handler.index;
                if (!handler.isPinPath) {
                    var point = pinPointArray[index];
                    iStones[j] = new IndexMileStone(j, point.slaveMileStone);
                } else {
                    var path = pinPathArray[index];
                    iStones[j] = new IndexMileStone(j, path.v0.slaveMileStone);
                }
            }
            
            iStones.Sort();

            var indexArray = new NativeArray<int>(n, Allocator.Temp);

            for (int j = 0; j < n; ++j) {
                indexArray[j] = iStones[j].index;
            }

            return indexArray;
        }
    }

}