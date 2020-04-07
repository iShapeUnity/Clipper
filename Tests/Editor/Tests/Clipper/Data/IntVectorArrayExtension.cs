using iShape.Geometry;
using UnityEngine;

namespace Tests.Clipper.Data {

    internal static class IntVectorArrayExtension {
        
        internal static Vector2[] toVectors(this IntVector[] self, long scale) {
            var result = new Vector2[self.Length];
            var geom = IntGeom.DefGeom;
            for (int i = 0; i < self.Length; ++i) {
                var p = self[i];
                float x = geom.Float(p.x * scale);
                float y = geom.Float(p.y * scale);
                result[i] = new Vector2(x, y);
            }

            return result;
        }
        
    }

}