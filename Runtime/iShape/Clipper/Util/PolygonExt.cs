using iShape.Geometry;
using Unity.Collections;
using UnityEngine;

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
        
        public static IntVector AnyInside(this NativeArray<IntVector> self, bool isClockWise) {
            int n = self.Length;
            if (n == 1) {
                return self[0];
            }

            var p0 = self[0];
            var p1 = self[1];

            var cx = (p0.x + p1.x) >> 1;
            var cy = (p0.y + p1.y) >> 1;
            float dx = p1.x - p0.x;
            float dy = p1.y - p0.y;
            float l = 1f / Mathf.Sqrt(dx * dx + dy * dy);
            float nx = -dy * l;
            float ny = dx * l;

            var n0x = (long)Mathf.Round(nx);
            var n0y = (long) Mathf.Round(ny);

            if (isClockWise) {
                n0x = -n0x;
                n0y = -n0y;
            }

            return new IntVector(cx + n0x, cy + n0y);
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
        
        public static bool IsContain(this NativeArray<IntVector> self, NativeArray<IntVector> hole, bool isClockWise) {
            if (hole.Length == 0) {
                return false;
            }

            var p = hole.AnyInside(isClockWise);

            int n = self.Length;
            var isContain = false;
            var b = self[n - 1];
            for (int i = 0; i < n; ++i) {
                var a = self[i];

                bool isInRange = a.y > p.y != b.y > p.y;
                if (isInRange) {
                    long dx = b.x - a.x;
                    long dy = b.y - a.y;
                    long sx = (p.y - a.y) * dx / dy + a.x;
                    if (p.x < sx) {
                        isContain = !isContain;
                    }
                }

                b = a;
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