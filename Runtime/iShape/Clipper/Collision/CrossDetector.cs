using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Sort;
using iShape.Clipper.Util;
using iShape.Geometry;
using iShape.Collections;
using Unity.Collections;

namespace iShape.Clipper.Collision {

    public struct CrossDetector {
        private struct AdjacencyMatrix {
        
            internal NativeArray<int> masterIndices;
            internal NativeArray<int> slaveIndices;
            internal readonly Rect masterBox;
            internal readonly Rect slaveBox;
        
            internal AdjacencyMatrix(NativeArray<int> masterIndices, NativeArray<int> slaveIndices, Rect masterBox, Rect slaveBox) {
                this.masterIndices = masterIndices;
                this.slaveIndices = slaveIndices;
                this.masterBox = masterBox;
                this.slaveBox = slaveBox;
            }
        }
        
        public static PinNavigator FindPins(NativeArray<IntVector> iMaster, NativeArray<IntVector> iSlave, PinPoint.PinType exclusionPinType, Allocator allocator) {
            var posMatrix = CreatePossibilityMatrix(iMaster, iSlave, allocator);

            var masterIndices = posMatrix.masterIndices;
            var slaveIndices = posMatrix.slaveIndices;

            var pinPoints = new DynamicArray<PinPoint>(0, allocator);

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

                    var crossType = CrossResolver.DefineType(ms0, ms1, sl0, sl1, out var point, out var dp);

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
                            continue;
                        case CrossType.same_line:
                            if (sl1 == ms0) {
                                var pin = new PinPoint(ms0, PinPoint.PinType.nil, new PathMileStone(msIx0), new PathMileStone(slIx1));
                                pinPoints.Add(pin);
                            } else {
                                if (Edge.IsInRect(ms0, ms1, sl1)) {
                                    var pin = new PinPoint(sl1, PinPoint.PinType.nil, new PathMileStone(msIx0, ms0.SqrDistance(sl1)), new PathMileStone(slIx1));
                                    pinPoints.Add(pin);
                                }
                                if (Edge.IsInRect(sl0, sl1, ms0)) {
                                    var pin = new PinPoint(ms0, PinPoint.PinType.nil, new PathMileStone(msIx0), new PathMileStone(slIx0, sl0.SqrDistance(ms0)));
                                    pinPoints.Add(pin);
                                }
                            }
                            break;
                        case CrossType.pure:
                            // simple intersection and most common case

                            pinPointDef = new PinPoint.Def(
                                dp,
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
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0),
                                new PathMileStone(slIx0, sl0.SqrDistance(point))
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a1:
                            prevMs = msIx0;
                            nextMs = (msIx1 + 1) % masterCount;

                            prevSl = slIx0;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx1),
                                new PathMileStone(slIx0, sl0.SqrDistance(point))
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_b0:
                            prevMs = msIx0;
                            nextMs = msIx1;

                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0, ms0.SqrDistance(point)),
                                new PathMileStone(slIx0)
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_b1:
                            prevMs = msIx0;
                            nextMs = msIx1;

