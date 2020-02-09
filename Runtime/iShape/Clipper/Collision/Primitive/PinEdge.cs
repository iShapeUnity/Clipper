namespace iShape.Clipper.Collision.Primitive {

    internal struct PinEdge {
        internal PinPoint v0;
        internal PinPoint v1;
        private readonly bool isDirect;

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

            switch (a) {
                case PinPoint.PinType.outside:
                case PinPoint.PinType.out_in:
                    switch (b) {
                        case PinPoint.PinType.outside:
                        case PinPoint.PinType.in_out:
                            return PinPoint.PinType.outside;
                        case PinPoint.PinType.inside:
                        case PinPoint.PinType.out_in:
                            return PinPoint.PinType.out_in;
                        default:
                            return PinPoint.PinType.nil;
                    }
                case PinPoint.PinType.inside:
                case PinPoint.PinType.in_out:
                    switch (b) {
                        case PinPoint.PinType.inside:
                        case PinPoint.PinType.out_in:
                            return PinPoint.PinType.inside;
                        case PinPoint.PinType.outside:
                        case PinPoint.PinType.in_out:
                            return PinPoint.PinType.in_out;
                        default:
                            return PinPoint.PinType.nil;
                    }
                default:
                    return PinPoint.PinType.nil;
            }
        }

    }


}