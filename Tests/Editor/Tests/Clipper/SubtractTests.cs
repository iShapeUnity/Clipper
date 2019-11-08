using iShape.Clipper.Intersection;
using iShape.Clipper.Intersection.Primitive;
using iShape.Clipper.Shape;
using iShape.Geometry;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class SubtractTests {

        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = SubtractTestData.data[0];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Subtract(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, SubtractSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.GetPath(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -10f),
                new Vector2(5, 0f),
                new Vector2(-5, 0f),
                new Vector2(-5, -10f),
                new Vector2(-10, -10f),
                new Vector2(-10, 10f),
                new Vector2(10, 10f),
                new Vector2(10, -10f)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }
    }

}