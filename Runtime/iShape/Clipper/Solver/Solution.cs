using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {

    public struct Solution {
        public enum Nature {
            notOverlap,
            overlap,
            equal,
            masterIncludeSlave,
            slaveIncludeMaster
        }

        public PlainShape pathList;
        public readonly Nature nature;

        public Solution(PlainShape pathList, Nature nature) {
            this.pathList = pathList;
            this.nature = nature;
        }

        public void Dispose() {
            this.pathList.Dispose();
        }
    }
}