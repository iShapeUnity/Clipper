using iShape.Geometry;

namespace iShape.Clipper.Collision.Primitive {

    internal struct PinEdge {
        internal PinPoint v0;
        internal PinPoint v1;
        private readonly bool isDirect;
        internal bool isZeroLength => v0.point == v1.point;

        internal PinPoint.PinType type {
            get {
                if (isDirect) {
                    return PinEdge.Union(v0.type, v1.type);
                } else {
                    return PinEdge.Union(v1.type, v0.type);
                }
            }
        }

        internal PinEdge(PinPoint v0, PinPoint v1, bool isDirect) {
            this.v0 = v0;
            this.v1 = v1;
            this.isDirect = isDirect;
        }

        private static PinPoint.PinType Union(PinPoint.PinType a, PinPoint.PinType b) {
            if (a == b) {
                return a;
            }

            return a switch {
                PinPoint.PinType.outside => (b switch {
                    PinPoint.PinType.outside => PinPoint.PinType.outside,
                    PinPoint.PinType.in_out => PinPoint.PinType.outside,
                    PinPoint.PinType.inside => PinPoint.PinType.out_in,
                    PinPoint.PinType.out_in => PinPoint.PinType.out_in,
                    _ => PinPoint.PinType.nil
                }),
                PinPoint.PinType.out_in => (b switch {
                    PinPoint.PinType.outside => PinPoint.PinType.outside,
                    PinPoint.PinType.in_out => PinPoint.PinType.outside,
                    PinPoint.PinType.inside => PinPoint.PinType.out_in,
                    PinPoint.PinType.out_in => PinPoint.PinType.out_in,
                    _ => PinPoint.PinType.nil
                }),
                PinPoint.PinType.inside => (b switch {
                    PinPoint.PinType.inside => PinPoint.PinType.inside,
                    PinPoint.PinType.out_in => PinPoint.PinType.inside,
                    PinPoint.PinType.outside => PinPoint.PinType.in_out,
                    PinPoint.PinType.in_out => PinPoint.PinType.in_out,
                    _ => PinPoint.PinType.nil
                }),
                PinPoint.PinType.in_out => (b switch {
                    PinPoint.PinType.inside => PinPoint.PinType.inside,
                    PinPoint.PinType.out_in => PinPoint.PinType.inside,
                    PinPoint.PinType.outside => PinPoint.PinType.in_out,
                    PinPoint.PinType.in_out => PinPoint.PinType.in_out,
                    _ => PinPoint.PinType.nil
                }),
                _ => PinPoint.PinType.nil
            };
        }

    }


}