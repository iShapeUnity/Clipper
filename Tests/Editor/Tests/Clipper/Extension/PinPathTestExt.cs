using iShape.Clipper.Collision.Primitive;
using iShape.Collections;
using iShape.Geometry;
using Unity.Collections;

namespace Tests.Clipper.Extension {

    public static class PinPathTestExt {

        public static IntVector[] Extract(this PinPath self, NativeArray<IntVector> points) {
            int n = points.Length;

            int length = self.GetLength(n);

            if (length < 2) {
                return new[] {self.v0.point, self.v1.point};
            }

            if (length == 2) {
                var middle = points[(self.v0.masterMileStone.index + 1) % n];
                return new[] {self.v0.point, middle, self.v1.point};
            }

            var path = new DynamicArray<IntVector>(length + 1, Allocator.Temp);
            path.Add(self.v0.point);
            var i = (self.v0.masterMileStone.index + 1) % n;
            int endIndex;
            if (self.v1.masterMileStone.offset != 0) {
                endIndex = self.v1.masterMileStone.index;
            } else {
                endIndex = (self.v1.masterMileStone.index - 1 + n) % n;
            }

            while (i != endIndex) {
                path.Add(points[i]);
                i = (i + 1) % n;
            }

            path.Add(points[endIndex]);

            path.Add(self.v1.point);

            return path.ConvertToArray();
        }
    }

}