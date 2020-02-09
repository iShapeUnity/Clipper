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

            nil = 0, // can be ignored

            outside = -1,
            out_in = -2
        }

        public readonly IntVector point;
        public readonly PinType type; // 1 - in, -1 - out, 2 in-out, -2 out-in
        public readonly PathMileStone masterMileStone;
        public readonly PathMileStone slaveMileStone;

        private PinPoint(IntVector point, PinType type, PathMileStone masterMileStone, PathMileStone slaveMileStone) {
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

        public static bool operator== (PinPoint left, PinPoint right) {
            return left.masterMileStone == right.masterMileStone && left.slaveMileStone == right.slaveMileStone;
        }
        
        public static bool operator!= (PinPoint left, PinPoint right) {
            return left.masterMileStone != right.masterMileStone || left.slaveMileStone != right.slaveMileStone;
        }

        public override bool Equals(object obj) {
            if(obj is PinPoint pinPoint) {
                return pinPoint == this;
            }
            return false;
        }

        public bool Equals(PinPoint other) {
            return masterMileStone.Equals(other.masterMileStone) && slaveMileStone.Equals(other.slaveMileStone);
        }

        public override int GetHashCode() {
            unchecked {
                return (masterMileStone.GetHashCode() * 397) ^ slaveMileStone.GetHashCode();
            }
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
        
        internal static PinPoint BuildOnSlave(Def def, IntGeom iGeom) {
            var corner = new Corner(def.pt,def.ms0,def.ms1, iGeom);

            bool isSl0 = corner.IsBetween(def.sl0, true);
            bool isSl1 = corner.IsBetween(def.sl1, true);

            PinType type;
            if (isSl0 && isSl1) {
                type = PinType.in_out;
            } else if (!isSl0 && !isSl1) {
                type = PinType.out_in;
            } else {
                type = isSl0 ? PinType.inside : PinType.outside;
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