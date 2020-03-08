using iShape.Clipper.Collision.Primitive;

namespace iShape.Clipper.Collision.Navigation {

    internal struct Cursor {
        internal static readonly Cursor empty = new Cursor(0, -1);

        internal readonly PinPoint.PinType type;
        internal readonly int index;

        internal bool isNotEmpty => index >= 0;
        internal bool isEmpty => index < 0;

        internal Cursor(PinPoint.PinType type, int index) {
            this.type = type;
            this.index = index;
        }

        public static bool operator ==(Cursor left, Cursor right) {
            return left.index == right.index;
        }

        public static bool operator !=(Cursor left, Cursor right) {
            return left.index != right.index;
        }

        private bool Equals(Cursor other) {
            return type == other.type && index == other.index;
        }

        public override bool Equals(object obj) {
            return obj is Cursor other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return 10 * index + (int) type;
            }
        }
    }

}