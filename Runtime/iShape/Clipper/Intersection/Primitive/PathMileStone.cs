namespace iShape.Clipper.Intersection.Primitive {
    public struct PathMileStone {

        public readonly int index;
        public readonly long offset;

        internal PathMileStone(int index, long offset = 0) {
            this.index = index;
            this.offset = offset;
        }

        public static bool operator== (PathMileStone a, PathMileStone b) {
            return a.index == b.index && a.offset == b.offset;
        }

        public static bool operator!= (PathMileStone a, PathMileStone b) {
            return a.index != b.index || a.offset != b.offset;
        }

        public static bool operator <(PathMileStone a, PathMileStone b) {
            if (a.index != b.index) {
                return a.index < b.index;
            }

            return a.offset < b.offset;
        }

        public static bool operator >(PathMileStone a, PathMileStone b) {
            if (a.index != b.index) {
                return a.index > b.index;
            }

            return a.offset > b.offset;
        }
        
        public static bool operator <=(PathMileStone a, PathMileStone b) {
            if (a.index != b.index) {
                return a.index <= b.index;
            }

            return a.offset <= b.offset;
        }
        
        public static bool operator >=(PathMileStone a, PathMileStone b) {
            if (a.index != b.index) {
                return a.index >= b.index;
            }

            return a.offset >= b.offset;
        }

        private bool Equals(PathMileStone other) {
            return index == other.index && offset == other.offset;
        }

        public override bool Equals(object obj) {
            return obj is PathMileStone other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (index * 397) ^ offset.GetHashCode();
            }
        }
    }
}