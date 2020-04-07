using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Primitive;
using iShape.Clipper.Util;
using iShape.Collections;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Solver {

    internal struct FilterNavigator {
        internal PinNavigator navigator;
        private NativeArray<Cursor> nextCursors;
        private int nextIndex;

        internal FilterNavigator(PinNavigator navigator, PinPoint.PinType primary, PinPoint.PinType secondary, Allocator allocator) {
            this.navigator = navigator;
            this.nextCursors = getCursors(navigator, primary, secondary, allocator);
            this.nextIndex = 0;
        }
    
        internal Cursor Next() {
            while (nextIndex < nextCursors.Length) {
                var next = this.nextCursors[nextIndex];
                var node = this.navigator.nodeArray[next.index];
                ++this.nextIndex;
                if (node.marker != 1) {
                    return next;
                }
            }

            return Cursor.empty;
        }
    
        internal Cursor First() {
            return nextCursors.Length > 0 ? this.nextCursors[0] : Cursor.empty;
        }
        
        
        internal Solution.Nature Nature(NativeArray<IntVector> master, NativeArray<IntVector> slave, bool isSlaveClockWise) {
            if (navigator.isEqual) {
                return Solution.Nature.equal;
            }

            var cursor = this.First();
            if (cursor.isNotEmpty) {
                return Solution.Nature.overlap;
            } else if (this.navigator.hasContacts) {
                var masterBox = this.navigator.masterBox;
                var slaveBox = this.navigator.slaveBox;
                if (masterBox.IsInside(slaveBox) && master.IsContain(slave, isSlaveClockWise)) {
                    return Solution.Nature.masterIncludeSlave;
                }
                if (slaveBox.IsInside(masterBox) && slave.IsContain(master, true)) {
                    return Solution.Nature.slaveIncludeMaster;
                }
                return Solution.Nature.notOverlap;
            } else if (master.IsContain(slave[0])) {
                return Solution.Nature.masterIncludeSlave;
            } else if (slave.IsContain(master[0])) {
                return Solution.Nature.slaveIncludeMaster;
            } else {
                return Solution.Nature.notOverlap;
            }
        }
        
        internal void Reset() {
            var nodeArray = this.navigator.nodeArray;
            int n = nodeArray.Length;
            for(int i = 0; i < n; ++i) {
                var node = nodeArray[i];
                node.marker = 0;
                nodeArray[i] = node;
            }
        }

        internal void Dispose() {
            this.navigator.Dispose();
            this.nextCursors.Dispose();
        }

        private static NativeArray<Cursor> getCursors(PinNavigator navigator, PinPoint.PinType primary, PinPoint.PinType secondary, Allocator allocator) {
            var n = navigator.nodeArray.Length;
            var cursors = new DynamicArray<Cursor>(n, Allocator.Temp);
            for(int i = 0; i < n; ++i) {
                var node = navigator.nodeArray[i];
                PinPoint.PinType type;
                if (!node.isPinPath) {
                    var pin = navigator.pinPointArray[node.index];
                    type = pin.type;
                } else {
                    var path = navigator.pinPathArray[node.index];
                    type = path.v0.type;
                }
                if (type == primary || type == secondary) {
                    cursors.Add(new Cursor(type, i));
                }
            }

            var result = cursors.ToArray(allocator);
            sort(result, primary);

            return result;
        }
        
        private static void sort(NativeArray<Cursor> cursors, PinPoint.PinType primary) {
            // this array is already sorted by edge index

            int n = cursors.Length;

            if (n < 2) {
                return;
            }

            bool isNotSorted;

            var m = n;

            do {
                isNotSorted = false;
                var a = cursors[0];
                var i = 1;
                while (i < m) {
                    var b = cursors[i];
                    if (a.type != primary && b.type == primary) {
                        cursors[i - 1] = b;
                        isNotSorted = true;
                    }
                    else {
                        cursors[i - 1] = a;
                        a = b;
                    }

                    i += 1;
                }

                m -= 1;
                cursors[m] = a;
            } while (isNotSorted);       
        }
    }

}