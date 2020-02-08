using iShape.Clipper.Solver;
using iShape.Geometry;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class BiteTests {
        
        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = SubtractTestData.data[0];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, SubtractSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -10),
                new Vector2(5, 0),
                new Vector2(-5, 0),
                new Vector2(-5, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }
    }

}