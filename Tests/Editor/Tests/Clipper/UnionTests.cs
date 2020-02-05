using iShape.Clipper.Solver;
using iShape.Geometry;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {
    public class UnionTests {
        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = UnionTestData.data[0];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -10),
                new Vector2(5, -15),
                new Vector2(-5, -15),
                new Vector2(-5, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }


        [Test]
        public void Test_02() {
            var data = UnionTestData.data[2];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-5, -15),
                new Vector2(-5, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_03() {
            var data = UnionTestData.data[3];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-5, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_04() {
            var data = UnionTestData.data[4];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-5, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_05() {
            var data = UnionTestData.data[5];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 0),
                new Vector2(-20, 10),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(0, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_07() {
            var data = UnionTestData.data[7];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, -10),
                new Vector2(0, -15),
                new Vector2(-5, -10),
                new Vector2(-10, -10),
                new Vector2(-10, -5),
                new Vector2(-15, 0),
                new Vector2(-10, 0),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_08() {
            var data = UnionTestData.data[8];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_09() {
            var data = UnionTestData.data[9];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_10() {
            var data = UnionTestData.data[10];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_11() {
            var data = UnionTestData.data[11];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, -10),
                new Vector2(-30, -10),
                new Vector2(-10, 0),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_12() {
            var data = UnionTestData.data[12];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_13() {
            var data = UnionTestData.data[13];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -10),
                new Vector2(0, -15),
                new Vector2(-5, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_14() {
            var data = UnionTestData.data[14];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_15() {
            var data = UnionTestData.data[15];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, 10),
                new Vector2(5, 10),
                new Vector2(5, 0),
                new Vector2(10, 0),
                new Vector2(10, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_16() {
            var data = UnionTestData.data[16];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(5, 5),
                new Vector2(-5, 5),
                new Vector2(-5, 0)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, -5),
                new Vector2(-10, -5),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -5),
                new Vector2(5, -5),
                new Vector2(5, 0),
                new Vector2(0, -5)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_17() {
            var data = UnionTestData.data[17];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-5, 15),
                new Vector2(-10, 15),
                new Vector2(-10, -5),
                new Vector2(5, -5),
                new Vector2(5, 5),
                new Vector2(0, 5),
                new Vector2(0, 0),
                new Vector2(-5, 0),
                new Vector2(-5, 5),
                new Vector2(-7.5f, 5),
                new Vector2(-7.5f, 10)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 10),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, -15),
                new Vector2(15, -15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, 10),
                new Vector2(12.5f, 10),
                new Vector2(12.5f, 5),
                new Vector2(10, 5),
                new Vector2(10, -10),
                new Vector2(-15, -10),
                new Vector2(-15, 20),
                new Vector2(0, 20),
                new Vector2(0, 10)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }


        [Test]
        public void Test_18() {
            var data = UnionTestData.data[18];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-5, 2.5f),
                new Vector2(-5, 15),
                new Vector2(-10, 15),
                new Vector2(-10, -5),
                new Vector2(5, -5),
                new Vector2(5, -2.5f),
                new Vector2(-7.5f, -2.5f),
                new Vector2(-7.5f, 2.5f)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 2.5f),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, -15),
                new Vector2(15, -15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, 2.5f),
                new Vector2(12.5f, 2.5f),
                new Vector2(12.5f, -2.5f),
                new Vector2(10, -2.5f),
                new Vector2(10, -10),
                new Vector2(-15, -10),
                new Vector2(-15, 20),
                new Vector2(0, 20),
                new Vector2(0, 2.5f)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }


        [Test]
        public void Test_19() {
            var data = UnionTestData.data[19];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }


        [Test]
        public void Test_20() {
            var data = UnionTestData.data[20];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }


        [Test]
        public void Test_21() {
            var data = UnionTestData.data[21];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(10, 5),
                new Vector2(15, 5),
                new Vector2(10, -5),
                new Vector2(10, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_22() {
            var data = UnionTestData.data[22];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(0, -5),
                new Vector2(0, -10),
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(5, -10),
                new Vector2(5, -5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(5, 5),
                new Vector2(0, 5),
                new Vector2(0, 0)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }


        [Test]
        public void Test_23() {
            var data = UnionTestData.data[23];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -5),
                new Vector2(5, 5),
                new Vector2(-10, 5),
                new Vector2(-10, 10),
                new Vector2(5, 10),
                new Vector2(5, 15),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-15, -15),
                new Vector2(-15, -5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }


        [Test]
        public void Test_24() {
            var data = UnionTestData.data[24];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -5),
                new Vector2(-5, 5),
                new Vector2(5, 5),
                new Vector2(5, 15),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-15, -15),
                new Vector2(-15, -5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }


        [Test]
        public void Test_25() {
            var data = UnionTestData.data[25];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(15, 5),
                new Vector2(25, 0),
                new Vector2(15, -2.5f),
                new Vector2(15, -15),
                new Vector2(-15, -15),
                new Vector2(-15, -5),
                new Vector2(5, -5),
                new Vector2(-5, 5),
                new Vector2(5, 5),
                new Vector2(5, 15),
                new Vector2(15, 15)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }


        [Test]
        public void Test_26() {
            var data = UnionTestData.data[26];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-15, -15),
                new Vector2(-15, -5),
                new Vector2(5, -5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_27() {
            var data = UnionTestData.data[27];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, -5),
                new Vector2(-10, 0),
                new Vector2(5, 0),
                new Vector2(5, 15),
                new Vector2(15, 15),
                new Vector2(15, -15),
                new Vector2(-15, -15),
                new Vector2(-15, -5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_28() {
            var data = UnionTestData.data[28];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-10, 0),
                new Vector2(-15, 0),
                new Vector2(-15, -10),
                new Vector2(5, -10),
                new Vector2(5, 10),
                new Vector2(-5, 10),
                new Vector2(-5, 5),
                new Vector2(0, 5),
                new Vector2(0, 0),
                new Vector2(-5, 0),
                new Vector2(-5, -5),
                new Vector2(-10, -5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(-20, 10),
                new Vector2(-20, 20),
                new Vector2(10, 20),
                new Vector2(10, -15),
                new Vector2(-20, -15),
                new Vector2(-20, 5),
                new Vector2(-10, 5)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_29() {
            var data = UnionTestData.data[29];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-15, 10),
                new Vector2(-20, 10),
                new Vector2(-20, 20),
                new Vector2(10, 20),
                new Vector2(10, -15),
                new Vector2(-20, -15),
                new Vector2(-20, 5),
                new Vector2(-16.2963f, 5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-11.619f, 5),
                new Vector2(0, 5),
                new Vector2(0, 0),
                new Vector2(-15, 0),
                new Vector2(-15, -10),
                new Vector2(5, -10),
                new Vector2(5, 10),
                new Vector2(-8.125f, 10),
                new Vector2(-10, 7)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_30() {
            var data = UnionTestData.data[30];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(0, 5),
                new Vector2(5, 5),
                new Vector2(5, 0),
                new Vector2(-10, 0),
                new Vector2(-10, -10),
                new Vector2(10, -10),
                new Vector2(10, 10),
                new Vector2(0, 10)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-15, 10),
                new Vector2(-15, 20),
                new Vector2(15, 20),
                new Vector2(15, -15),
                new Vector2(-15, -15),
                new Vector2(-15, 5),
                new Vector2(-5, 5)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_31() {
            var data = UnionTestData.data[31];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, -15),
                new Vector2(15, -15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, -10),
                new Vector2(-15, -10),
                new Vector2(-15, 20),
                new Vector2(0, 20),
                new Vector2(0, 15)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_32() {
            var data = UnionTestData.data[32];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, -15),
                new Vector2(15, -15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, -10),
                new Vector2(-15, -10),
                new Vector2(-15, 20),
                new Vector2(0, 20),
                new Vector2(0, 15)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_33() {
            var data = UnionTestData.data[33];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 20),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, 20)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(7, -15),
                new Vector2(15, -15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, -10),
                new Vector2(7, -10)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_34() {
            var data = UnionTestData.data[34];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 3);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 20),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, 20)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = new[] {
                new IntVector(49167, -50000),
                iGeom.Int(new Vector2(5, -5)),
                iGeom.Int(new Vector2(5, -4))
            };

            Assert.AreEqual(path1, sample1);

            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(4.0833f, -15),
                new Vector2(15, -15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, -10),
                new Vector2(4.5f, -10)
            });

            Assert.AreEqual(path2, sample2);

            solution.Dispose();
        }


        [Test]
        public void Test_35() {
            var data = UnionTestData.data[35];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 3);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = new[] {
                iGeom.Int(new Vector2(-4, -10)),
                iGeom.Int(new Vector2(-15, -10)),
                iGeom.Int(new Vector2(-15, 20)),
                iGeom.Int(new Vector2(0, 20)),
                iGeom.Int(new Vector2(0, 0)),
                iGeom.Int(new Vector2(-5, 0)),
                iGeom.Int(new Vector2(-5, 15)),
                iGeom.Int(new Vector2(-10, 15)),
                iGeom.Int(new Vector2(-10, -5)),
                iGeom.Int(new Vector2(5, -5)),
                iGeom.Int(new Vector2(5, 25)),
                iGeom.Int(new Vector2(-20, 25)),
                iGeom.Int(new Vector2(-20, -14)),
                new IntVector(-168000, -140000)
            };

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-10, -14),
                new Vector2(15, -14),
                new Vector2(15, -9)
            });

            Assert.AreEqual(path1, sample1);

            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(15, -9),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, -5.625f),
                new Vector2(12, -5)
            });

            Assert.AreEqual(path2, sample2);
            
            solution.Dispose();
        }

        [Test]
        public void Test_36() {
            var data = UnionTestData.data[36];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(10, 0),
                new Vector2(10, -10),
                new Vector2(-5, -10),
                new Vector2(-5, 5),
                new Vector2(-10, 10),
                new Vector2(-5, 10),
                new Vector2(-5, 15),
                new Vector2(0, 15),
                new Vector2(0, 10),
                new Vector2(5, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_37() {
            var data = UnionTestData.data[37];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(10, 0),
                new Vector2(10, -10),
                new Vector2(-5, -10),
                new Vector2(-5, 15),
                new Vector2(0, 15),
                new Vector2(0, 10),
                new Vector2(5, 10)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(0, 8),
                new Vector2(0, 4),
                new Vector2(2, 6)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_38() {
            var data = UnionTestData.data[38];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 5);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(5, 10),
                new Vector2(0, 10),
                new Vector2(0, 0)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(-10, -5),
                new Vector2(-5, -5),
                new Vector2(-5, 0),
                new Vector2(-5, 10)
            });

            Assert.AreEqual(path1, sample1);

            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(-15, -10),
                new Vector2(-15, 20),
                new Vector2(0, 20),
                new Vector2(0, 15),
                new Vector2(5, 15),
                new Vector2(5, 25),
                new Vector2(-20, 25),
                new Vector2(-20, -15),
                new Vector2(-15, -15)
            });

            Assert.AreEqual(path2, sample2);

            var path3 = solution.pathList.Get(3, Allocator.Temp).ToArray();
            var sample3 = iGeom.Int(new[] {
                new Vector2(10, -5),
                new Vector2(10, -10),
                new Vector2(15, -10),
                new Vector2(15, -5)
            });

            Assert.AreEqual(path3, sample3);

            var path4 = solution.pathList.Get(4, Allocator.Temp).ToArray();
            var sample4 = iGeom.Int(new[] {
                new Vector2(15, 15),
                new Vector2(15, 20),
                new Vector2(20, 20),
                new Vector2(20, -20),
                new Vector2(-25, -20),
                new Vector2(-25, 30),
                new Vector2(10, 30),
                new Vector2(10, 15)
            });

            Assert.AreEqual(path4, sample4);

            solution.Dispose();
        }

        [Test]
        public void Test_39() {
            var data = UnionTestData.data[39];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, -5),
                new Vector2(-5, -5),
                new Vector2(-5, -10),
                new Vector2(-15, -10),
                new Vector2(-15, 10),
                new Vector2(15, 10),
                new Vector2(15, -10),
                new Vector2(5, -10)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, 0),
                new Vector2(5, 0),
                new Vector2(5, 5),
                new Vector2(-5, 5)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }

        [Test]
        public void Test_40() {
            var data = UnionTestData.data[40];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -5),
                new Vector2(-5, -5),
                new Vector2(-5, -10),
                new Vector2(-15, -10),
                new Vector2(-15, 10),
                new Vector2(15, 10),
                new Vector2(15, -10),
                new Vector2(5, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_41() {
            var data = UnionTestData.data[41];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }

        [Test]
        public void Test_42() {
            var data = UnionTestData.data[42];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.slaveIncludeMaster);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
        }
        
        [Test]
        public void Test_43() {
            var data = UnionTestData.data[43];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Union(slave, iGeom, Allocator.Temp);

            Assert.AreEqual(solution.nature, UnionSolution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, -15),
                new Vector2(-15, -15),
                new Vector2(-15, 5),
                new Vector2(5, 5),
                new Vector2(5, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(15, 10),
                new Vector2(15, 0),
                new Vector2(20, 0),
                new Vector2(20, -20),
                new Vector2(5, -20)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(-10, 0),
                new Vector2(-10, -10),
                new Vector2(5, -10)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
        }
    }
}