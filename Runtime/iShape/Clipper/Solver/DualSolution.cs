using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {
    
    public struct DualSolution {
        public PlainShape mainPathList;
        public PlainShape partPathList;
        public readonly Solution.Nature nature;

        public DualSolution(PlainShape mainPathList, PlainShape partPathList, Solution.Nature nature) {
            this.mainPathList = mainPathList;
            this.partPathList = partPathList;
            this.nature = nature;
        }

        public void Dispose() {
            this.mainPathList.Dispose();
            this.partPathList.Dispose();            
        }
    }
}