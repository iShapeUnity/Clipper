namespace iShape.Clipper.Intersection.Primitive {
    
    internal struct PathMileStone {
        
        internal static PathMileStone zero = new PathMileStone(0, 0);

        internal readonly int index;
        internal readonly long offset;

        internal PathMileStone(int index, long offset = 0) {
            this.index = index;
            this.offset = offset;
        }

        internal static bool Compare(PathMileStone a, PathMileStone b) {
            if (a.index != b.index) {
                return a.index > b.index;
            }

            return a.offset > b.offset;
        }

        internal static bool MoreOrEqual(PathMileStone a, PathMileStone b) {
            if (a.index != b.index) {
                return a.index > b.index;
            }

            return a.offset >= b.offset;
        }
        
        public static bool operator== (PathMileStone left, PathMileStone right) {
            return left.index == right.index && left.offset == right.offset;
        }

        public static bool operator!= (PathMileStone left, PathMileStone right) {
            return left.index != right.index || left.offset != right.offset;
        }
    }
}