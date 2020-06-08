using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {

    public struct ComplexSubtractSolution {

        public readonly bool isInteract;
        public PlainShapeList mainList;
        public PlainShapeList partList;

        public ComplexSubtractSolution(PlainShapeList mainList, PlainShapeList partList, bool isInteract) {
            this.mainList = mainList;
            this.partList = partList;
            this.isInteract = isInteract;
        }

        public void Dispose() {
            this.mainList.Dispose();
            this.partList.Dispose();
        }
    }

}