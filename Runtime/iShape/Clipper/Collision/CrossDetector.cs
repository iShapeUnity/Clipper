using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Collision.Navigation;
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
            let msIx0 = masterIndices[i]
            let msIx1 = msIx0 < msLastIx ? msIx0 + 1 : 0
            
            let ms0 = iMaster[msIx0]
            let ms1 = iMaster[msIx1]
            
            var j = i
            
            repeat {
                let slIx0 = slaveIndices[j]
                let slIx1 = slIx0 < slLastIx ? slIx0 + 1 : 0
                
                let sl0 = iSlave[slIx0]
                let sl1 = iSlave[slIx1]
                
                var point: IntPoint = .zero
                let crossType = CrossResolver.defineType(a0: ms0, a1: ms1, b0: sl0, b1: sl1, cross: &point)
                
                // .not_cross, .pure are the most possible cases (more then 99%)
                
                j += 1
                
                switch crossType {
                case .not_cross, .same_line:
                    continue
                case .pure:
                    // simple intersection and most common case
                    
                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: ms0,
                        ms1: ms1,
                        sl0: sl0,
                        sl1: sl1,
                        masterMileStone: PathMileStone(index: msIx0, offset: ms0.sqrDistance(point: point)),
                        slaveMileStone: PathMileStone(index: slIx0, offset: sl0.sqrDistance(point: point))
                    )
                    
                    let pinPoint = PinPoint.buildSimple(def: pinPointDef)

                    pinPoints.append(pinPoint)
                case .end_a0:
                    let prevMs = (msIx0 - 1 + masterCount) % masterCount
                    let nextMs = msIx1
                    
                    let prevSl = slIx0
                    let nextSl = slIx1

                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx0),
                        slaveMileStone: PathMileStone(index: slIx0, offset: sl0.sqrDistance(point: point))
                    )

                    let pinPoint = PinPoint.buildOnSlave(def: pinPointDef, iGeom: iGeom)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_a1:
                    let prevMs = msIx0
                    let nextMs = (msIx1 + 1) % masterCount
                    
                    let prevSl = slIx0
                    let nextSl = slIx1

                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx1),
                        slaveMileStone: PathMileStone(index: slIx0, offset: sl0.sqrDistance(point: point))
                    )

                    let pinPoint = PinPoint.buildOnSlave(def: pinPointDef, iGeom: iGeom)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_b0:
                    let prevMs = msIx0
                    let nextMs = msIx1
                    
                    let prevSl = (slIx0 - 1 + slaveCount) % slaveCount
                    let nextSl = slIx1

                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx0, offset: ms0.sqrDistance(point: point)),
                        slaveMileStone: PathMileStone(index: slIx0)
                    )
                    
                    let pinPoint = PinPoint.buildOnMaster(def: pinPointDef)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_b1:
                    let prevMs = msIx0
                    let nextMs = msIx1
                    
                    let prevSl = slIx0
                    let nextSl = (slIx1 + 1) % slaveCount

                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx0, offset: ms0.sqrDistance(point: point)),
                        slaveMileStone: PathMileStone(index: slIx1)
                    )
                    
                    let pinPoint = PinPoint.buildOnMaster(def: pinPointDef)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_a0_b0:
                    let prevMs = (msIx0 - 1 + masterCount) % masterCount
                    let nextMs = msIx1

                    let prevSl = (slIx0 - 1 + slaveCount) % slaveCount
                    let nextSl = slIx1
                    
                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx0),
                        slaveMileStone: PathMileStone(index: slIx0)
                    )
                    
                    let pinPoint = PinPoint.buildOnCross(def: pinPointDef, iGeom: iGeom)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_a0_b1:
                    let prevMs = (msIx0 - 1 + masterCount) % masterCount
                    let nextMs = msIx1

                    let prevSl = slIx0
                    let nextSl = (slIx1 + 1) % slaveCount
                    
                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx0),
                        slaveMileStone: PathMileStone(index: slIx1)
                    )
                    
                    let pinPoint = PinPoint.buildOnCross(def: pinPointDef, iGeom: iGeom)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_a1_b0:
                    let prevMs = msIx0
                    let nextMs = (msIx1 + 1) % masterCount

                    let prevSl = (slIx0 - 1 + slaveCount) % slaveCount
                    let nextSl = slIx1
                    
                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx1),
                        slaveMileStone: PathMileStone(index: slIx0)
                    )
                    
                    let pinPoint = PinPoint.buildOnCross(def: pinPointDef, iGeom: iGeom)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                case .end_a1_b1:
                    let prevMs = msIx0
                    let nextMs = (msIx1 + 1) % masterCount

                    let prevSl = slIx0
                    let nextSl = (slIx1 + 1) % slaveCount
                    
                    let pinPointDef = PinPoint.Def(
                        pt: point,
                        ms0: iMaster[prevMs],
                        ms1: iMaster[nextMs],
                        sl0: iSlave[prevSl],
                        sl1: iSlave[nextSl],
                        masterMileStone: PathMileStone(index: msIx1),
                        slaveMileStone: PathMileStone(index: slIx1)
                    )
                    
                    let pinPoint = PinPoint.buildOnCross(def: pinPointDef, iGeom: iGeom)
                    pinPoints.append(pinPoint)
                    endsCount += 1
                }
            } while j < n && msIx0 == masterIndices[j]
            
            i = j
        }
        
        var pinPaths: [PinPath]
        var hasExclusion = false
        if endsCount > 0 {
            pinPaths = CrossDetector.organize(pinPoints: &pinPoints, masterCount: masterCount, slaveCount: slaveCount)
            if !pinPaths.isEmpty {
                
                // test for same shapes
                if pinPaths.count == 1 && iMaster.count == iSlave.count && pinPaths[0].isClosed {
                    return PinNavigator()
                }
                
                hasExclusion = CrossDetector.removeExclusion(pinPaths: &pinPaths, exclusion: exclusionPinType)
            }
        } else {
            pinPaths = []
        }
        
        if !pinPoints.isEmpty {
            let pinExclusion = CrossDetector.removeExclusion(pinPoints: &pinPoints, exclusion: exclusionPinType)
            hasExclusion = pinExclusion || hasExclusion
        }

        let navigator = CrossDetector.buildNavigator(pinPointArray: &pinPoints, pinPathArray: &pinPaths, masterCount: iMaster.count, hasExclusion: hasExclusion)
        
        return navigator
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


    private static DynamicArray<PinPath> Organize(ref DynamicArray<PinPoint> pinPoints, int masterCount, int slaveCount, Allocator allocator) {
        /*
        pinPoints.sort(by: { a, b in
            if a.masterMileStone.index != b.masterMileStone.index {
                return a.masterMileStone.index < b.masterMileStone.index
            }

            return a.masterMileStone.offset < b.masterMileStone.offset
        })
        */
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
                    edges.Add(new PinEdge(a,  b, isDirectSlave));
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
        if (a.index != b.index) {
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
    }
}