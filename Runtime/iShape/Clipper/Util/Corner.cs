using iShape.Geometry;

namespace iShape.Clipper.Util {
    public struct Corner {

        public enum Result {
            onBoarder,
            contain,
            absent
        }
        
        private readonly IntVector o;
        private readonly IntVector a;
        private readonly IntVector b;
        
        private readonly DBVector d0;
        private readonly DBVector da;
        private readonly DBVector db;
        private readonly bool isInnerCornerCW;

        public Corner(DBVector d0, IntVector o, IntVector a, IntVector b) {
            this.d0 = d0;
            this.da = new DBVector(a);
            this.db = new DBVector(b);
            
            this.o = o;
            this.a = a;
            this.b = b;

            this.isInnerCornerCW = Corner.isClockWise(this.a, this.o, this.b) == 1;
        }
        
        public Corner(IntVector o, IntVector a, IntVector b) {
            this.d0 = new DBVector(o);
            this.da = new DBVector(a);
            this.db = new DBVector(b);
            
            this.o = o;
            this.a = a;
            this.b = b;

            this.isInnerCornerCW = Corner.isClockWise(this.a, this.o, this.b) == 1;
        }

        public Result IsBetweenIntVersion(IntVector p, bool clockwise) {
            int aop = Corner.isClockWise(a, o, p);
            int bop = Corner.isClockWise(b, o, p);
            if (aop == 0 || bop == 0) {
                if (aop == 0 && bop == 0) {
                    return Result.onBoarder;
                }

                long dotProduct;
                if (aop == 0) {
                    var ao = a - o;
                    var po = p - o;
                    dotProduct = ao.x * po.x + ao.y * po.y;
                } else {
                    var bo = b - o;
                    var po = p - o;
                    dotProduct = bo.x * po.x + bo.y * po.y;
                }
                if (dotProduct > 0) {
                    return Result.onBoarder;
                } else if (clockwise == this.isInnerCornerCW) {
                    return Result.absent;
                } else {
                    return Result.contain;
                }
            }

            bool isClockWiseAOP = aop == 1;
            bool isClockWiseBOP = bop == 1;

            bool isInner = isClockWiseAOP != isClockWiseBOP && this.isInnerCornerCW == isClockWiseAOP;

            if (this.isInnerCornerCW != clockwise) {
                isInner = !isInner;
            }
            if (isInner) {
                return Result.contain;
            } else {
                return Result.absent;
            }
            
        }
        
        public Result IsBetweenDoubleVersion(IntVector p, bool clockwise) {
            int aop = Corner.isClockWise(a, o, p);
            int bop = Corner.isClockWise(b, o, p);
            if (aop == 0 || bop == 0) {
                if (aop == 0 && bop == 0) {
                    return Result.onBoarder;
                }

                long dotProduct;
                if (aop == 0) {
                    var ao = a - o;
                    var po = p - o;
                    dotProduct = ao.x * po.x + ao.y * po.y;
                } else {
                    var bo = b - o;
                    var po = p - o;
                    dotProduct = bo.x * po.x + bo.y * po.y;
                }
                if (dotProduct > 0) {
                    return Result.onBoarder;
                } else if (clockwise == this.isInnerCornerCW) {
                    return Result.absent;
                } else {
                    return Result.contain;
                }
            }

            var dp = new DBVector(p);

            int dAOP = Corner.isClockWise(da, d0, dp);
            int dBOP = Corner.isClockWise(db, d0, dp);

            bool isClockWiseAOP = dAOP == 1;
            bool isClockWiseBOP = dBOP == 1;

            bool isInner = isClockWiseAOP != isClockWiseBOP && this.isInnerCornerCW == isClockWiseAOP;

            if (this.isInnerCornerCW != clockwise) {
                isInner = !isInner;
            }
            if (isInner) {
                return Result.contain;
            } else {
                return Result.absent;
            }
        }

        private static int isClockWise(IntVector a, IntVector b, IntVector c) {
            var m0 = (c.y - a.y) * (b.x - a.x);
            var m1 = (b.y - a.y) * (c.x - a.x);

            if (m0 < m1) {
                return -1;
            } else if (m0 > m1) {
                return 1;
            }

            return 0;
        }
        
        private static int isClockWise(DBVector a, DBVector b, DBVector c) {
            var m0 = (c.y - a.y) * (b.x - a.x);
            var m1 = (b.y - a.y) * (c.x - a.x);

            if (m0 < m1) {
                return -1;
            }
            
            if (m0 > m1) {
                return 1;
            }

            return 0;
        }

    }
}