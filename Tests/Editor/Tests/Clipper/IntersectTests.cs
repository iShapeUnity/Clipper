using iShape.Clipper.Solver;
using iShape.Geometry;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class IntersectTests {
        
        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = SubtractTestData.data[0];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
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
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_01() {
            var data = SubtractTestData.data[1];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-5, -10),
                new Vector2(10, -10),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_02() {
            var data = SubtractTestData.data[2];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-5, -10),
                new Vector2(10, -10),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_03() {
            var data = SubtractTestData.data[3];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-5, -10),
                new Vector2(10, -10),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        [Test]
        public void Test_04() {
            var data = SubtractTestData.data[4];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(-5, -10),
                new Vector2(10, -10),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_05() {
            var data = SubtractTestData.data[5];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-10, 0),
                new Vector2(0, -10),
                new Vector2(10, -10),
                new Vector2(10, 10),
                new Vector2(-10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_06() {
            var data = SubtractTestData.data[6];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 10),
                new Vector2(-10, 2.5f),
                new Vector2(-10, -2.5f),
                new Vector2(5, -10),
                new Vector2(10, -10),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_07() {
            var data = SubtractTestData.data[7];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, -10),
                new Vector2(0, 0),
                new Vector2(-10, 0),
                new Vector2(-10, -5),
                new Vector2(-5, -10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_08() {
            var data = SubtractTestData.data[8];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_09() {
            var data = SubtractTestData.data[9];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, -10),
                new Vector2(5, -10),
                new Vector2(0, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_10() {
            var data = SubtractTestData.data[10];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_11() {
            var data = SubtractTestData.data[11];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, -10),
                new Vector2(10, -10),
                new Vector2(20, 10),
                new Vector2(0, 0)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_12() {
            var data = SubtractTestData.data[12];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, 0),
                new Vector2(10, -5),
                new Vector2(10, 5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_13() {
            var data = SubtractTestData.data[13];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.notOverlap);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_14() {
            var data = SubtractTestData.data[14];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-10, 5),
                new Vector2(-10, -5),
                new Vector2(-5, -10),
                new Vector2(5, -10),
                new Vector2(10, -5),
                new Vector2(10, 5),
                new Vector2(5, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_15() {
            var data = SubtractTestData.data[15];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, 0),
                new Vector2(0, -5),
                new Vector2(5, -5),
                new Vector2(5, 0)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_16() {
            var data = SubtractTestData.data[16];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.notOverlap);
            Assert.AreEqual(solution.pathList.Count, 0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_17() {
            var data = SubtractTestData.data[17];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-5, 5),
                new Vector2(0, 5),
                new Vector2(0, 10)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 10),
                new Vector2(5, 5),
                new Vector2(10, 5),
                new Vector2(10, 10)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }

        [Test]
        public void Test_18() {
            var data = SubtractTestData.data[18];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-5, 2.5f),
                new Vector2(-5, 0),
                new Vector2(0, 0),
                new Vector2(0, 2.5f)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 2.5f),
                new Vector2(5, -2.5f),
                new Vector2(10, -2.5f),
                new Vector2(10, 2.5f)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_19() {
            var data = SubtractTestData.data[19];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_20() {
            var data = SubtractTestData.data[20];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.masterIncludeSlave);
            Assert.AreEqual(solution.pathList.Count, 0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_21() {
            var data = SubtractTestData.data[21];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(10, 5),
                new Vector2(0, 5),
                new Vector2(10, 0)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_22() {
            var data = SubtractTestData.data[22];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(0, -5),
                new Vector2(0, 0),
                new Vector2(-5, 0),
                new Vector2(-5, -5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_23() {
            var data = SubtractTestData.data[23];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 5),
                new Vector2(5, -10),
                new Vector2(10, -10),
                new Vector2(10, 10),
                new Vector2(5, 10)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_24() {
            var data = SubtractTestData.data[24];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, -5),
                new Vector2(15, -5),
                new Vector2(15, 5),
                new Vector2(5, 5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_25() {
            var data = SubtractTestData.data[25];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(15, 5),
                new Vector2(5, 5),
                new Vector2(5, -5),
                new Vector2(15, -2.5f)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_26() {
            var data = SubtractTestData.data[26];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(5, -10),
                new Vector2(15, -10),
                new Vector2(15, 5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_27() {
            var data = SubtractTestData.data[27];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-10, -5),
                new Vector2(-10, -10),
                new Vector2(5, -5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, -5),
                new Vector2(10, 0),
                new Vector2(5, 0)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_28() {
            var data = SubtractTestData.data[28];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-10, 0),
                new Vector2(-5, 0),
                new Vector2(-5, 5),
                new Vector2(-10, 5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(-5, 10),
                new Vector2(-5, 15),
                new Vector2(-10, 15)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_29() {
            var data = SubtractTestData.data[29];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-15, 10),
                new Vector2(-8.125f, 10),
                new Vector2(-5, 15)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-11.619f, 5),
                new Vector2(-16.2963f, 5),
                new Vector2(-18.5f, -3.5f)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_30() {
            var data = SubtractTestData.data[30];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(0, 5),
                new Vector2(-5, 5),
                new Vector2(-5, 3),
                new Vector2(0, 3)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(0, 10),
                new Vector2(0, 15),
                new Vector2(-5, 15)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_31() {
            var data = SubtractTestData.data[31];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(5, -5),
                new Vector2(-10, -5),
                new Vector2(-10, 15),
                new Vector2(-11, -5),
                new Vector2(10, -10),
                new Vector2(10, 15)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, 15),
                new Vector2(-5, 0),
                new Vector2(0, 0),
                new Vector2(0, 15)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_32() {
            var data = SubtractTestData.data[32];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 3);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(5, -5),
                new Vector2(-10, -5),
                new Vector2(10, -10),
                new Vector2(10, 15)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, 15),
                new Vector2(-5, 0),
                new Vector2(0, 0),
                new Vector2(0, 15)
            });

            Assert.AreEqual(path1, sample1);
            
            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(-10, -5),
                new Vector2(-10, 15),
                new Vector2(-14, -4)
            });

            Assert.AreEqual(path2, sample2);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_33() {
            var data = SubtractTestData.data[33];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 3);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 20),
                new Vector2(5, -5),
                new Vector2(-10, -5),
                new Vector2(-10, 15),
                new Vector2(-5, 15),
                new Vector2(-5, 0),
                new Vector2(0, 0),
                new Vector2(0, 20),
                new Vector2(-15, 20),
                new Vector2(-15, -10),
                new Vector2(7, -10),
                new Vector2(7, 20)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(7, -15),
                new Vector2(-20, -15),
                new Vector2(7, -16)
            });

            Assert.AreEqual(path1, sample1);
            
            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(-20, -15),
                new Vector2(-20, 20),
                new Vector2(-23, 20)
            });

            Assert.AreEqual(path2, sample2);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_34() {
            var data = SubtractTestData.data[34];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 4);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 20),
                new Vector2(5, -4),
                new Vector2(7, 20)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(4.9167f, -5),
                new Vector2(-10, -5),
                new Vector2(-10, 15),
                new Vector2(-5, 15),
                new Vector2(-5, 0),
                new Vector2(0, 0),
                new Vector2(0, 20),
                new Vector2(-15, 20),
                new Vector2(-15, -10),
                new Vector2(4.5f,-10)
            });

            Assert.AreEqual(path1, sample1);
            
            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(4.0833f, -15),
                new Vector2(-20, -15),
                new Vector2(4, -16)
            });

            Assert.AreEqual(path2, sample2);
            
            var path3 = solution.pathList.Get(3, Allocator.Temp).ToArray();
            var sample3 = iGeom.Int(new[] {
                new Vector2(-20, -15),
                new Vector2(-20, 20),
                new Vector2(-23, 20)
            });

            Assert.AreEqual(path3, sample3);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_35() {
            var data = SubtractTestData.data[35];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-4, -10),
                new Vector2(10, -10),
                new Vector2(10, -5.625f)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-10, -14),
                new Vector2(-16.80001f, -14),
                new Vector2(-20, -15),
                new Vector2(-15, -15)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_36() {
            var data = SubtractTestData.data[36];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(0, 0),
                new Vector2(5, -5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(-5, 10),
                new Vector2(-5, 5),
                new Vector2(0, 0),
                new Vector2(0, 10)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_37() {
            var data = SubtractTestData.data[37];
            
            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 3);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(0, 0),
                new Vector2(5, -5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(0, 8),
                new Vector2(0, 10),
                new Vector2(-2, 10)
            });

            Assert.AreEqual(path1, sample1);
            
            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(0, 0),
                new Vector2(0, 4),
                new Vector2(-2, 2)
            });

            Assert.AreEqual(path2, sample2);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_38() {
            var data = SubtractTestData.data[38];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 4);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(5, 0),
                new Vector2(5, -5),
                new Vector2(10, -5),
                new Vector2(10, 0)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(0, 10),
                new Vector2(0, 15),
                new Vector2(-5, 15),
                new Vector2(-5, 10)
            });

            Assert.AreEqual(path1, sample1);
            
            var path2 = solution.pathList.Get(2, Allocator.Temp).ToArray();
            var sample2 = iGeom.Int(new[] {
                new Vector2(-10, -5),
                new Vector2(-10, 15),
                new Vector2(-15, 15),
                new Vector2(-15, -10),
                new Vector2(-10, -10)
            });

            Assert.AreEqual(path2, sample2);
            
            var path3 = solution.pathList.Get(3, Allocator.Temp).ToArray();
            var sample3 = iGeom.Int(new[] {
                new Vector2(5, 15),
                new Vector2(5, 10),
                new Vector2(10, 10),
                new Vector2(10, 15)
            });

            Assert.AreEqual(path3, sample3);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
                
        [Test]
        public void Test_39() {
            var data = SubtractTestData.data[39];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(15, 15),
                new Vector2(5, 10),
                new Vector2(5, -5),
                new Vector2(-15, -5),
                new Vector2(-15, -10),
                new Vector2(15, -10)
            });

            Assert.AreEqual(path0, sample0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_40() {
            var data = SubtractTestData.data[40];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.equal);
            Assert.AreEqual(solution.pathList.Count, 0);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_41() {
            var data = SubtractTestData.data[41];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 5),
                new Vector2(-5, -5),
                new Vector2(5, -5),
                new Vector2(10, -5),
                new Vector2(10, 5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_42() {
            var data = SubtractTestData.data[42];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(-5, 5),
                new Vector2(-5, -5),
                new Vector2(10, -5),
                new Vector2(10, 0),
                new Vector2(10, 5)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_43() {
            var data = SubtractTestData.data[43];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 1);

            var path = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample = iGeom.Int(new[] {
                new Vector2(10, 5),
                new Vector2(0, 5),
                new Vector2(10, 0)
            });

            Assert.AreEqual(path, sample);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
        
        [Test]
        public void Test_44() {
            var data = SubtractTestData.data[44];

            var master = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var slave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);

            var solution = master.Intersect(slave, Allocator.Temp);

            Assert.AreEqual(solution.nature, Solution.Nature.overlap);
            Assert.AreEqual(solution.pathList.Count, 2);

            var path0 = solution.pathList.Get(0, Allocator.Temp).ToArray();
            var sample0 = iGeom.Int(new[] {
                new Vector2(-10, -5),
                new Vector2(-5, -5),
                new Vector2(-5, 5),
                new Vector2(-10, 5)
            });

            Assert.AreEqual(path0, sample0);

            var path1 = solution.pathList.Get(1, Allocator.Temp).ToArray();
            var sample1 = iGeom.Int(new[] {
                new Vector2(5, 5),
                new Vector2(5, -5),
                new Vector2(10, -5),
                new Vector2(10, 5)
            });

            Assert.AreEqual(path1, sample1);

            solution.Dispose();
            master.Dispose();
            slave.Dispose();
        }
    }

}