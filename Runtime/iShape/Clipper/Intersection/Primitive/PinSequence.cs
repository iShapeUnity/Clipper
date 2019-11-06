using iShape.Clipper.Intersection.Navigation;
using iShape.Support;
using Unity.Collections;

namespace iShape.Clipper.Intersection.Primitive {
    
    internal struct PinSequence {
        
        private NativeArray<PinPath> pinPathArray;
        private NativeArray<PinPoint> pinPointArray;
        private DynamicArray<PinHandler> handlerArray;

        private readonly int masterCount;
        private readonly Allocator allocator;


        internal PinSequence(
            NativeArray<PinPoint> pinPointArray,
            NativeArray<PinPath> pinPathArray,
            int masterCount,
            Allocator allocator
            ) {
            this.pinPointArray = pinPointArray;
            this.pinPathArray = pinPathArray;
            this.masterCount = masterCount;
            this.handlerArray = new DynamicArray<PinHandler>(pinPathArray.Length + pinPointArray.Length, allocator);
            this.allocator = allocator;
        }


        internal PinNavigator Convert(PinPoint.PinType exclusionPinType, bool hasExclusion) {
            int i = 0;
            while (i < pinPathArray.Length) {
                var path = pinPathArray[i];

                var pathHandlers = path.Extract(i, masterCount, allocator);

                handlerArray.Add(pathHandlers);

                i += 1;
            }

            i = 0;
            while (i < pinPointArray.Length) {
                handlerArray.Add(new PinHandler(pinPointArray[i], i));
                i += 1;
            }

            bool hasContacts = hasExclusion || handlerArray.Count != 0;

            if (handlerArray.Count == 0) {
                return new PinNavigator(
                    new NativeArray<int>(0, allocator),
                    new NativeArray<PinPath>(0, allocator),
                    new NativeArray<PinPoint>(0, allocator),
                    new NativeArray<PinNode>(0, allocator),
                    hasContacts
                );
            }

            this.SortMaster();
            this.CleanDoubles(exclusionPinType);

            if (handlerArray.Count == 0) {
                return new PinNavigator(
                    new NativeArray<int>(0, allocator),
                    new NativeArray<PinPath>(0, allocator),
                    new NativeArray<PinPoint>(0, allocator),
                    new NativeArray<PinNode>(0, allocator),
                    hasContacts
                );
            }

            var slavePath = this.BuildSlavePath();

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


            return new PinNavigator(slavePath, pinPathArray, pinPointArray, nodes, hasContacts);
        }


        internal void Dispose() {
            pinPathArray.Dispose();
            pinPointArray.Dispose();
            handlerArray.Dispose();
        }


        private void SortMaster() {
            // this array is mostly sorted

            var n = handlerArray.Count;

            bool isNotSorted;
            var m = n;

            do {
                isNotSorted = false;
                var a = handlerArray[0];
                int i = 1;
                while (i < m) {
                    var b = handlerArray[i];
                    if (PathMileStone.Compare(a.masterSortFactor, b.masterSortFactor)) {
                        handlerArray[i - 1] = b;
                        isNotSorted = true;
                    } else {
                        handlerArray[i - 1] = a;
                        a = b;
                    }

                    i += 1;
                }

                m -= 1;
                handlerArray[m] = a;
            } while (isNotSorted);
        }


        private void CleanDoubles(PinPoint.PinType exclusionPinType) {
            var i = 1;
            var prevIndex = 0;
            var prev = handlerArray[prevIndex];
            var isCompactRequired = pinPathArray.Length > 0;
            while (i < handlerArray.Count) {
                var handler = handlerArray[i];
                if (handler != prev) {
                    prev = handler;
                    prevIndex = i;
                } else {
                    isCompactRequired = true;
                    if (handler.isPinPath == 0) {
                        handler.marker = 1;
                        handlerArray[i] = handler;
                    } else {
                        prev.marker = 1;
                        handlerArray[prevIndex] = prev;
                        prev = handler;
                        prevIndex = i;
                    }
                }

                i += 1;
            }

            i = 0;
            while (i < handlerArray.Count) {
                var handler = handlerArray[i];
                if (handler.marker == 0 && handler.type == exclusionPinType) {
                    handler.marker = 1;
                    handlerArray[i] = handler;
                    isCompactRequired = true;
                }

                i += 1;
            }

            if (isCompactRequired) {
                this.Compact();
            }
        }


        private void Compact() {
            var paths = new DynamicArray<PinPath>(this.pinPathArray.Length, Allocator.Temp);
            var points = new DynamicArray<PinPoint>(this.pinPathArray.Length, Allocator.Temp);
            var handlers = new DynamicArray<PinHandler>(0, allocator);

            int n = this.handlerArray.Count;
            for (int i = 0; i < n; ++i) {
                var pinHandler = handlerArray[i];
                if (pinHandler.marker == 0) {
                    var index = pinHandler.index;
                    if (pinHandler.isPinPath == 1) {
                        var path = this.pinPathArray[index];
                        paths.Add(path);

                        var handler = new PinHandler(pinHandler.masterSortFactor, paths.Count - 1, 1, pinHandler.type);
                        handlers.Add(handler);
                    } else {
                        var pin = this.pinPointArray[index];
                        points.Add(pin);

                        var handler = new PinHandler(pinHandler.masterSortFactor, points.Count - 1,0, pinHandler.type);
                        handlers.Add(handler);
                    }
                }
            }

            this.pinPathArray = paths.ToArray(allocator);
            this.pinPointArray = points.ToArray(allocator);
            this.handlerArray = handlers;
            
            paths.Dispose();
            points.Dispose();
        }

        private NativeArray<int> BuildSlavePath() {
            int n = handlerArray.Count;

            var iStones = new NativeArray<IndexMileStone>(n, Allocator.Temp);

            for (int j = 0; j < n; ++j) {
                var handler = handlerArray[j];
                var index = handler.index;
                if (handler.isPinPath == 0) {
                    var point = this.pinPointArray[index];
                    iStones[j] = new IndexMileStone(j, point.slaveMileStone);
                } else {
                    var path = this.pinPathArray[index];
                    iStones[j] = new IndexMileStone(j, path.v0.slaveMileStone);
                }
            }


            bool isNotSorted;

            var m = n;

            do {
                isNotSorted = false;
                var a = iStones[0];
                var i = 1;
                while (i < m) {
                    var b = iStones[i];
                    if (PathMileStone.Compare(a.stone, b.stone)) {
                        iStones[i - 1] = b;
                        isNotSorted = true;
                    } else {
                        iStones[i - 1] = a;
                        a = b;
                    }

                    i += 1;
                }

                m -= 1;
                iStones[m] = a;
            } while (isNotSorted);

            var indexArray = new NativeArray<int>(n, allocator);

            for (int j = 0; j < n; ++j) {
                indexArray[j] = iStones[j].index;
            }

            return indexArray;
        }
    }
}