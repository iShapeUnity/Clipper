using iShape.Geometry;
namespace iShape.Clipper.Intersection.Primitive  {
    internal struct PinEdge {
        internal PinPoint v0;
        internal PinPoint v1;
        internal readonly int interposition; // if slave and master same direction 1, other -1

        internal bool isZeroLength => v0.point == v1.point;

        internal PinEdge(PinPoint v0, PinPoint v1, int interposition) {
            this.v0 = v0;
            this.v1 = v1;
            this.interposition = interposition;
        }

        internal PinEdge(Vertex msPt0, Vertex msPt1, Vertex slPt0, Vertex slPt1) {
            Vertex minMsPt;
            Vertex maxMsPt;

            bool isDirectMaster = msPt0.point.BitPack < msPt1.point.BitPack;
            if (isDirectMaster) {
                minMsPt = msPt0;
                maxMsPt = msPt1;
            } else {
                minMsPt = msPt1;
                maxMsPt = msPt0;
            }

            Vertex minSlPt;
            Vertex maxSlPt;
            bool isDirectSlave = slPt0.point.BitPack < slPt1.point.BitPack;

            if (isDirectSlave) {
                minSlPt = slPt0;
                maxSlPt = slPt1;
            } else {
                minSlPt = slPt1;
                maxSlPt = slPt0;
            }

            // left end, case 1, 2

            PathMileStone minMsStone;
            PathMileStone minSlStone;
            IntVector minCross;
            if (minMsPt.point.BitPack < minSlPt.point.BitPack) {
                // a < b
                minCross = minSlPt.point;
                minMsStone = msPt0.SqrDistance(minCross);
                minSlStone = new PathMileStone(minSlPt.index);
            } else if (minMsPt.point != minSlPt.point) {
                // a > b
                minCross = minMsPt.point;
                minMsStone = new PathMileStone(minMsPt.index);
                minSlStone = slPt0.SqrDistance(minCross);
            } else {
                // a == b
                minCross = minMsPt.point;
                minMsStone = new PathMileStone(minMsPt.index);
                minSlStone = new PathMileStone(minSlPt.index);
            }


            // right end, case 1, 2

            PathMileStone maxMsStone;
            PathMileStone maxSlStone;
            IntVector maxCross;

            if (maxMsPt.point.BitPack < maxSlPt.point.BitPack) {
                // a < b
                maxCross = maxMsPt.point;
                maxMsStone = new PathMileStone(maxMsPt.index);
                maxSlStone = slPt0.SqrDistance(maxCross);
            } else if (maxMsPt.point != maxSlPt.point) {
                // a > b
                maxCross = maxSlPt.point;
                maxMsStone = msPt0.SqrDistance(maxCross);
                maxSlStone = new PathMileStone(maxSlPt.index);
            } else {
                // a == b
                maxCross = maxMsPt.point;
                maxMsStone = new PathMileStone(maxMsPt.index);
                maxSlStone = new PathMileStone(maxSlPt.index);
            }


            IntVector pnt0;
            IntVector pnt1;
            PathMileStone masterMileStone0;
            PathMileStone masterMileStone1;
            PathMileStone slaveMileStone0;
            PathMileStone slaveMileStone1;

            if (isDirectMaster) {
                pnt0 = minCross;
                masterMileStone0 = minMsStone;
                slaveMileStone0 = minSlStone;

                pnt1 = maxCross;
                masterMileStone1 = maxMsStone;
                slaveMileStone1 = maxSlStone;
            } else {
                pnt0 = maxCross;
                masterMileStone0 = maxMsStone;
                slaveMileStone0 = maxSlStone;

                pnt1 = minCross;
                masterMileStone1 = minMsStone;
                slaveMileStone1 = minSlStone;
            }

            this.v0 = new PinPoint(pnt0, PinPoint.PinType.nil, masterMileStone0, slaveMileStone0);
            this.v1 = new PinPoint(pnt1, PinPoint.PinType.nil, masterMileStone1, slaveMileStone1);
            this.interposition = isDirectMaster == isDirectSlave ? 1 : -1;
        }
    }

    internal static class VertexExt {
        internal static PathMileStone SqrDistance(this Vertex self, IntVector stone) {
            return new PathMileStone(self.index, self.point.SqrDistance(stone));
        }
    }


}