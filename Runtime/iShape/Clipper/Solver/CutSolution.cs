using iShape.Geometry;

namespace iShape.Clipper.Solver {

    public struct CutSolution {

        public PlainPathList restPathList;
        public PlainPathList bitePathList;
        public readonly SubtractSolution.Nature nature;

        public CutSolution(PlainPathList restPathList, PlainPathList bitePathList, SubtractSolution.Nature nature) {
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