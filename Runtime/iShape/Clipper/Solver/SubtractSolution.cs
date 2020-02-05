using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {

    public struct SubtractSolution {
        public enum Nature {
            notOverlap,
            overlap,
            empty,
            hole
        }

        public PlainShape pathList;
        public readonly Nature nature;

        public SubtractSolution(PlainShape pathList, Nature nature) {
            this.pathList = pathList;
            this.nature = nature;
        }

        public void Dispose() {
            this.pathList.Dispose();
        }
    }
}