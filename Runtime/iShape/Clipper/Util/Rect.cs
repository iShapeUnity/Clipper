using iShape.Geometry;

namespace iShape.Clipper.Util {

    public struct Rect {
        private long minX;
        private long minY;
        private long maxX;
        private long maxY;

        public Rect(long minX, long minY, long maxX, long maxY) {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public Rect(IntVector a, IntVector b) {
            if (b.x > a.x) {
                minX = a.x;
                maxX = b.x;
            } else {
                minX = b.x;
                maxX = a.x;
            }

            if (b.y > a.y) {
                minY = a.y;
                maxY = b.y;
            } else {
                minY = b.y;
                maxY = a.y;
            }
        }
        
        public Rect(IntVector[] points) {
            int n = points.Length;
            var p0 = points[0];
            minX = p0.x;
            minY = p0.y;
            maxX = p0.x;
            maxY = p0.y;
            for (int i = 0; i < n; ++i) {
                var p = points[i];
                if (minX > p.x) {
                    minX = p.x;
                }
                if (maxX < p.x) {
                    maxX = p.x;
                }
                if (minY > p.y) {
                    minY = p.y;
                }
                if (maxY < p.y) {
                    maxY = p.y;
                }
            }
        }

        public void Assimilate(IntVector p) {
            if (minX > p.x) {
                minX = p.x;
            }

            if (minY > p.y) {
                minY = p.y;
            }

            if (maxX < p.x) {
                maxX = p.x;
            }

            if (maxY < p.y) {
                maxY = p.y;
            }
        }

        public bool IsNotIntersecting(IntVector a, IntVector b) {
            return a.x < minX && b.x < minX || a.x > maxX && b.x > maxX ||
                   a.y < minY && b.y < minY || a.y > maxY && b.y > maxY;
        }

        public bool IsIntersecting(Rect box) {
            return !(maxX < box.minX || minX > box.maxX || maxY < box.minY || minY > box.maxY);
        }
        
        public bool IsNotIntersecting(Rect box) {
            return maxX < box.minX || minX > box.maxX || maxY < box.minY || minY > box.maxY;
        }
    }

}