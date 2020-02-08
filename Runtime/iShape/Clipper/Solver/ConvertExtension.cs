using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;

namespace iShape.Clipper.Util {

    static class ConvertExtension {
        // for inner use only 
        internal static PlainShape ToShape(this NativeArray<IntVector> self, bool isClockWise, Allocator allocator) {
            var layouts = new NativeArray<PathLayout>(1, allocator) {[0] = new PathLayout(0, self.Length, isClockWise)};
            return new PlainShape(self, layouts);
        }

        internal static DynamicPlainShape ToDynamicShape(this NativeArray<IntVector> self, bool isClockWise, Allocator allocator) {
            var layouts = new NativeArray<PathLayout>(1, allocator) {[0] = new PathLayout(0, self.Length, isClockWise)};
            return new DynamicPlainShape(self, layouts);
        }

        internal static DynamicPlainShape ToDynamicShape(this PlainShape shape) {
            return new DynamicPlainShape(shape.points, shape.layouts);
        }

        internal static PlainShapeList ToShapeList(this NativeArray<IntVector> self, bool isClockWise, Allocator allocator) {
            var layouts = new NativeArray<PathLayout>(1, allocator) {[0] = new PathLayout(0, self.Length, isClockWise)};
            var segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, 1)};
            return new PlainShapeList(self, layouts, segments);
        }

        internal static DynamicPlainShapeList ToDynamicShapeList(this NativeArray<IntVector> self, bool isClockWise, Allocator allocator) {
            var layouts = new NativeArray<PathLayout>(1, allocator) {[0] = new PathLayout(0, self.Length, isClockWise)};
            var segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, 1)};
            return new DynamicPlainShapeList(self, layouts, segments);
        }

        internal static PlainShapeList ToShapeList(this PlainShape shape, Allocator allocator) {
            var segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, 1)};
            return new PlainShapeList(shape.points, shape.layouts, segments);
        }

        internal static DynamicPlainShapeList ToDynamicShapeList(this PlainShape shape, Allocator allocator) {
            var segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, 1)};
            return new DynamicPlainShapeList(shape.points, shape.layouts, segments);
        }

        internal static PlainShapeList ToShapeList(this DynamicPlainShape shape, Allocator allocator) {
            var segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, 1)};
            return new PlainShapeList(shape.points.Convert(), shape.layouts.Convert(), segments);
        }
    }

}