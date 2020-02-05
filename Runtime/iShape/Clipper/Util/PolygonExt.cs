using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Util {

    public static class PolygonExt {
        
        public static IntVector Any(this NativeArray<IntVector> self) {
            if (self.Length > 1) {
                var a = self[0];
                var b = self[1];
                return new IntVector((a.x + b.x) >> 1, (a.y + b.y) >> 1);
            } else {
                return self[0];
            }
        }

        public static bool IsContain(this NativeArray<IntVector> self, IntVector point) {
            int n = self.Length;
            
            var isContain = false;
            var p2 = self[n - 1];
            for (int i = 0; i < n; ++i) {
                var p1 = self[i];
                if (p1.y > point.y != p2.y > point.y &&
                    point.x < (p2.x - p1.x) * (point.y - p1.y) / (p2.y - p1.y) + p1.x) {
                    isContain = !isContain;
                }

                p2 = p1;
            }

            return isContain;
        }

        public static bool IsOverlap(this NativeArray<IntVector> self, NativeArray<IntVector> points) {
            int n = points.Length;
            var a = points[n - 1];
            for (int i = 0; i < n; ++i) {
                var b = points[i];
                var c = new IntVector((a.x + b.x) >> 1, (a.y + b.y) >> 1);
                if (self.IsContain(c)) {
                    return true;
                }

                a = b;
            }

            return false;
        }
    }

}