namespace iShape.Clipper.Collision.Primitive {
    public struct PinHandler {

        internal PathMileStone masterSortFactor;

        internal readonly bool isPinPath;            
        internal readonly int index;                  // index in outside array
        internal readonly PinPoint.PinType type;
        internal readonly bool marker;                // true - present, false - removed

        internal PinHandler(PathMileStone sortFactor, int index, bool isPinPath, PinPoint.PinType type) {
            this.index = index;
            this.isPinPath = isPinPath;
            this.masterSortFactor = sortFactor;
            this.type = type;
            this.marker = true;
        }


        internal PinHandler(PathMileStone sortFactor, int index, bool isPinPath, bool marker, PinPoint.PinType type) {
            this.index = index;
            this.isPinPath = isPinPath;
            this.masterSortFactor = sortFactor;
            this.marker = marker;
            this.type = type;
        }


        internal PinHandler(PinPoint pinPoint, int index) {
            this.index = index;
            this.isPinPath = false;
            this.type = pinPoint.type;
            this.masterSortFactor = pinPoint.masterMileStone;
            this.marker = true;
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