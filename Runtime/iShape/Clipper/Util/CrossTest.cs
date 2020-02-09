using iShape.Clipper.Collision;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Util {

    public static class CrossTest {
        
        public static bool IsSelfIntersected(this NativeArray<IntVector> points) {
            int n = points.Length;

            for (int i = 0, j = n - 1; i < n - 1; j = i++) {
                var a0 = points[i];
                var a1 = points[j];

                var aRect = new Rect(a0, a1);

                for (int k = i + 1; k < n - 1; ++k) {
                    var b0 = points[k];
                    var b1 = points[k + 1];

                    var bRect = new Rect(b0, b1);
                    if (aRect.IsIntersecting(bRect) && CrossResolver.DefineType(a0, a1, b0, b1,out var point) != CrossType.not_cross) {
                        return false;
                    }
                }
            }

            return false;
        }

        public static bool IsSelfIntersected(this IntVector[] points) {
            int n = points.Length;

            for (int i = 0, j = n - 1; i < n - 1; j = i++) {
                var a0 = points[i];
                var a1 = points[j];

                var aRect = new Rect(a0, a1);

                for (int k = i + 1; k < n - 1; ++k) {
                    var b0 = points[k];
                    var b1 = points[k + 1];

                    var bRect = new Rect(b0, b1);
                    if (aRect.IsIntersecting(bRect) && CrossResolver.DefineType(a0, a1, b0, b1,out var point) != CrossType.not_cross) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsIntersected(this IntVector[] points, IntVector[] target) {
            var rectShapeA = new Rect(points);
            var rectShapeB = new Rect(target);

            if (rectShapeA.IsNotIntersecting(rectShapeB)) {
                return false;
            }

            int n = points.Length;
            int m = target.Length;

            for (int i = 0, j = n - 1; i < n; j = i++) {
                var a0 = points[i];
                var a1 = points[j];

                var aRect = new Rect(a0, a1);

                if (rectShapeB.IsIntersecting(aRect)) {
                    for (int k = 0, l = m - 1; k < m; l = k++) {
                        var b0 = target[k];
                        var b1 = target[l];

                        var bRect = new Rect(b0, b1);
                        if (aRect.IsIntersecting(bRect) && CrossResolver.DefineType(a0, a1, b0, b1,out var point) != CrossType.not_cross) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }

}