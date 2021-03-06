using Unity.Collections;

namespace iShape.Clipper.Collision.Primitive {
    public struct PinPath {
        public PinPoint v0;
        public PinPoint v1;

        public bool isClosed => v0 == v1;

        internal PinPath(PinEdge edge) {
            var type = edge.type;
            this.v0 = new PinPoint(edge.v0, type);
            this.v1 = new PinPoint(edge.v1, type);
        }

        public int GetLength(int count) {
            var a = v0.masterMileStone;
            var b = v1.masterMileStone;
            int length = 0;
            if (a > b) {
                length = count;
            }

            length += b.index - a.index;
            if (b.offset != 0) {
                length += 1;
            }

            return length;
        }


        public NativeArray<PinHandler> Extract(int index, int pathCount, Allocator allocator) {
            int n = pathCount;

            var firstHandler = new PinHandler(v0.masterMileStone, index, true, true, v0.type);
            var lastHandler = new PinHandler(v1.masterMileStone, index, true, false, v0.type);

            var length = GetLength(n);

            if (length < 2) {
                return new NativeArray<PinHandler>(2, allocator) {[0] = firstHandler, [1] = lastHandler};
            }

            if (length == 2) {
                var middleIndex = (v0.masterMileStone.index + 1) % n;
                var middleSortFactor = new PathMileStone(middleIndex);
                var middle = new PinHandler(middleSortFactor, index, true, false, v0.type);
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
                handlers[j++] = new PinHandler(new PathMileStone(i), index, true, false, v0.type);
                i = (i + 1) % n;
            }

            handlers[j++] = new PinHandler(new PathMileStone(endIndex), index, true, false, v0.type);
            handlers[j] = lastHandler;

            return handlers;
        }
    }
}