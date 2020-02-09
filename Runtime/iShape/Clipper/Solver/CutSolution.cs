using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {

    public struct CutSolution {
        public PlainShape restPathList;
        public PlainShape bitePathList;
        public readonly SubtractSolution.Nature nature;

        public CutSolution(PlainShape restPathList, PlainShape bitePathList, SubtractSolution.Nature nature) {
            this.restPathList = restPathList;
            this.bitePathList = bitePathList;
            this.nature = nature;
        }

        public void Dispose() {
            this.restPathList.Dispose();
            this.bitePathList.Dispose();            
        }
    }

}