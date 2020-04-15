using iShape.Clipper.Util;
using iShape.Geometry;

namespace iShape.Clipper.Collision.Primitive {
    public struct PinPoint {
        internal struct Def {
            
            internal readonly DBVector dp;
            internal readonly IntVector pt;
            internal readonly IntVector ms0;
            internal readonly IntVector ms1;
            internal readonly IntVector sl0;
            internal readonly IntVector sl1;
            internal readonly PathMileStone masterMileStone;
            internal readonly PathMileStone slaveMileStone;

            internal Def(
                DBVector dp,
                IntVector pt,
                IntVector ms0,
                IntVector ms1,
                IntVector sl0,
                IntVector sl1,
                PathMileStone masterMileStone,
                PathMileStone slaveMileStone
            ) {
                this.dp = dp;
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

        public PinPoint(IntVector point, PinType type, PathMileStone masterMileStone, PathMileStone slaveMileStone) {
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
        
        public static bool operator <(PinPoint a, PinPoint b) {
            if (a != b) {
                return a.masterMileStone < b.masterMileStone;
            } else {
                return a.type != PinPoint.PinType.nil;
            }
        }

        public static bool operator >(PinPoint a, PinPoint b) {
            if (a != b) {
                return a.masterMileStone > b.masterMileStone;
            } else {
                return a.type == PinPoint.PinType.nil;
            }
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
            bool isCW = PinPoint.IsClockWise(def.ms1, def.dp, def.sl1);
            var type = isCW ? PinType.outside : PinType.inside;
            return new PinPoint(def.pt, type, def.masterMileStone, def.slaveMileStone);
        }
        
        internal static PinPoint BuildOnSide(Def def) {
            var corner = new Corner(def.dp, def.pt, def.ms0, def.ms1);

            var s0 = corner.IsBetweenDoubleVersion(def.sl0, true);
            var s1 = corner.IsBetweenDoubleVersion(def.sl1, true);

            PinType type;
            if (s0 == Corner.Result.onBoarder || s1 == Corner.Result.onBoarder) {
                if (s0 == Corner.Result.onBoarder && s1 == Corner.Result.onBoarder) {
                    type = PinType.nil;
                } else if (s0 != Corner.Result.onBoarder) {
                    type = s0 == Corner.Result.contain ? PinType.inside : PinType.outside;
                } else {
                    type = s1 == Corner.Result.contain ? PinType.outside : PinType.inside;
                }
            } else {
                bool isSl0 = s0 == Corner.Result.contain;
                bool isSl1 = s1 == Corner.Result.contain;
            
                if (isSl0 && isSl1) {
                    type = PinType.in_out;
                } else if (!isSl0 && !isSl1) {
                    type = PinType.out_in;
                } else {
                    type = isSl0 ? PinType.inside : PinType.outside;
                }
            }

            return new PinPoint(def.pt, type, def.masterMileStone, def.slaveMileStone);
        }

        private static bool IsClockWise(IntVector ia, DBVector b, IntVector ic) {
            var a = new DBVector(ia);
            var c = new DBVector(ic);
            double m0 = (c.y - a.y) * (b.x - a.x);
            double m1 = (b.y - a.y) * (c.x - a.x);

            return m0 < m1;
        }
    }
}