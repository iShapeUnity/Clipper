namespace iShape.Clipper.Intersection.Primitive {
    public struct PinHandler {
        
        internal PathMileStone masterSortFactor;

        internal readonly int isPinPath;            // 0 - false, 1 - true
        internal readonly int index;                // index in outside array
        internal readonly PinPoint.PinType  type;   // PinType

        internal int marker; // 0 - present, 1 - removed

        internal PinHandler(PathMileStone sortFactor, int index, int isPinPath, PinPoint.PinType type) {
            this.index = index;
            this.isPinPath = isPinPath;
            this.masterSortFactor = sortFactor;
            this.type = type;
            this.marker = 0;
        }


        internal PinHandler(PathMileStone sortFactor, int index, int isPinPath, int marker, PinPoint.PinType type) {
            this.index = index;
            this.isPinPath = isPinPath;
            this.masterSortFactor = sortFactor;
            this.marker = marker;
            this.type = type;
        }


        internal PinHandler(PinPoint pinPoint, int index) {
            this.index = index;
            this.isPinPath = 0;
            this.type = pinPoint.type;
            this.masterSortFactor = pinPoint.masterMileStone;
            this.marker = 0;
        }


        public static bool operator ==(PinHandler left, PinHandler right) {
            return left.masterSortFactor.index == right.masterSortFactor.index &&
                   left.masterSortFactor.offset == right.masterSortFactor.offset;
        }

        public static bool operator !=(PinHandler left, PinHandler right) {
            return left.masterSortFactor.index != right.masterSortFactor.index ||
                   left.masterSortFactor.offset != right.masterSortFactor.offset;
        }
    }
}