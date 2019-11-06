namespace iShape.Clipper.Intersection.Primitive {
    internal struct IndexMileStone {

        internal readonly int index;                         // index in outside array
        internal PathMileStone stone;

        internal IndexMileStone(int index, PathMileStone stone) {
            this.index = index;
            this.stone = stone;
        }

    }
}