using iShape.Geometry.Container;

namespace iShape.Clipper.Solver {

    public struct BiteSolution {

        public readonly bool isInteract;
        public readonly PlainShapeList mainList;
        public readonly PlainShapeList biteList;

        public BiteSolution(PlainShapeList mainList, PlainShapeList biteList, bool isInteract) {
            this.mainList = mainList;
            this.biteList = biteList;
            this.isInteract = isInteract;
        }

        public void Dispose() {
            this.mainList.Dispose();
            this.biteList.Dispose();
        }
    }

}