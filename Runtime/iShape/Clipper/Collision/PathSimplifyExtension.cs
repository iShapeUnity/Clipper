using iShape.Collections;
using iShape.Geometry;

namespace iShape.Clipper.Collision {

    public static class PathSimplifyExtension {
        public static void Simplify(this ref DynamicArray<IntVector> path) {
            int n = path.Count;
            if (n <= 3) {
                return;
            }

            int i0 = 0;

            while (i0 < n && n >= 4) {
                int i1 = (i0 + 1) % n;
                int i2 = (i0 + 2) % n;
                int i3 = (i0 + 3) % n;

                var p0 = path[i0];
                var p1 = path[i1];
                var p2 = path[i2];
                var p3 = path[i3];

                var type = CrossResolver.IsCross(p0, p1, p2, p3, out var cross);

                switch (type) {
                    case CrossType.pure:
                        if (i1 < i2) {
                            path.RemoveAt(i2);
                            path[i1] = cross;
                        } else {
                            path.RemoveAt(i1);
                            path[i2] = cross;
                        }

                        n = path.Count;
                        break;
                    case CrossType.end_a0:
                    case CrossType.end_b1:
                    case CrossType.end_a0_b0:
                    case CrossType.same_line:
                        if (i1 < i2) {
                            path.RemoveAt(i2);
                            path.RemoveAt(i1);
                        } else {
                            path.RemoveAt(i1);
                            path.RemoveAt(i2);
                        }

                        n = path.Count;
                        break;
                    case CrossType.end_a1:
                    case CrossType.end_a1_b0:
                        path.RemoveAt(i2);
                        n = path.Count;
                        break;
                    case CrossType.end_b0:
                        path.RemoveAt(i1);
                        n = path.Count;
                        break;
                    case CrossType.end_a0_b1:
                        if (i1 < i3) {
                            path.RemoveAt(i3);
                            path.RemoveAt(i2);
                            path.RemoveAt(i1);
                        } else if (i1 < i2) {
                            // i3 === 0
                            path.RemoveFromEnd(2);
                            path.RemoveAt(0);
                        } else {
                            // i3 === 1, i2 === 0
                            path.RemoveFromEnd(1);
                            path.RemoveAt(1);
                            path.RemoveAt(0);
                        }

                        n = path.Count;
                        break;
                    case CrossType.end_a1_b1:
                        if (i2 < i3) {
                            path.RemoveAt(i3);
                            path.RemoveAt(i2);
                        } else {
                            path.RemoveAt(i2);
                            path.RemoveAt(i3);
                        }

                        n = path.Count;
                        break;
                    default:
                        i0 += 1;
                        break;
                }
            }
        }
    }

}