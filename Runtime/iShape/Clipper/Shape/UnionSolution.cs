using iShape.Geometry;

namespace iShape.Clipper.Shape {
    
    public struct UnionSolution {
        public enum Nature {
            notOverlap,
            overlap
        }

        public PlainPathList pathList;
        public readonly Nature nature;

        public UnionSolution(PlainPathList pathList, Nature nature) {
            this.pathList = pathList;
            this.nature = nature;
        }
    }
}