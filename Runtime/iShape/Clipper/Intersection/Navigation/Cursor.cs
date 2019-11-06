using iShape.Clipper.Intersection.Primitive;

namespace iShape.Clipper.Intersection.Navigation {
    internal struct Cursor {

        internal static Cursor empty = new Cursor(0, -1);

        internal readonly PinPoint.PinType type;
        internal readonly int index;

        internal bool isNotEmpty => index >= 0;
        internal bool isEmpty => index < 0;

        internal Cursor(PinPoint.PinType type, int index) {
            this.type = type;
            this.index = index;
        }

        public static bool operator== (Cursor left, Cursor right) {
            return left.index == right.index;
        }

        public static bool operator!= (Cursor left, Cursor right) {
            return left.index != right.index;
        }
    }
}