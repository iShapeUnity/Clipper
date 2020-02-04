using iShape.Clipper.Collision.Primitive;
using Unity.Collections;

namespace iShape.Clipper.Collision.Sort {

    internal static class IndexMileStoneSortExt {

        internal static void Sort(this ref NativeArray<IndexMileStone> array) {
            int n = array.Length;
            int r = 2;
            int rank = 1;

            while (r <= n) {
                rank = r;
                r <<= 1;
            }
            rank -= 1;

            int jEnd = rank;
            int jStart = ((jEnd + 1) >> 1) - 1;

            while (jStart >= 0) {
                int k = jStart;
                while (k < jEnd) {
                    int j = k;

                    var a = array[j];
                    bool fallDown;
                    do {
                        fallDown = false;

                        int j0 = (j << 1) + 1;
                        int j1 = j0 + 1;

                        if (j1 < n) {
                            var a0 = array[j0];
                            var a1 = array[j1];

                            if (a.stone < a0.stone || a.stone < a1.stone) {
                                if (a0.stone > a1.stone) {
                                    array[j0] = a;
                                    array[j] = a0;
                                    j = j0;
                                } else {
                                    array[j1] = a;
                                    array[j] = a1;
                                    j = j1;
                                }

                                fallDown = j < rank;
                            }
                        } else if (j0 < n) {
                            var ax = array[j];
                            var a0 = array[j0];
                            if (ax.stone < a0.stone) {
                                array[j0] = ax;
                                array[j] = a0;
                            }
                        }
                    } while (fallDown);

                    ++k;
                }

                jEnd = jStart;
                jStart = ((jEnd + 1) >> 1) - 1;
            }

            while (n > 0) {
                int m = n - 1;

                var a = array[m];
                array[m] = array[0];
                array[0] = a;

                int j = 0;
                bool fallDown;
                do {
                    fallDown = false;

                    int j0 = (j << 1) + 1;
                    int j1 = j0 + 1;

                    if (j1 < m) {
                        var a0 = array[j0];
                        var a1 = array[j1];
                        fallDown = a.stone < a0.stone || a.stone < a1.stone;

                        if (fallDown) {
                            if (a0.stone > a1.stone) {
                                array[j0] = a;
                                array[j] = a0;
                                j = j0;
                            } else {
                                array[j1] = a;
                                array[j] = a1;
                                j = j1;
                            }
                        }
                    } else if (j0 < m) {
                        var ax = array[j];
                        var a0 = array[j0];
                        if (ax.stone < a0.stone) {
                            array[j0] = ax;
                            array[j] = a0;
                        }
                    }
                } while (fallDown);

                n = m;
            }
        }
    }
}