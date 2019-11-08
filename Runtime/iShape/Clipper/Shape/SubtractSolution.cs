using iShape.Geometry;

namespace iShape.Clipper.Shape {

    public struct SubtractSolution {
        public enum Nature {
            notOverlap,
            overlap,
            empty,
            hole
        }

        public readonly PlainPathList pathList;
        public readonly Nature nature;

        public SubtractSolution(PlainPathList pathList, Nature nature) {
            this.pathList = pathList;
            this.nature = nature;
        }
    }
}