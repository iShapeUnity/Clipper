using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {
    
    public struct ComplexSolution {
        public PlainShape restPathList;
        public PlainShape bitePathList;
        public readonly Solution.Nature nature;

        public ComplexSolution(PlainShape restPathList, PlainShape bitePathList, Solution.Nature nature) {
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