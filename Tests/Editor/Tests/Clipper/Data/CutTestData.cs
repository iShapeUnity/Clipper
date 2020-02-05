using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper.Data {

    internal struct CutTestData {

        internal struct TestData {

            internal readonly PlainShape shape;
            internal readonly NativeArray<IntVector> path;

            internal TestData(Vector2[] hull, Vector2[][] holes, Vector2[] path) {
                var iHull = IntGeom.DefGeom.Int(hull);
                var iHoles = IntGeom.DefGeom.Int(holes);
                var iShape = new IntShape(iHull, iHoles);
                this.shape = new PlainShape(iShape, Allocator.Persistent);
                var iPath = IntGeom.DefGeom.Int(path);
                this.path = new NativeArray<IntVector>(iPath, Allocator.Persistent);
            }
        }


        internal static readonly TestData[] data = {
            new TestData(new[] {
                    new Vector2(-15, -15),
                    new Vector2(-15, 15),
                    new Vector2(15, 15),
                    new Vector2(15, -15)
                }, new[] {
                    new[] {
                        new Vector2(-10, 10),
                        new Vector2(-10, -10),
                        new Vector2(10, -10),
                        new Vector2(10, 10)
                    }
                }, new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(5, -5),
                    new Vector2(5, 5)
                }
            )
        };
    }

}