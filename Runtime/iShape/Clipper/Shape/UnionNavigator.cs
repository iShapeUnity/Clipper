using iShape.Clipper.Collision.Navigation;
using iShape.Clipper.Collision.Primitive;
using iShape.Collections;
using Unity.Collections;

namespace iShape.Clipper.Shape {

    internal struct UnionNavigator {

        internal PinNavigator navigator;
        private NativeArray<Cursor> nextCursors;
        private int nextIndex;

        internal UnionNavigator(PinNavigator navigator, Allocator allocator) {
            this.navigator = navigator;
            this.nextCursors = getCursors(navigator, allocator);
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

        private static NativeArray<Cursor> getCursors(PinNavigator navigator, Allocator allocator) {
            var n = navigator.nodeArray.Length;
            var cursors = new DynamicArray<Cursor>(n, Allocator.Temp);
            for (int i = 0; i < n; ++i) {
                var node = navigator.nodeArray[i];
                PinPoint.PinType type;
                if (node.isPinPath == 0) {
                    var pin = navigator.pinPointArray[node.index];
                    type = pin.type;
                } else {
                    var path = navigator.pinPathArray[node.index];
                    type = path.v0.type;
                }

                if (type == PinPoint.PinType.outside || type == PinPoint.PinType.in_out) {
                    cursors.Add(new Cursor(type, i));
                }
            }

            var result = cursors.ToArray(allocator);
            sort(result);

            return result;
        }

        private static void sort(NativeArray<Cursor> cursors) {
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
                    if (a.type == PinPoint.PinType.outside && b.type != PinPoint.PinType.outside) {
                        cursors[i - 1] = b;
                        isNotSorted = true;
                    } else {
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