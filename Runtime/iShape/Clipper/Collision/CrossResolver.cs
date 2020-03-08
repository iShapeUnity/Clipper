using System;
using iShape.Geometry;

namespace iShape.Clipper.Collision {

    internal struct CrossResolver {

        internal static CrossType DefineType(IntVector a0, IntVector a1, IntVector b0, IntVector b1,
            out IntVector cross) {
            int d0 = IsClockWise(a0, b0, b1);
            int d1 = IsClockWise(a1, b0, b1);
            int d2 = IsClockWise(a0, a1, b0);
            int d3 = IsClockWise(a0, a1, b1);

            if (d0 == 0 || d1 == 0 || d2 == 0 || d3 == 0) {
                if (d0 == 0 && d1 == 0 && d2 == 0 && d3 == 0) {
                    cross = IntVector.Zero;
                    return CrossType.same_line;
                }

                if (d0 == 0) {
                    cross = a0;
                    if (d2 == 0 || d3 == 0) {
                        if (d2 == 0) {
                            return CrossType.end_a0_b0;
                        } else {
                            return CrossType.end_a0_b1;
                        }
                    } else if (d2 != d3) {
                        return CrossType.end_a0;
                    } else {
                        return CrossType.not_cross;
                    }
                }

                if (d1 == 0) {
                    cross = a1;
                    if (d2 == 0 || d3 == 0) {
                        if (d2 == 0) {
                            return CrossType.end_a1_b0;
                        } else {
                            return CrossType.end_a1_b1;
                        }
                    } else if (d2 != d3) {
                        return CrossType.end_a1;
                    } else {
                        return CrossType.not_cross;
                    }
                }

                if (d0 != d1) {
                    if (d2 == 0) {
                        cross = b0;
                        return CrossType.end_b0;
                    } else {
                        cross = b1;
                        return CrossType.end_b1;
                    }
                } else {
                    cross = IntVector.Zero;
                    return CrossType.not_cross;
                }
            } else if (d0 != d1 && d2 != d3) {
                cross = CrossResolver.Cross(a0, a1, b0, b1);
                // still can be ends (watch case union 44)
                var isA0 = a0 == cross;
                var isA1 = a1 == cross;
                var isB0 = b0 == cross;
                var isB1 = b1 == cross;

                if (!(isA0 || isA1 || isB0 || isB1)) {
                    return CrossType.pure;
                } else if (isA0 && isB0) {
                    return CrossType.end_a0_b0;
                } else if (isA0 && isB1) {
                    return CrossType.end_a0_b1;
                } else if (isA1 && isB0) {
                    return CrossType.end_a1_b0;
                } else if (isA1 && isB1) {
                    return CrossType.end_a1_b1;
                } else if (isA0) {
                    return CrossType.end_a0;
                } else if (isA1) {
                    return CrossType.end_a1;
                } else if (isB0) {
                    return CrossType.end_b0;
                } else {
                    return CrossType.end_b1;
                }
            }

            cross = IntVector.Zero;
            return CrossType.not_cross;
        }

        private static int IsClockWise(IntVector a, IntVector b, IntVector c) {
            long m0 = (c.y - a.y) * (b.x - a.x);
            long m1 = (b.y - a.y) * (c.x - a.x);

            if (m0 < m1) {
                return -1;
            }

            if (m0 > m1) {
                return 1;
            }

            return 0;
        }

        private static IntVector Cross(IntVector a0, IntVector a1, IntVector b0, IntVector b1) {
            long dxA = a0.x - a1.x;
            long dyB = b0.y - b1.y;
            long dyA = a0.y - a1.y;
            long dxB = b0.x - b1.x;

            long divider = dxA * dyB - dyA * dxB;

            long xyA = a0.x * a1.y - a0.y * a1.x;
            long xyB = b0.x * b1.y - b0.y * b1.x;
            
            double invert_divider = 1.0 / divider;

            double x = xyA * (double)(b0.x - b1.x) - (double)(a0.x - a1.x) * xyB;
            double y = xyA * (double)(b0.y - b1.y) - (double)(a0.y - a1.y) * xyB;

            double dx = x * invert_divider;
            double dy = y * invert_divider;
            
            long cx = (long) Math.Round(dx, MidpointRounding.AwayFromZero);
            long cy = (long) Math.Round(dy, MidpointRounding.AwayFromZero);

            return new IntVector(cx, cy);
        }
    }

}