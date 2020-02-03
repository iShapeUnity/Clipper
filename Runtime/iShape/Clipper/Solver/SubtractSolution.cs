using iShape.Geometry;

namespace iShape.Clipper.Solver {

    public struct SubtractSolution {
        public enum Nature {
            notOverlap,
            overlap,
            empty,
            hole
        }

        public PlainPathList pathList;
        public readonly Nature nature;

        public SubtractSolution(PlainPathList pathList, Nature nature) {
            this.pathList = pathList;
            this.nature = nature;
        }

        public void Dispose() {
            this.pathList.Dispose();
        }
    }
}