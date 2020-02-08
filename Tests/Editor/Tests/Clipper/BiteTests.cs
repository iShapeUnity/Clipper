using iShape.Clipper.Solver;
using iShape.Geometry;
using iShape.Geometry.Container;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class BiteTests {
        
        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = BiteTestData.data[0];
            var solution = data.shape.Bite(data.path, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true), 
                    new PathLayout(4, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15, -15),
                        new Vector2(-15, 15),
                        new Vector2(15, 15),
                        new Vector2(15, -15),
                        new Vector2(10, 10),
                        new Vector2(10, -10),
                        new Vector2(-10, -10),
                        new Vector2(-10, 10)
                })
            );

            Assert.AreEqual(solution.biteList.layouts.ToArray(), new PathLayout[0]);
            Assert.AreEqual(solution.biteList.points.ToArray(), new IntVector[0]);

            solution.Dispose();
        }
        
        [Test]
        public void Test_01() {
            var data = BiteTestData.data[1];
            var solution = data.shape.Bite(data.path, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true), 
                    new PathLayout(4, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, -10),
                        new Vector2(-10, 10),
                        new Vector2(10, 10),
                        new Vector2(10, -10),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 5)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_02() {
            var data = BiteTestData.data[2];
            var solution = data.shape.Bite(data.path, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true), 
                    new PathLayout(4, 8, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15, 15),
                        new Vector2(15, 15),
                        new Vector2(15, -15),
                        new Vector2(-15, -15),
                        new Vector2(-5, -5),
                        new Vector2(-10, -5),
                        new Vector2(-10, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 5),
                        new Vector2(10, 5),
                        new Vector2(10, -10),
                        new Vector2(-5, -10)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 6, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 5),
                        new Vector2(10, 5),
                        new Vector2(10, -10),
                        new Vector2(-5, -10)
                    })
            );

            solution.Dispose();
        }
    }

}