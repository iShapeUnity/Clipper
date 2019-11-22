using iShape.Geometry;

namespace iShape.Clipper.Collision {
    internal struct CrossResolver {

        //-1 - no intersections
        // 0 - same line
        // 1 - simple intersection with no overlaps
        // 2 - one of the end is lying on others edge
        // 3 - first master end is equal to one of slave ends
        internal static CrossType DefineType(IntVector a0, IntVector a1, IntVector b0, IntVector b1) {
            int d0 = IsClockWise(a0, b0, b1);
            int d1 = IsClockWise(a1, b0, b1);
            int d2 = IsClockWise(a0, a1, b0);
            int d3 = IsClockWise(a0, a1, b1);

            if (d0 != 0 || d1 != 0 || d2 != 0 || d3 != 0) {
                bool t0 = d0 < 0;
                bool t1 = d1 < 0;
                bool t2 = d2 < 0;
                bool t3 = d3 < 0;

                if (t0 != t1 && t2 != t3) {
                    if (d0 != 0 && d1 != 0 && d2 != 0 && d3 != 0) {
                        return CrossType.pure;
                    }

                    return CrossType.edge_cross;
                }

                // TODO check corner case 
                if (a0 != b0 && a0 != b1) {
                    return CrossType.not_cross;
                }

                return CrossType.common_end;
            }

            return CrossType.same_line;
        }


        private static int IsClockWise(IntVector a, IntVector b, IntVector c) {
            long m0 = (c.y - a.y) * (b.x - a.x);
            long m1 = (b.y - a.y) * (c.x - a.x);

            if (m0 < m1) {
                return -1;
            }

            return m0 > m1 ? 1 : 0;
        }
        
        
    }
}