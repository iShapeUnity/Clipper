using iShape.Clipper.Solver;
using iShape.Geometry;
using iShape.Geometry.Container;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class ComplexUnionTests {
        private const Allocator allocator = Allocator.Temp;
        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = ComplexTestData.data[0].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();
            
            Assert.AreEqual(solution.nature, Solution.Nature.notOverlap);
            
            solution.Dispose();
        }

        [Test]
        public void Test_01() {
            var data = ComplexTestData.data[1].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();
            
            Assert.AreEqual(solution.nature, Solution.Nature.masterIncludeSlave);
            
            solution.Dispose();
        }

        [Test]
        public void Test_02() {
            var data = ComplexTestData.data[2].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 6, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15.0f, 15.0f),
                        new Vector2(15.0f, 15.0f),
                        new Vector2(15.0f, -15.0f),
                        new Vector2(-15.0f, -15.0f),
                        new Vector2(5.0f, 10.0f),
                        new Vector2(-10.0f, 10.0f),
                        new Vector2(-10.0f, -5.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(5.0f, 5.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_03() {
            var data = ComplexTestData.data[3].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(8, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(15.0f, 10.0f),
                        new Vector2(15.0f, -10.0f),
                        new Vector2(-15.0f, -10.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-10.0f, 0.0f),
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(10.0f, -5.0f),
                        new Vector2(10.0f, 0.0f),
                        new Vector2(5.0f, 0.0f),
                        new Vector2(5.0f, -5.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_04() {
            var data = ComplexTestData.data[4].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 4, false),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);
            Assert.AreEqual(mainShape.IsClockWise(5), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-30.0f, 35.0f),
                        new Vector2(30.0f, 35.0f),
                        new Vector2(30.0f, -35.0f),
                        new Vector2(-30.0f, -35.0f),
                        new Vector2(10.0f, 0.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-10.0f, 0.0f),
                        new Vector2(10.0f, -20.0f),
                        new Vector2(10.0f, -15.0f),
                        new Vector2(-10.0f, -15.0f),
                        new Vector2(-10.0f, -20.0f),
                        new Vector2(-5.0f, 15.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(5.0f, 10.0f),
                        new Vector2(5.0f, 15.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(-5.0f, -10.0f),
                        new Vector2(5.0f, -10.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(-5.0f, -25.0f),
                        new Vector2(-5.0f, -30.0f),
                        new Vector2(5.0f, -30.0f),
                        new Vector2(5.0f, -25.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_05() {
            var data = ComplexTestData.data[5].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 4, false),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);
            Assert.AreEqual(mainShape.IsClockWise(5), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-30.0f, 35.0f),
                        new Vector2(30.0f, 35.0f),
                        new Vector2(30.0f, -40.0f),
                        new Vector2(-30.0f, -40.0f),
                        new Vector2(-5.0f, 15.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(0.0f, 10.0f),
                        new Vector2(0.0f, 15.0f),
                        new Vector2(0.0f, 0.0f),
                        new Vector2(0.0f, 5.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-10.0f, 0.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(-5.0f, -10.0f),
                        new Vector2(0.0f, -10.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(0.0f, -20.0f),
                        new Vector2(0.0f, -15.0f),
                        new Vector2(-10.0f, -15.0f),
                        new Vector2(-10.0f, -20.0f),
                        new Vector2(-5.0f, -25.0f),
                        new Vector2(-5.0f, -30.0f),
                        new Vector2(0.0f, -30.0f),
                        new Vector2(0.0f, -25.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_06() {
            var data = ComplexTestData.data[6].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();
            
            Assert.AreEqual(solution.nature, Solution.Nature.slaveIncludeMaster);
            
            solution.Dispose();
        }

        [Test]
        public void Test_07() {
            var data = ComplexTestData.data[7].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();
            
            Assert.AreEqual(solution.nature, Solution.Nature.equal);
            
            solution.Dispose();
        }

        [Test]
        public void Test_08() {
            var data = ComplexTestData.data[8].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();
            
            Assert.AreEqual(solution.nature, Solution.Nature.slaveIncludeMaster);
            
            solution.Dispose();
        }

        [Test]
        public void Test_09() {
            var data = ComplexTestData.data[9].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(-5.0f, 15.0f),
                        new Vector2(5.0f, 15.0f),
                        new Vector2(5.0f, 10.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(10.0f, -10.0f),
                        new Vector2(-10.0f, -10.0f),
                        new Vector2(-10.0f, 10.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_10() {
            var data = ComplexTestData.data[10].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(-10.0f, 0.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(10.0f, -15.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(5.0f, -10.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(0.0f, -10.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_11() {
            var data = ComplexTestData.data[11].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 12, true),
                    new PathLayout(12, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(-10.0f, 0.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(15.0f, 5.0f),
                        new Vector2(15.0f, 0.0f),
                        new Vector2(10.0f, 0.0f),
                        new Vector2(10.0f, -15.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(5.0f, -10.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(0.0f, -10.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_12() {
            var data = ComplexTestData.data[12].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 4, false),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false),
                    new PathLayout(24, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);
            Assert.AreEqual(mainShape.IsClockWise(5), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(20.0f, 20.0f),
                        new Vector2(25.0f, 20.0f),
                        new Vector2(25.0f, -15.0f),
                        new Vector2(20.0f, -15.0f),
                        new Vector2(20.0f, -25.0f),
                        new Vector2(-20.0f, -25.0f),
                        new Vector2(-20.0f, 30.0f),
                        new Vector2(20.0f, 30.0f),
                        new Vector2(-15.0f, 15.0f),
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(10.0f, 15.0f),
                        new Vector2(-15.0f, -5.0f),
                        new Vector2(-15.0f, -10.0f),
                        new Vector2(10.0f, -10.0f),
                        new Vector2(10.0f, -5.0f),
                        new Vector2(-5.0f, 25.0f),
                        new Vector2(-5.0f, 20.0f),
                        new Vector2(5.0f, 20.0f),
                        new Vector2(5.0f, 25.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(5.0f, 0.0f),
                        new Vector2(5.0f, 5.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(-5.0f, -20.0f),
                        new Vector2(5.0f, -20.0f),
                        new Vector2(5.0f, -15.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_13() {
            var data = ComplexTestData.data[13].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 12, true),
                    new PathLayout(12, 4, false),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(20.0f, 10.0f),
                        new Vector2(25.0f, 10.0f),
                        new Vector2(25.0f, -10.0f),
                        new Vector2(20.0f, -10.0f),
                        new Vector2(20.0f, -25.0f),
                        new Vector2(-20.0f, -25.0f),
                        new Vector2(-20.0f, -10.0f),
                        new Vector2(-25.0f, -10.0f),
                        new Vector2(-25.0f, 10.0f),
                        new Vector2(-20.0f, 10.0f),
                        new Vector2(-20.0f, 30.0f),
                        new Vector2(20.0f, 30.0f),
                        new Vector2(-5.0f, 25.0f),
                        new Vector2(-5.0f, 20.0f),
                        new Vector2(5.0f, 20.0f),
                        new Vector2(5.0f, 25.0f),
                        new Vector2(-15.0f, 15.0f),
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(15.0f, 10.0f),
                        new Vector2(15.0f, 15.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(-5.0f, -20.0f),
                        new Vector2(5.0f, -20.0f),
                        new Vector2(5.0f, -15.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_14() {
            var data = ComplexTestData.data[14].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 3, false),
                    new PathLayout(11, 3, false),
                    new PathLayout(14, 5, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            
            Assert.AreEqual(mainShape.points.ToArray(),
                new[] {
                    new IntVector(200000, 171311),
                    new IntVector(250000, 200000),
                    new IntVector(250000, -150000),
                    new IntVector(200000, -121311),
                    new IntVector(200000, -150000),
                    new IntVector(-200000, -150000),
                    new IntVector(-200000, 200000),
                    new IntVector(200000, 200000),
                    new IntVector(-50000, 0),
                    new IntVector(-11429, 0),
                    new IntVector(-50000, 22131),
                    new IntVector(-50000, 50000),
                    new IntVector(-50000, 27869),
                    new IntVector(-11429, 50000),
                    new IntVector(-150000, -50000),
                    new IntVector(-150000, -100000),
                    new IntVector(150000, -100000),
                    new IntVector(150000, -92623),
                    new IntVector(75714, -50000)
                }
            );
            solution.Dispose();
        }

        [Test]
        public void Test_15() {
            var data = ComplexTestData.data[15].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 7, true),
                    new PathLayout(7, 3, false),
                    new PathLayout(10, 3, false),
                    new PathLayout(13, 3, false),
                    new PathLayout(16, 3, false),
                    new PathLayout(19, 5, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);
            Assert.AreEqual(mainShape.IsClockWise(5), false);
            
            Assert.AreEqual(mainShape.points.ToArray(),
                new[] {
                    new IntVector(200000, -110000),
                    new IntVector(250000, -150000),
                    new IntVector(200000, -121311),
                    new IntVector(200000, -150000),
                    new IntVector(-200000, -150000),
                    new IntVector(-200000, 200000),
                    new IntVector(200000, 200000),
                    new IntVector(50000, 50000),
                    new IntVector(0, 50000),
                    new IntVector(50000, 10000),
                    new IntVector(-50000, 0),
                    new IntVector(-11429, 0),
                    new IntVector(-50000, 22131),
                    new IntVector(-50000, 50000),
                    new IntVector(-50000, 27273),
                    new IntVector(0, 50000),
                    new IntVector(150000, -50000),
                    new IntVector(125000, -50000),
                    new IntVector(150000, -70000),
                    new IntVector(-150000, -50000),
                    new IntVector(-150000, -100000),
                    new IntVector(150000, -100000),
                    new IntVector(150000, -92623),
                    new IntVector(75714, -50000)
                }
            );
            solution.Dispose();
        }

        [Test]
        public void Test_16() {
            var data = ComplexTestData.data[16].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 16, true),
                    new PathLayout(16, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5.0f, -10.0f),
                        new Vector2(5.0f, -15.0f),
                        new Vector2(0.0f, -15.0f),
                        new Vector2(0.0f, -10.0f),
                        new Vector2(-10.0f, -10.0f),
                        new Vector2(-10.0f, 10.0f),
                        new Vector2(0.0f, 10.0f),
                        new Vector2(0.0f, 15.0f),
                        new Vector2(5.0f, 15.0f),
                        new Vector2(5.0f, 10.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(5.0f, 5.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(10.0f, -5.0f),
                        new Vector2(10.0f, -10.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(0.0f, 5.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(-5.0f, -5.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_17() {
            var data = ComplexTestData.data[17].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(5.0f, 5.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(10.0f, -5.0f),
                        new Vector2(10.0f, -20.0f),
                        new Vector2(-10.0f, -20.0f),
                        new Vector2(-10.0f, 20.0f),
                        new Vector2(10.0f, 20.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(0.0f, 5.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(-5.0f, -5.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_18() {
            var data = ComplexTestData.data[18].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 8, true),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(15.0f, 5.0f),
                        new Vector2(15.0f, -5.0f),
                        new Vector2(20.0f, -5.0f),
                        new Vector2(20.0f, -25.0f),
                        new Vector2(-10.0f, -25.0f),
                        new Vector2(-10.0f, 25.0f),
                        new Vector2(20.0f, 25.0f),
                        new Vector2(20.0f, 5.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(0.0f, 5.0f),
                        new Vector2(-5.0f, 5.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(-5.0f, 15.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(0.0f, 10.0f),
                        new Vector2(0.0f, 15.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_19() {
            var data = ComplexTestData.data[19].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 12, false),
                    new PathLayout(16, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-25.0f, 20.0f),
                        new Vector2(25.0f, 20.0f),
                        new Vector2(25.0f, -20.0f),
                        new Vector2(-25.0f, -20.0f),
                        new Vector2(20.0f, 5.0f),
                        new Vector2(20.0f, 15.0f),
                        new Vector2(-20.0f, 15.0f),
                        new Vector2(-20.0f, 5.0f),
                        new Vector2(-15.0f, 5.0f),
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(-10.0f, 10.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(15.0f, 10.0f),
                        new Vector2(15.0f, 5.0f),
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(5.0f, 0.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_20() {
            var data = ComplexTestData.data[20].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 4, false),
                    new PathLayout(16, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-25.0f, 30.0f),
                        new Vector2(25.0f, 30.0f),
                        new Vector2(25.0f, -30.0f),
                        new Vector2(-25.0f, -30.0f),
                        new Vector2(20.0f, -5.0f),
                        new Vector2(20.0f, 5.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-10.0f, -5.0f),
                        new Vector2(-20.0f, 5.0f),
                        new Vector2(-20.0f, -5.0f),
                        new Vector2(-15.0f, -5.0f),
                        new Vector2(-15.0f, 5.0f),
                        new Vector2(-5.0f, -10.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(5.0f, -15.0f),
                        new Vector2(5.0f, -10.0f),
                        new Vector2(-5.0f, 15.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(5.0f, 10.0f),
                        new Vector2(5.0f, 15.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_21() {
            var data = ComplexTestData.data[21].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(15.0f, 10.0f),
                        new Vector2(15.0f, -10.0f),
                        new Vector2(-15.0f, -10.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_22() {
            var data = ComplexTestData.data[22].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 12, false),
                    new PathLayout(24, 4, false),
                    new PathLayout(28, 4, false),
                    new PathLayout(32, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);
            Assert.AreEqual(mainShape.IsClockWise(5), false);
            Assert.AreEqual(mainShape.IsClockWise(6), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-25.0f, 20.0f),
                        new Vector2(25.0f, 20.0f),
                        new Vector2(25.0f, -20.0f),
                        new Vector2(-25.0f, -20.0f),
                        new Vector2(20.0f, -5.0f),
                        new Vector2(20.0f, 0.0f),
                        new Vector2(15.0f, 0.0f),
                        new Vector2(15.0f, -5.0f),
                        new Vector2(20.0f, -15.0f),
                        new Vector2(20.0f, -10.0f),
                        new Vector2(15.0f, -10.0f),
                        new Vector2(15.0f, -15.0f),
                        new Vector2(20.0f, 5.0f),
                        new Vector2(20.0f, 15.0f),
                        new Vector2(-20.0f, 15.0f),
                        new Vector2(-20.0f, 5.0f),
                        new Vector2(-15.0f, 5.0f),
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(-10.0f, 10.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(15.0f, 10.0f),
                        new Vector2(15.0f, 5.0f),
                        new Vector2(-20.0f, 0.0f),
                        new Vector2(-20.0f, -5.0f),
                        new Vector2(-15.0f, -5.0f),
                        new Vector2(-15.0f, 0.0f),
                        new Vector2(-20.0f, -10.0f),
                        new Vector2(-20.0f, -15.0f),
                        new Vector2(-15.0f, -15.0f),
                        new Vector2(-15.0f, -10.0f),
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(5.0f, 0.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_23() {
            var data = ComplexTestData.data[23].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 4, false),
                    new PathLayout(8, 4, false),
                    new PathLayout(12, 4, false),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false),
                    new PathLayout(24, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);
            Assert.AreEqual(mainShape.IsClockWise(5), false);
            Assert.AreEqual(mainShape.IsClockWise(6), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-25.0f, 30.0f),
                        new Vector2(25.0f, 30.0f),
                        new Vector2(25.0f, -30.0f),
                        new Vector2(-25.0f, -30.0f),
                        new Vector2(20.0f, -5.0f),
                        new Vector2(20.0f, 5.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(-10.0f, -5.0f),
                        new Vector2(-20.0f, 5.0f),
                        new Vector2(-20.0f, -5.0f),
                        new Vector2(-15.0f, -5.0f),
                        new Vector2(-15.0f, 5.0f),
                        new Vector2(20.0f, -15.0f),
                        new Vector2(20.0f, -10.0f),
                        new Vector2(15.0f, -10.0f),
                        new Vector2(15.0f, -15.0f),
                        new Vector2(-20.0f, 15.0f),
                        new Vector2(-20.0f, 10.0f),
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(-15.0f, 15.0f),
                        new Vector2(-5.0f, -10.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(5.0f, -15.0f),
                        new Vector2(5.0f, -10.0f),
                        new Vector2(-5.0f, 15.0f),
                        new Vector2(-5.0f, 10.0f),
                        new Vector2(5.0f, 10.0f),
                        new Vector2(5.0f, 15.0f)
                    })
            );
            
            solution.Dispose();
        }

        [Test]
        public void Test_24() {
            var data = ComplexTestData.data[24].AllocateUnion(allocator);
            var solution = data.shape.ComplexUnion(data.path, allocator);
            data.Dispose();

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);

            var mainShape = solution.pathList;

            Assert.AreEqual(mainShape.layouts.ToArray(),
                new[] {
                    new PathLayout(0, 4, true),
                    new PathLayout(4, 12, false),
                    new PathLayout(16, 4, false),
                    new PathLayout(20, 4, false),
                    new PathLayout(24, 4, false),
                    new PathLayout(28, 4, false),
                    new PathLayout(32, 4, false)
                });

            Assert.AreEqual(mainShape.IsClockWise(0), true);
            Assert.AreEqual(mainShape.IsClockWise(1), false);
            Assert.AreEqual(mainShape.IsClockWise(2), false);
            Assert.AreEqual(mainShape.IsClockWise(3), false);
            Assert.AreEqual(mainShape.IsClockWise(4), false);

            Assert.AreEqual(mainShape.points.ToArray(),
                iGeom.Int(
                    new[] {
                        new Vector2(-25.0f, 20.0f),
                        new Vector2(25.0f, 20.0f),
                        new Vector2(25.0f, -20.0f),
                        new Vector2(-25.0f, -20.0f),
                        new Vector2(20.0f, 5.0f),
                        new Vector2(20.0f, 15.0f),
                        new Vector2(-20.0f, 15.0f),
                        new Vector2(-20.0f, 5.0f),
                        new Vector2(-15.0f, 5.0f),
                        new Vector2(-15.0f, 10.0f),
                        new Vector2(-10.0f, 10.0f),
                        new Vector2(-10.0f, 5.0f),
                        new Vector2(10.0f, 5.0f),
                        new Vector2(10.0f, 10.0f),
                        new Vector2(15.0f, 10.0f),
                        new Vector2(15.0f, 5.0f),
                        new Vector2(-5.0f, -5.0f),
                        new Vector2(-5.0f, 0.0f),
                        new Vector2(-10.0f, 0.0f),
                        new Vector2(-10.0f, -5.0f),
                        new Vector2(-20.0f, 0.0f),
                        new Vector2(-20.0f, -5.0f),
                        new Vector2(-15.0f, -5.0f),
                        new Vector2(-15.0f, 0.0f),
                        new Vector2(5.0f, -7.5f),
                        new Vector2(-5.0f, -7.5f),
                        new Vector2(-5.0f, -10.0f),
                        new Vector2(5.0f, -10.0f),
                        new Vector2(-5.0f, -17.5f),
                        new Vector2(5.0f, -17.5f),
                        new Vector2(5.0f, -15.0f),
                        new Vector2(-5.0f, -15.0f),
                        new Vector2(0.0f, 0.0f),
                        new Vector2(0.0f, -5.0f),
                        new Vector2(5.0f, -5.0f),
                        new Vector2(5.0f, 0.0f)
                    })
            );
            
            solution.Dispose();
        }
    }

}