using iShape.Clipper.Solver;
using iShape.Geometry;
using iShape.Geometry.Container;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class BiteTests {
        
        private const Allocator allocator = Allocator.Temp;
        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = BiteTestData.data[0].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();
            
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
            var data = BiteTestData.data[1].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

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
            var data = BiteTestData.data[2].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

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
        
        [Test]
        public void Test_03() {
            var data = BiteTestData.data[3].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true), 
                    new PathLayout(4, 10, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15, 10),
                        new Vector2(15, 10),
                        new Vector2(15, -10),
                        new Vector2(-15, -10),
                        new Vector2(5, 0),
                        new Vector2(10, 0),
                        new Vector2(10, -5),
                        new Vector2(0, -5),
                        new Vector2(-5, -5),
                        new Vector2(-5, 0),
                        new Vector2(-10, 0),
                        new Vector2(-10, 5),
                        new Vector2(0, 5),
                        new Vector2(5, 5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5, 0),
                        new Vector2(0, 0),
                        new Vector2(0, 5),
                        new Vector2(5, 5),
                        new Vector2(0, 0),
                        new Vector2(0, -5),
                        new Vector2(-5, -5),
                        new Vector2(-5, 0)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_04() {
            var data = BiteTestData.data[4].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 8, false),
                    new PathLayout(12, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, 20),
                        new Vector2(10, 20),
                        new Vector2(10, 5),
                        new Vector2(-10, 5),
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15),
                        new Vector2(-10, 0),
                        new Vector2(10, 0),
                        new Vector2(10, -15),
                        new Vector2(-10, -15),
                        new Vector2(-5, -5),
                        new Vector2(-5, -10),
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(-30, 35),
                        new Vector2(30, 35),
                        new Vector2(30, -35),
                        new Vector2(-30, -35),
                        new Vector2(10, -25),
                        new Vector2(20, -25),
                        new Vector2(20, 25),
                        new Vector2(-20, 25),
                        new Vector2(-20, -25),
                        new Vector2(-10, -25),
                        new Vector2(-10, -20),
                        new Vector2(10, -20),
                        new Vector2(-5, -25),
                        new Vector2(-5, -30),
                        new Vector2(5, -30),
                        new Vector2(5, -25)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 24, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, -15),
                        new Vector2(-15, -15),
                        new Vector2(-15, -20),
                        new Vector2(-10, -20),
                        new Vector2(-10, -25),
                        new Vector2(-20, -25),
                        new Vector2(-20, 25),
                        new Vector2(20, 25),
                        new Vector2(20, -25),
                        new Vector2(10, -25),
                        new Vector2(10, -20),
                        new Vector2(15, -20),
                        new Vector2(15, -15),
                        new Vector2(10, -15),
                        new Vector2(10, 0),
                        new Vector2(15, 0),
                        new Vector2(15, 5),
                        new Vector2(10, 5),
                        new Vector2(10, 20),
                        new Vector2(-10, 20),
                        new Vector2(-10, 5),
                        new Vector2(-15, 5),
                        new Vector2(-15, 0),
                        new Vector2(-10, 0)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_05() {
            var data = BiteTestData.data[5].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(0, 8, true),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 12, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, 20),
                        new Vector2(0, 20),
                        new Vector2(0, 15),
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(0, 10),
                        new Vector2(0, 5),
                        new Vector2(-10, 5),
                        new Vector2(-10, 0),
                        new Vector2(0, 0),
                        new Vector2(0, -5),
                        new Vector2(-5, -5),
                        new Vector2(-5, -10),
                        new Vector2(0, -10),
                        new Vector2(0, -15),
                        new Vector2(-10, -15),
                        new Vector2(-30, 35),
                        new Vector2(30, 35),
                        new Vector2(30, -40),
                        new Vector2(-30, -40),
                        new Vector2(0, -35),
                        new Vector2(20, -35),
                        new Vector2(20, 25),
                        new Vector2(-20, 25),
                        new Vector2(-20, -25),
                        new Vector2(-10, -25),
                        new Vector2(-10, -20),
                        new Vector2(0, -20),
                        new Vector2(0, -25),
                        new Vector2(-5, -25),
                        new Vector2(-5, -30),
                        new Vector2(0, -30)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 36, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(0, -30),
                        new Vector2(5, -30),
                        new Vector2(5, -25),
                        new Vector2(0, -25),
                        new Vector2(0, -20),
                        new Vector2(15, -20),
                        new Vector2(15, -15),
                        new Vector2(0, -15),
                        new Vector2(0, -10),
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(0, -5),
                        new Vector2(0, 0),
                        new Vector2(15, 0),
                        new Vector2(15, 5),
                        new Vector2(0, 5),
                        new Vector2(0, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15),
                        new Vector2(0, 15),
                        new Vector2(0, 20),
                        new Vector2(-10, 20),
                        new Vector2(-10, 5),
                        new Vector2(-15, 5),
                        new Vector2(-15, 0),
                        new Vector2(-10, 0),
                        new Vector2(-10, -15),
                        new Vector2(-15, -15),
                        new Vector2(-15, -20),
                        new Vector2(-10, -20),
                        new Vector2(-10, -25),
                        new Vector2(-20, -25),
                        new Vector2(-20, 25),
                        new Vector2(20, 25),
                        new Vector2(20, -35),
                        new Vector2(0, -35)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_06() {
            var data = BiteTestData.data[6].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),new PathLayout[0]);

            Assert.AreEqual(solution.mainList.points.ToArray(),new IntVector[0]);

            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, -10),
                        new Vector2(-10, 10),
                        new Vector2(10, 10),
                        new Vector2(10, -10)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_07() {
            var data = BiteTestData.data[7].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),new PathLayout[0]);

            Assert.AreEqual(solution.mainList.points.ToArray(),new IntVector[0]);

            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, -10),
                        new Vector2(-10, 10),
                        new Vector2(10, 10),
                        new Vector2(10, -10)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_08() {
            var data = BiteTestData.data[8].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),new PathLayout[0]);

            Assert.AreEqual(solution.mainList.points.ToArray(),new IntVector[0]);

            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, -10),
                        new Vector2(-10, 10),
                        new Vector2(10, 10),
                        new Vector2(10, -10),
                        new Vector2(5, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(5, -5)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_09() {
            var data = BiteTestData.data[9].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5, 10),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 10),
                        new Vector2(10, 10),
                        new Vector2(10, -10),
                        new Vector2(-10, -10),
                        new Vector2(-10, 10)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5, 10),
                        new Vector2(5, -5),
                        new Vector2(-5, -5),
                        new Vector2(-5, 10)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_10() {
            var data = BiteTestData.data[10].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5, 0),
                        new Vector2(0, 0),
                        new Vector2(0, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, 10),
                        new Vector2(10, 10),
                        new Vector2(10, -15),
                        new Vector2(-5, -15),
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(0, -5),
                        new Vector2(0, -10)
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
                        new Vector2(0, 5),
                        new Vector2(0, 0),
                        new Vector2(-5, 0)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_11() {
            var data = BiteTestData.data[11].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5 , 0),
                        new Vector2(10, 0),
                        new Vector2(10, -15),
                        new Vector2(-5, -15),
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(0, -5),
                        new Vector2(0, -10),
                        new Vector2(10, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, 10),
                        new Vector2(10, 10)
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
                        new Vector2(10, 5),
                        new Vector2(10, 0),
                        new Vector2(-5, 0)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_12() {
            var data = BiteTestData.data[12].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 16, true),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false),
                    new PathLayout(24, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(10, -5),
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(10, -10),
                        new Vector2(10, -15),
                        new Vector2(20, -15),
                        new Vector2(20, -25),
                        new Vector2(-20, -25),
                        new Vector2(-20, 30),
                        new Vector2(20, 30),
                        new Vector2(20, 20),
                        new Vector2(10, 20),
                        new Vector2(10, 15),
                        new Vector2(-15, 15),
                        new Vector2(-15, 10),
                        new Vector2(10, 10),
                        new Vector2(-5, 25),
                        new Vector2(-5, 20),
                        new Vector2(5, 20),
                        new Vector2(5, 25),
                        new Vector2(-5, 5),
                        new Vector2(-5, 0),
                        new Vector2(5, 0),
                        new Vector2(5, 5),
                        new Vector2(-5, -15),
                        new Vector2(-5, -20),
                        new Vector2(5, -20),
                        new Vector2(5, -15)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 12, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(10, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5),
                        new Vector2(10, -5),
                        new Vector2(10, 10),
                        new Vector2(15, 10),
                        new Vector2(15, 15),
                        new Vector2(10, 15),
                        new Vector2(10, 20),
                        new Vector2(20, 20),
                        new Vector2(20, -15),
                        new Vector2(10, -15)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_13() {
            var data = BiteTestData.data[13].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(15, 10),
                        new Vector2(15, 15),
                        new Vector2(-15, 15),
                        new Vector2(-15, 10),
                        new Vector2(-20, 10),
                        new Vector2(-20, 30),
                        new Vector2(20, 30),
                        new Vector2(20, 10),
                        new Vector2(-5, 25),
                        new Vector2(-5, 20),
                        new Vector2(5, 20),
                        new Vector2(5, 25),
                        new Vector2(-20, -10),
                        new Vector2(20, -10),
                        new Vector2(20, -25),
                        new Vector2(-20, -25),
                        new Vector2(-5, -15),
                        new Vector2(-5, -20),
                        new Vector2(5, -20),
                        new Vector2(5, -15)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(15, -10),
                        new Vector2(15, -5),
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(-20, -10),
                        new Vector2(-20, 10),
                        new Vector2(20, 10),
                        new Vector2(20, -10),
                        new Vector2(-5, 5),
                        new Vector2(-5, 0),
                        new Vector2(5, 0),
                        new Vector2(5, 5)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_14() {
            var data = BiteTestData.data[14].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 18, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(), 
                new[] {
                    new IntVector(75715, -50000),
                    new IntVector(-150000, -50000),
                    new IntVector(-150000, -100000),
                    new IntVector(150000, -100000),
                    new IntVector(150000, -92623),
                    new IntVector(200000, -121311),
                    new IntVector(200000, -150000),
                    new IntVector(-200000, -150000),
                    new IntVector(-200000, 200000),
                    new IntVector(200000, 200000),
                    new IntVector(200000, 171311),
                    new IntVector(-11428, 50000),
                    new IntVector(-50000, 50000),
                    new IntVector(-50000, 27869),
                    new IntVector(-55000, 25000),
                    new IntVector(-50000, 22131),
                    new IntVector(-50000, 0),
                    new IntVector(-11428, 0)
                }
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 9, true),
                    new PathLayout(9, 3, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                new[] {
                    new IntVector(150000, -92623),
                    new IntVector(150000, -50000),
                    new IntVector(75715, -50000),
                    new IntVector(-11428, 0),
                    new IntVector(50000, 0),
                    new IntVector(50000, 50000),
                    new IntVector(-11428, 50000),
                    new IntVector(200000, 171311),
                    new IntVector(200000, -121311),
                    new IntVector(-50000, 27869),
                    new IntVector(-50000, 22131),
                    new IntVector(-55000, 25000)
                }
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_15() {
            var data = BiteTestData.data[15].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 22, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(), 
                new[] {
                    new IntVector(75715, -50000),
                    new IntVector(-150000, -50000),
                    new IntVector(-150000, -100000),
                    new IntVector(150000, -100000),
                    new IntVector(150000, -92623),
                    new IntVector(200000, -121311),
                    new IntVector(200000, -150000),
                    new IntVector(-200000, -150000),
                    new IntVector(-200000, 200000),
                    new IntVector(200000, 200000),
                    new IntVector(200000, -110000),
                    new IntVector(150000, -70000),
                    new IntVector(150000, -50000),
                    new IntVector(125000, -50000),
                    new IntVector(50000, 10000),
                    new IntVector(50000, 50000),
                    new IntVector(-50000, 50000),
                    new IntVector(-50000, 27273),
                    new IntVector(-55000, 25000),
                    new IntVector(-50000, 22131),
                    new IntVector(-50000, 0),
                    new IntVector(-11428, 0)
                }
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 5, true),
                    new PathLayout(5, 3, true),
                    new PathLayout(8, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                new[] {
                    new IntVector(125000, -50000),
                    new IntVector(75715, -50000),
                    new IntVector(-11428, 0),
                    new IntVector(50000, 0),
                    new IntVector(50000, 10000),
                    new IntVector(-50000, 27273),
                    new IntVector(-50000, 22131),
                    new IntVector(-55000, 25000),
                    new IntVector(150000, -92623),
                    new IntVector(150000, -70000),
                    new IntVector(200000, -110000),
                    new IntVector(200000, -121311)
                }
            );

            solution.Dispose();
        }
        
                [Test]
        public void Test_16() {
            var data = BiteTestData.data[16].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, true),
                    new PathLayout(12, 4, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(0, -5),
                        new Vector2(0, -10),
                        new Vector2(-10, -10),
                        new Vector2(-10, 10),
                        new Vector2(0, 10),
                        new Vector2(0, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(10, -5),
                        new Vector2(10, -10),
                        new Vector2(5, 5),
                        new Vector2(5, 10),
                        new Vector2(10, 10),
                        new Vector2(10, 5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5, -5),
                        new Vector2(5, -10),
                        new Vector2(0, -10),
                        new Vector2(0, -5),
                        new Vector2(5, 10),
                        new Vector2(5, 5),
                        new Vector2(0, 5),
                        new Vector2(0, 10)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_17() {
            var data = BiteTestData.data[17].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 16, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(0, -5),
                        new Vector2(0, -15),
                        new Vector2(5, -15),
                        new Vector2(5, -5),
                        new Vector2(10, -5),
                        new Vector2(10, -20),
                        new Vector2(-10, -20),
                        new Vector2(-10, 20),
                        new Vector2(10, 20),
                        new Vector2(10, 5),
                        new Vector2(5, 5),
                        new Vector2(5, 15),
                        new Vector2(0, 15),
                        new Vector2(0, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5, -5),
                        new Vector2(5, -15),
                        new Vector2(0, -15),
                        new Vector2(0, -5),
                        new Vector2(0, 5),
                        new Vector2(0, 15),
                        new Vector2(5, 15),
                        new Vector2(5, 5)

                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_18() {
            var data = BiteTestData.data[18].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 20, true)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(0, 15),
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(0, 10),
                        new Vector2(0, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(0, -5),
                        new Vector2(0, -20),
                        new Vector2(15, -20),
                        new Vector2(15, -5),
                        new Vector2(20, -5),
                        new Vector2(20, -25),
                        new Vector2(-10, -25),
                        new Vector2(-10, 25),
                        new Vector2(20, 25),
                        new Vector2(20, 5),
                        new Vector2(15, 5),
                        new Vector2(15, 20),
                        new Vector2(0, 20)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 8, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(15, -5),
                        new Vector2(15, -20),
                        new Vector2(0, -20),
                        new Vector2(0, -5),
                        new Vector2(5, -10),
                        new Vector2(5, -15),
                        new Vector2(10, -15),
                        new Vector2(10, -10),
                        new Vector2(0, 10),
                        new Vector2(10, 10),
                        new Vector2(10, 15),
                        new Vector2(0, 15),
                        new Vector2(0, 20),
                        new Vector2(15, 20),
                        new Vector2(15, 5),
                        new Vector2(0, 5)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_19() {
            var data = BiteTestData.data[19].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 8, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(10, -10),
                        new Vector2(-10, -10),
                        new Vector2(-10, 5),
                        new Vector2(10, 5),
                        new Vector2(-5, 0),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 0),
                        new Vector2(-25, 20),
                        new Vector2(25, 20),
                        new Vector2(25, -20),
                        new Vector2(-25, -20),
                        new Vector2(-15, -15),
                        new Vector2(15, -15),
                        new Vector2(15, 5),
                        new Vector2(20, 5),
                        new Vector2(20, 15),
                        new Vector2(-20, 15),
                        new Vector2(-20, 5),
                        new Vector2(-15, 5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(10, 5),
                        new Vector2(15, 5),
                        new Vector2(15, -15),
                        new Vector2(-15, -15),
                        new Vector2(-15, 5),
                        new Vector2(-10, 5),
                        new Vector2(-10, -10),
                        new Vector2(10, -10)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_20() {
            var data = BiteTestData.data[20].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 12, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, 20),
                        new Vector2(10, 20),
                        new Vector2(10, 5),
                        new Vector2(-10, 5),
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15),
                        new Vector2(10, -20),
                        new Vector2(-10, -20),
                        new Vector2(-10, -5),
                        new Vector2(10, -5),
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(5, -15),
                        new Vector2(5, -10),
                        new Vector2(-25, 30),
                        new Vector2(25, 30),
                        new Vector2(25, -30),
                        new Vector2(-25, -30),
                        new Vector2(-15, -25),
                        new Vector2(15, -25),
                        new Vector2(15, -5),
                        new Vector2(20, -5),
                        new Vector2(20, 5),
                        new Vector2(15, 5),
                        new Vector2(15, 25),
                        new Vector2(-15, 25),
                        new Vector2(-15, 5),
                        new Vector2(-20, 5),
                        new Vector2(-20, -5),
                        new Vector2(-15, -5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 8, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, 5),
                        new Vector2(-15, 5),
                        new Vector2(-15, 25),
                        new Vector2(15, 25),
                        new Vector2(15, 5),
                        new Vector2(10, 5),
                        new Vector2(10, 20),
                        new Vector2(-10, 20),
                        new Vector2(-15, -5),
                        new Vector2(-10, -5),
                        new Vector2(-10, -20),
                        new Vector2(10, -20),
                        new Vector2(10, -5),
                        new Vector2(15, -5),
                        new Vector2(15, -25),
                        new Vector2(-15, -25)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_21() {
            var data = BiteTestData.data[21].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15, 10),
                        new Vector2(15, 10),
                        new Vector2(15, -10),
                        new Vector2(-15, -10),
                        new Vector2(10, 5),
                        new Vector2(10, -5),
                        new Vector2(-10, -5),
                        new Vector2(-10, 5)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5, -5),
                        new Vector2(5, 5),
                        new Vector2(10, 5),
                        new Vector2(10, -5),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(-10, -5),
                        new Vector2(-10, 5)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_22() {
            var data = BiteTestData.data[22].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 22, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(10, -10),
                        new Vector2(-10, -10),
                        new Vector2(-10, 5),
                        new Vector2(10, 5),
                        new Vector2(-5, 0),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 0),
                        new Vector2(-25, 20),
                        new Vector2(25, 20),
                        new Vector2(25, -20),
                        new Vector2(-25, -20),
                        new Vector2(10, -15),
                        new Vector2(20, -15),
                        new Vector2(20, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5),
                        new Vector2(20, -5),
                        new Vector2(20, 0),
                        new Vector2(15, 0),
                        new Vector2(15, 5),
                        new Vector2(20, 5),
                        new Vector2(20, 15),
                        new Vector2(-20, 15),
                        new Vector2(-20, 5),
                        new Vector2(-15, 5),
                        new Vector2(-15, 0),
                        new Vector2(-20, 0),
                        new Vector2(-20, -5),
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(-20, -10),
                        new Vector2(-20, -15),
                        new Vector2(-10, -15)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, true),
                    new PathLayout(8, 4, true),
                    new PathLayout(12, 4, true),
                    new PathLayout(16, 4, true),
                    new PathLayout(20, 4, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(10, 5),
                        new Vector2(15, 5),
                        new Vector2(15, 0),
                        new Vector2(10, 0),
                        new Vector2(15, -10),
                        new Vector2(10, -10),
                        new Vector2(10, -5),
                        new Vector2(15, -5),
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(-10, -15),
                        new Vector2(-10, -10),
                        new Vector2(-10, 0),
                        new Vector2(-15, 0),
                        new Vector2(-15, 5),
                        new Vector2(-10, 5),
                        new Vector2(-10, -10),
                        new Vector2(-15, -10),
                        new Vector2(-15, -5),
                        new Vector2(-10, -5),
                        new Vector2(5, -15),
                        new Vector2(5, -10),
                        new Vector2(10, -10),
                        new Vector2(10, -15)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_23() {
            var data = BiteTestData.data[23].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 20, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-10, 20),
                        new Vector2(10, 20),
                        new Vector2(10, 5),
                        new Vector2(-10, 5),
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15),
                        new Vector2(10, -20),
                        new Vector2(-10, -20),
                        new Vector2(-10, -5),
                        new Vector2(10, -5),
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(5, -15),
                        new Vector2(5, -10),
                        new Vector2(-25, 30),
                        new Vector2(25, 30),
                        new Vector2(25, -30),
                        new Vector2(-25, -30),
                        new Vector2(-15, 5),
                        new Vector2(-20, 5),
                        new Vector2(-20, -5),
                        new Vector2(-15, -5),
                        new Vector2(-15, -25),
                        new Vector2(15, -25),
                        new Vector2(15, -15),
                        new Vector2(20, -15),
                        new Vector2(20, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5),
                        new Vector2(20, -5),
                        new Vector2(20, 5),
                        new Vector2(15, 5),
                        new Vector2(15, 25),
                        new Vector2(-15, 25),
                        new Vector2(-15, 15),
                        new Vector2(-20, 15),
                        new Vector2(-20, 10),
                        new Vector2(-15, 10)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, true),
                    new PathLayout(8, 4, true),
                    new PathLayout(12, 4, true),
                    new PathLayout(16, 6, true),
                    new PathLayout(22, 4, true),
                    new PathLayout(26, 4, true),
                    new PathLayout(30, 6, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15, 10),
                        new Vector2(-10, 10),
                        new Vector2(-10, 5),
                        new Vector2(-15, 5),
                        new Vector2(-10, -25),
                        new Vector2(-10, -20),
                        new Vector2(-5, -20),
                        new Vector2(-5, -25),
                        new Vector2(15, -10),
                        new Vector2(10, -10),
                        new Vector2(10, -5),
                        new Vector2(15, -5),
                        new Vector2(-10, -20),
                        new Vector2(-15, -20),
                        new Vector2(-15, -5),
                        new Vector2(-10, -5),
                        new Vector2(10, -15),
                        new Vector2(15, -15),
                        new Vector2(15, -25),
                        new Vector2(5, -25),
                        new Vector2(5, -20),
                        new Vector2(10, -20),
                        new Vector2(10, 25),
                        new Vector2(10, 20),
                        new Vector2(5, 20),
                        new Vector2(5, 25),
                        new Vector2(10, 20),
                        new Vector2(15, 20),
                        new Vector2(15, 5),
                        new Vector2(10, 5),
                        new Vector2(-10, 15),
                        new Vector2(-15, 15),
                        new Vector2(-15, 25),
                        new Vector2(-5, 25),
                        new Vector2(-5, 20),
                        new Vector2(-10, 20)
                    })
            );

            solution.Dispose();
        }
        
        [Test]
        public void Test_24() {
            var data = BiteTestData.data[24].Allocate(allocator);
            var solution = data.shape.Bite(data.path, iGeom, allocator);
            data.Dispose();

            Assert.AreEqual(solution.isInteract, true);

            Assert.AreEqual(solution.mainList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 12, true),
                    new PathLayout(12, 4, false),
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 16, false)
                });

            Assert.AreEqual(solution.mainList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5, -10),
                        new Vector2(5, -7.5f),
                        new Vector2(-5, -7.5f),
                        new Vector2(-5, -10),
                        new Vector2(-10, -10),
                        new Vector2(-10, -5),
                        new Vector2(-5, -5),
                        new Vector2(-5, 0),
                        new Vector2(-10, 0),
                        new Vector2(-10, 5),
                        new Vector2(10, 5),
                        new Vector2(10, -10),
                        new Vector2(0, 0),
                        new Vector2(0, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 0),
                        new Vector2(-25, 20),
                        new Vector2(25, 20),
                        new Vector2(25, -20),
                        new Vector2(-25, -20),
                        new Vector2(15, -15),
                        new Vector2(15, 5),
                        new Vector2(20, 5),
                        new Vector2(20, 15),
                        new Vector2(-20, 15),
                        new Vector2(-20, 5),
                        new Vector2(-15, 5),
                        new Vector2(-15, 0),
                        new Vector2(-20, 0),
                        new Vector2(-20, -5),
                        new Vector2(-15, -5),
                        new Vector2(-15, -15),
                        new Vector2(-5, -15),
                        new Vector2(-5, -17.5f),
                        new Vector2(5, -17.5f),
                        new Vector2(5, -15)
                    })
            );
            
            Assert.AreEqual(solution.biteList.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 6, true),
                    new PathLayout(6, 4, true),
                    new PathLayout(10, 6, true)
                });

            Assert.AreEqual(solution.biteList.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(-15, -15),
                        new Vector2(-15, -5),
                        new Vector2(-10, -5),
                        new Vector2(-10, -10),
                        new Vector2(-10, 0),
                        new Vector2(-15, 0),
                        new Vector2(-15, 5),
                        new Vector2(-10, 5),
                        new Vector2(5, -15),
                        new Vector2(5, -10),
                        new Vector2(10, -10),
                        new Vector2(10, 5),
                        new Vector2(15, 5),
                        new Vector2(15, -15)
                    })
            );

            solution.Dispose();
        }
        
    }

}