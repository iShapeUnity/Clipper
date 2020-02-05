using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {
    
    public struct UnionSolution {
        public enum Nature {
            notOverlap,
            overlap, 
            masterIncludeSlave, 
            slaveIncludeMaster
        }

        public PlainShape pathList;
        public readonly Nature nature;

        public UnionSolution(PlainShape pathList, Nature nature) {
            this.pathList = pathList;
            this.nature = nature;
        }

        public void Dispose() {
            this.pathList.Dispose();
        }
    }
}