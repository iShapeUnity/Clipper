using iShape.Geometry;

namespace iShape.Clipper.Util {
    public struct Corner {

        private readonly IntVector basis;
        private readonly IntVector a;
        private readonly IntVector b;
        private readonly IntVector o;
        private readonly long projection;
        private readonly bool isCWS;
        private readonly IntGeom iGeom;

        public Corner(IntVector o, IntVector a, IntVector b, IntGeom iGeom) {
            this.o = o;
            this.a = a;
            this.b = b;

            this.basis = new IntVector(o.x - a.x, o.y - a.y).Normal(iGeom);
            var satellite = new IntVector(o.x - b.x, o.y - b.y).Normal(iGeom);

            this.projection = basis.ScalarMultiply(satellite);
            this.isCWS = Corner.IsClockWiseDirection(a, o, b);
            this.iGeom = iGeom;
        }

        public bool IsBetween(IntVector p, bool clockwise = false) {
            var target = new IntVector(o.x - p.x,o.y - p.y).Normal(iGeom);
            var targetProjection = basis.ScalarMultiply(target);
            var isTargetCws = Corner.IsClockWiseDirection(a,o,p);

            bool result;

            if (this.isCWS && isTargetCws) {
                result = targetProjection > this.projection;
            } else if (!this.isCWS && !isTargetCws) {
                result = targetProjection < this.projection;
            } else {
                result = !this.isCWS;
            }

            return result != clockwise;
        }

        public bool IsOnBorder(IntVector p) {
            var dir = p - o;
            var testA = Corner.IsSameDirection(a - o, dir);
            var testB = Corner.IsSameDirection(b - o, dir);

            return testA || testB;
        }

        private static bool IsSameDirection(IntVector a, IntVector b) {
            var isSameLine = a.x * b.y == a.y * b.x;
            var isSameDirection = a.x * b.x >= 0 && a.y * b.y >= 0;
            return isSameLine && isSameDirection;
        }


        private static bool IsClockWiseDirection(IntVector a, IntVector b, IntVector c) {
            var m0 = (c.y - a.y) * (b.x - a.x);
            var m1 = (b.y - a.y) * (c.x - a.x);

            return m0 < m1;
        }

    }
}