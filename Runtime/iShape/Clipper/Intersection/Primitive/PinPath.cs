using Unity.Collections;

namespace iShape.Clipper.Intersection.Primitive {
    internal struct PinPath {
        internal PinPoint v0;
        internal PinPoint v1;

        internal PinPath(PinPoint v0, PinPoint v1, PinPoint.PinType type) {
            this.v0 = new PinPoint(v0, type);
            this.v1 = new PinPoint(v1, type);
        }

        private int GetLength(int count) {
            var a = v0.masterMileStone;
            var b = v1.masterMileStone;
            int length = 0;
            if (PathMileStone.Compare(a, b)) {
                length = count;
            }

            length += b.index - a.index;
            if (b.offset != 0) {
                length += 1;
            }

            return length;
        }


        internal NativeArray<PinHandler> Extract(int index, int pathCount, Allocator allocator) {
            int n = pathCount;

            var firstHandler = new PinHandler(v0.masterMileStone, index, 1, 0, v0.type);
            var lastHandler = new PinHandler(v1.masterMileStone, index, 1, 1, v0.type);

            var length = GetLength(n);

            if (length < 2) {
                return new NativeArray<PinHandler>(2, allocator) {[0] = firstHandler, [1] = lastHandler};
            }

            if (length == 2) {
                var middleIndex = (v0.masterMileStone.index + 1) % n;
                var middleSortFactor = new PathMileStone(middleIndex,0);
                var middle = new PinHandler(middleSortFactor, index, 1, 1, v0.type);
                return new NativeArray<PinHandler>(3, allocator) {[0] = firstHandler, [1] = middle, [2] = lastHandler};
            }

            var handlers = new NativeArray<PinHandler>(length + 1, allocator);
            int j = 0;
            handlers[j++] = firstHandler;

            int i = (v0.masterMileStone.index + 1) % n;
            int endIndex;
            if (v1.masterMileStone.offset != 0) {
                endIndex = v1.masterMileStone.index;
            } else {
                endIndex = (v1.masterMileStone.index - 1 + n) % n;
            }

            while (i != endIndex) {
                handlers[j++] = new PinHandler(new PathMileStone(i), index, 1, 1, v0.type);
                i = (i + 1) % n;
            }

            handlers[j++] = new PinHandler(new PathMileStone(endIndex), index, 1, 1, v0.type);
            handlers[j] = lastHandler;

            return handlers;
        }
    }
}