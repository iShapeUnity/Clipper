using iShape.Geometry;

namespace iShape.Clipper.Util {

    public static class Edge {
        
        public static bool IsContain(IntVector a, IntVector b, IntVector p) {
            bool xTest = a.x >= b.x && a.x >= p.x && p.x >= b.x || a.x < b.x && b.x >= p.x && p.x >= a.x;
            bool yTest = a.y >= b.y && a.y >= p.y && p.y >= b.y || a.y < b.y && b.y >= p.y && p.y >= a.y;
            if (xTest && yTest) {
                long m0 = (p.y - a.y) * (b.x - a.x);
                long m1 = (b.y - a.y) * (p.x - a.x);
                return m0 == m1;
            }

            return false;
        }
        
        public static bool IsInRect(IntVector a, IntVector b, IntVector p) {
            bool xTest = a.x > b.x && a.x > p.x && p.x > b.x || a.x < b.x && b.x > p.x && p.x > a.x;
            bool yTest = a.y > b.y && a.y > p.y && p.y > b.y || a.y < b.y && b.y > p.y && p.y > a.y;
            return xTest && yTest || a.x == b.x && p.x == a.x && yTest || a.y == b.y && p.y == a.y && xTest;
        }
    }

}