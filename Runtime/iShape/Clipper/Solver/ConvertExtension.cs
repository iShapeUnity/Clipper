using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Util {

    internal static class ConvertExtension {
        // for inner use only 

        internal static DynamicPlainShape ToDynamicShape(this NativeArray<IntVector> self, bool isClockWise, Allocator allocator) {
            var layouts = new NativeArray<PathLayout>(1, allocator) {[0] = new PathLayout(0, self.Length, isClockWise)};
            return new DynamicPlainShape(self, layouts);
        }

        internal static PlainShapeList ToShapeList(this DynamicPlainShape shape, Allocator allocator) {
            var segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, shape.layouts.Count)};
            return new PlainShapeList(shape.points.Convert(), shape.layouts.Convert(), segments);
        }
    }

}