                            prevSl = slIx0;
                            nextSl = (slIx1 + 1) % slaveCount;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0, ms0.SqrDistance(point)),
                                new PathMileStone(slIx1)
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a0_b0:
                            prevMs = (msIx0 - 1 + masterCount) % masterCount;
                            nextMs = msIx1;

                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0),
                                new PathMileStone(slIx0)
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a0_b1:
                            prevMs = (msIx0 - 1 + masterCount) % masterCount;
                            nextMs = msIx1;

                            prevSl = slIx0;
                            nextSl = (slIx1 + 1) % slaveCount;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx0),
                                new PathMileStone(slIx1)
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a1_b0:
                            prevMs = msIx0;
                            nextMs = (msIx1 + 1) % masterCount;

                            prevSl = (slIx0 - 1 + slaveCount) % slaveCount;
                            nextSl = slIx1;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx1),
                                new PathMileStone(slIx0)
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                        case CrossType.end_a1_b1:
                            prevMs = msIx0;
                            nextMs = (msIx1 + 1) % masterCount;

                            prevSl = slIx0;
                            nextSl = (slIx1 + 1) % slaveCount;

                            pinPointDef = new PinPoint.Def(
                                dp,
                                point,
                                iMaster[prevMs],
                                iMaster[nextMs],
                                iSlave[prevSl],
                                iSlave[nextSl],
                                new PathMileStone(msIx1),
                                new PathMileStone(slIx1)
                            );

                            pinPoint = PinPoint.BuildOnSide(pinPointDef);
                            pinPoints.Add(pinPoint);
                            endsCount += 1;
                            break;
                    }
                } while (j < n && msIx0 == masterIndices[j]);

                i = j;
            }

            masterIndices.Dispose();
            slaveIndices.Dispose();

            DynamicArray<PinPath> pinPaths;
            var hasExclusion = false;
            if (endsCount > 0) {
                pinPaths = Organize(ref pinPoints, masterCount, slaveCount, allocator);
                if (pinPaths.Count > 0) {
                    // test for same shapes
                    if (pinPaths.Count == 1 && iMaster.Length == iSlave.Length && pinPaths[0].isClosed) {
                        
                        pinPoints.Dispose();
                        pinPaths.Dispose();
                        
                        return new PinNavigator(true);
                    }

                    hasExclusion = RemoveExclusion(ref pinPaths, exclusionPinType);
                }
            } else {
                pinPaths = new DynamicArray<PinPath>(0, allocator);
            }

            if (pinPoints.Count > 0) {
                bool pinExclusion = RemoveExclusion(ref pinPoints, exclusionPinType);
                hasExclusion = pinExclusion || hasExclusion;
            }

            var navigator = BuildNavigator(ref pinPoints, ref pinPaths, iMaster.Length, hasExclusion, posMatrix.masterBox, posMatrix.slaveBox, allocator);

            pinPoints.Dispose();
            pinPaths.Dispose();
            
            return navigator;
        }

        private static AdjacencyMatrix CreatePossibilityMatrix(NativeArray<IntVector> master, NativeArray<IntVector> slave, Allocator allocator) {
            var slaveBox = new Rect(long.MaxValue, long.MaxValue, long.MinValue, long.MinValue);
            var masterBox = new Rect(long.MaxValue, long.MaxValue, long.MinValue, long.MinValue);

            var slaveSegmentsBoxAreas = new DynamicArray<Rect>(8, allocator);

            int lastSlaveIndex = slave.Length - 1;

            for (int i = 0; i <= lastSlaveIndex; ++i) {
                var a = slave[i];
                var b = slave[i != lastSlaveIndex ? i + 1 : 0];

                slaveBox.Assimilate(a);
                slaveSegmentsBoxAreas.Add(new Rect(a, b));
            }

            // var posMatrix = new AdjacencyMatrix(0, allocator);

            var masterIndices = new DynamicArray<int>(Allocator.Temp);
            var slaveIndices = new DynamicArray<int>(Allocator.Temp);
            
            int lastMasterIndex = master.Length - 1;

            for (int i = 0; i <= lastMasterIndex; ++i) {
                var master_0 = master[i];
                var master_1 = master[i != lastMasterIndex ? i + 1 : 0];
                masterBox.Assimilate(master_0);

                bool isIntersectionImpossible = slaveBox.IsNotIntersecting(master_0, master_1);

                if (isIntersectionImpossible) {
                    continue;
                }

                var segmentBoxArea = new Rect(master_0, master_1);
                for (int j = 0; j <= lastSlaveIndex; ++j) {
                    bool isIntersectionPossible = slaveSegmentsBoxAreas[j].IsIntersecting(segmentBoxArea);

                    if (isIntersectionPossible) {
                        masterIndices.Add(i);
                        slaveIndices.Add(j);
                    }
                }
            }

            slaveSegmentsBoxAreas.Dispose();

            return new AdjacencyMatrix(masterIndices.Convert(), slaveIndices.Convert(), masterBox, slaveBox);
        }


        private static DynamicArray<PinPath> Organize(ref DynamicArray<PinPoint> pinPoints, int masterCount, int slaveCount, Allocator allocator) {
            pinPoints.SortByMaster();
            
            RemoveDoubles(ref pinPoints, allocator);

            if (pinPoints.Count > 1) {
                return FindEdges(ref pinPoints, masterCount, slaveCount, allocator);
            }

            return new DynamicArray<PinPath>(0, allocator);
        }

        private static void RemoveDoubles(ref DynamicArray<PinPoint> pinPoints, Allocator allocator) {
            int n = pinPoints.Count;
            var a = pinPoints[0];
            var i = 1;
            var removeIndex = new DynamicArray<int>(n >> 1, allocator);
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
            
            removeIndex.Dispose();
        }

        private static DynamicArray<PinPath> FindEdges(ref DynamicArray<PinPoint> pinPoints, int masterCount, int slaveCount, Allocator allocator) {
            var edges = new DynamicArray<PinEdge>(8, allocator);
            int n = pinPoints.Count;

            var isPrevEdge = false;

            int i = 0;
            int j = n - 1;

            var a = pinPoints[j];
            var removeMark = new NativeArray<bool>(n, allocator);

            while (i < n) {
                var b = pinPoints[i];

                int aMi = a.masterMileStone.index;
                int bMi = b.masterMileStone.index;

                bool isSameMaster = aMi == bMi || b.masterMileStone.offset == 0 && (aMi + 1) % masterCount == bMi;

                if (isSameMaster &&
                    IsDirect(a.masterMileStone, b.masterMileStone, masterCount) &&
                    Same(a.slaveMileStone, b.slaveMileStone, slaveCount)) {
                    if (isPrevEdge) {
                        var prevEdge = edges[edges.Count - 1];
                        prevEdge.v1 = b;
                        edges[edges.Count - 1] = prevEdge;
                    } else {
                        bool isDirectSlave = IsDirect(a.slaveMileStone, b.slaveMileStone, slaveCount);
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

            edges.Dispose();
            removeMark.Dispose();

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
        private static PinNavigator BuildNavigator(ref DynamicArray<PinPoint> pinPointArray, ref DynamicArray<PinPath> pinPathArray, int masterCount, bool hasExclusion, Rect masterBox, Rect slaveBox, Allocator allocator) {
            var handlerArray = new DynamicArray<PinHandler>(pinPathArray.Count + pinPointArray.Count, allocator);
            int i = 0;
            while (i < pinPathArray.Count) {
                var path = pinPathArray[i];
                var pathHandlers = path.Extract(i, masterCount, allocator);
                handlerArray.Add(pathHandlers);
                pathHandlers.Dispose();
                
                i += 1;
            }

            i = 0;
            while (i < pinPointArray.Count) {
                handlerArray.Add(new PinHandler(pinPointArray[i], i));
                i += 1;
            }

            bool hasContacts = hasExclusion || handlerArray.Count > 0;

            if (handlerArray.Count == 0) {
                handlerArray.Dispose();
                return new PinNavigator(
                    new NativeArray<int>(0, allocator),
                    new NativeArray<PinPath>(0, allocator),
                    new NativeArray<PinPoint>(0, allocator),
                    new NativeArray<PinNode>(0, allocator),
                    hasContacts,
                    masterBox,
                    slaveBox
                );
            }

            if (pinPathArray.Count > 0) {
                Compact(ref handlerArray, ref pinPointArray, ref pinPathArray, allocator);
            }

            if (handlerArray.Count == 0) {
                handlerArray.Dispose();
                return new PinNavigator(
                    new NativeArray<int>(0, allocator),
                    new NativeArray<PinPath>(0, allocator),
                    new NativeArray<PinPoint>(0, allocator),
                    new NativeArray<PinNode>(0, allocator),
                    hasContacts,
                    masterBox,
                    slaveBox
                );
            }

            var slavePath = Streamline(ref handlerArray, pinPointArray, pinPathArray, allocator);

            int n = slavePath.Length;

            var nodes = new NativeArray<PinNode>(n, allocator);
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
            
            handlerArray.Dispose();

            return new PinNavigator(
                slavePath,
                pinPathArray.ToArray(allocator),
                pinPointArray.ToArray(allocator),
                nodes,
                hasContacts,
                masterBox,
                slaveBox
                );
        }

        private static void Compact(ref DynamicArray<PinHandler> handlerArray, ref DynamicArray<PinPoint> pinPointArray, ref DynamicArray<PinPath> pinPathArray, Allocator allocator) {
            
            var paths = new DynamicArray<PinPath>(pinPathArray.Count, allocator);
            var points = new DynamicArray<PinPoint>(pinPointArray.Count, allocator);
            var handlers = new DynamicArray<PinHandler>(allocator);

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

            pinPathArray.Dispose();
            pinPointArray.Dispose();
            handlerArray.Dispose();
            
            pinPathArray = paths;
            pinPointArray = points;
            handlerArray = handlers;
        }

        private static NativeArray<int> Streamline(ref DynamicArray<PinHandler> handlerArray, DynamicArray<PinPoint> pinPointArray, DynamicArray<PinPath> pinPathArray, Allocator allocator) {
            handlerArray.SortByMaster();
            
            int n = handlerArray.Count;

            var iStones = new NativeArray<IndexMileStone>(n, allocator);

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

            var indexArray = new NativeArray<int>(n, allocator);

            for (int j = 0; j < n; ++j) {
                indexArray[j] = iStones[j].index;
            }

            iStones.Dispose();

            return indexArray;
        }
    }

}