using iShape.Clipper.Util;
using iShape.Geometry;

namespace iShape.Clipper.Collision.Primitive {
    public struct PinPoint {
        internal struct Def {
            internal readonly IntVector pt;
            internal readonly IntVector ms0;
            internal readonly IntVector ms1;
            internal readonly IntVector sl0;
            internal readonly IntVector sl1;
            internal readonly PathMileStone masterMileStone;
            internal readonly PathMileStone slaveMileStone;

            internal Def(
                IntVector pt,
                IntVector ms0,
                IntVector ms1,
                IntVector sl0,
                IntVector sl1,
                PathMileStone masterMileStone,
                PathMileStone slaveMileStone
            ) {
                this.pt = pt;
                this.ms0 = ms0;
                this.ms1 = ms1;
                this.sl0 = sl0;
                this.sl1 = sl1;
                this.masterMileStone = masterMileStone;
                this.slaveMileStone = slaveMileStone;
            }
        }

        public enum PinType {
            inside = 1,
            in_out = 2,
            in_null = 3,
            null_in = 4,

            nil = 0, // can be ignored

            outside = -1,
            out_in = -2,
            out_null = -3,
            null_out = -4
        }

        public IntVector point;
        internal readonly PinType type; // 1 - in, -1 - out, 2 in-out, -2 out-in
        public PathMileStone masterMileStone;
        public PathMileStone slaveMileStone;

        internal PinPoint(IntVector point, PinType type, PathMileStone masterMileStone, PathMileStone slaveMileStone) {
            this.point = point;
            this.type = type;
            this.masterMileStone = masterMileStone;
            this.slaveMileStone = slaveMileStone;
        }

        internal PinPoint(PinPoint pin, PinType type) {
            this.point = pin.point;
            this.type = type;
            this.masterMileStone = pin.masterMileStone;
            this.slaveMileStone = pin.slaveMileStone;
        }

        internal static PinPoint BuildSimple(Def def) {
            bool isCW = PinPoint.IsClockWise(def.ms1, def.pt, def.sl1);
            var type = isCW ? PinType.outside : PinType.inside;
            return new PinPoint(def.pt, type, def.masterMileStone, def.slaveMileStone);
        }

        internal static PinPoint BuildOnMaster(Def def) {
            bool isCW0 = PinPoint.IsClockWise(def.pt,def.ms1,def.sl0);
            bool isCW1 = PinPoint.IsClockWise(def.pt,def.ms1,def.sl1);

            PinType type;
            if (isCW0 == isCW1) {
                type = isCW0 ? PinType.out_in : PinType.in_out;
            }
            else {
                type = isCW0 ? PinType.outside : PinType.inside;
            }

            return new PinPoint(def.pt, type, def.masterMileStone, def.slaveMileStone);
        }


        internal static PinPoint BuildOnSlave(Def def) {
            bool isCCW0 = PinPoint.IsClockWise(def.pt,def.ms0,def.sl1);
            bool isCCW1 = PinPoint.IsClockWise(def.pt,def.ms1,def.sl1);

            PinType type;
            if (isCCW0 == isCCW1) {
                type = isCCW0 ? PinType.out_in : PinType.in_out;
            }
            else {
                type = isCCW0 ? PinType.outside : PinType.inside;
            }

            return new PinPoint(def.pt, type, def.masterMileStone, def.slaveMileStone);
        }


        internal static PinPoint BuildOnCross(Def def, IntGeom iGeom) {
            var corner = new Corner(def.pt,def.ms0,def.ms1, iGeom);

            var isSl0 = corner.IsBetween(def.sl0, true);
            var isSl1 = corner.IsBetween(def.sl1, true);

            PinType type;
            if (isSl0 && isSl1) {
                type = PinType.in_out;
            }
            else if (!isSl0 && !isSl1) {
                type = PinType.out_in;
            }
            else {
                type = isSl0 ? PinType.inside : PinType.outside;
            }

            return new PinPoint(def.pt, type, def.masterMileStone, def.slaveMileStone);
        }

        private static bool IsClockWise(IntVector a, IntVector b, IntVector c) {
            long m0 = (c.y - a.y) * (b.x - a.x);
            long m1 = (b.y - a.y) * (c.x - a.x);

            return m0 < m1;
        }
    }
}