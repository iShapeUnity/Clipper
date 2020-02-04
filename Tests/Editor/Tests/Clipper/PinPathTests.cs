using iShape.Clipper.Collision;
using iShape.Clipper.Collision.Primitive;
using iShape.Geometry;
using NUnit.Framework;
using Tests.Clipper.Data;
using Tests.Clipper.Extension;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class PinPathTests {

        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = PinTestData.data[0];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            var z = iGeom.Int(new Vector2(-10, 10));

            Assert.AreEqual(path.v0.masterMileStone.index, 0);
            Assert.AreEqual(path.v0.masterMileStone.offset, z.SqrDistance(path.v0.point));

            Assert.AreEqual(path.v1.masterMileStone.index, 0);
            Assert.AreEqual(path.v1.masterMileStone.offset, z.SqrDistance(path.v1.point));

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 1);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iGeom.Int(new Vector2( -5, 10)));
            Assert.AreEqual(path.v1.point, iGeom.Int(new Vector2(5, 10)));

            var points = iGeom.Float(path.Extract(iMaster));
            Assert.AreEqual(points.Length, 2);
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_01() {
            var data = PinTestData.data[1];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 3);
            Assert.AreEqual(path.v1.masterMileStone.index, 3);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 1);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);
        
            Assert.AreEqual(path.v0.point, iGeom.Int(new Vector2(-10, -5)));
            Assert.AreEqual(path.v1.point, iGeom.Int(new Vector2(-10, 5)));
            Assert.AreEqual(path.GetLength(iMaster.Length), 1);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_02() {
            var data = PinTestData.data[2];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 2);
            Assert.AreEqual(path.v1.masterMileStone.index, 2);

            Assert.AreEqual(path.v0.slaveMileStone.index, 1);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 2);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iSlave[1]);
            Assert.AreEqual(path.v1.point, iSlave[2]);
            Assert.AreEqual(path.GetLength(iMaster.Length), 1);

            Assert.AreEqual(path.Extract(iMaster).Length, 2);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_03() {
            var data = PinTestData.data[3];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 1);
            Assert.AreEqual(path.v1.masterMileStone.index, 1);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 1);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iSlave[2]);
            Assert.AreEqual(path.v1.point, iSlave[1]);
            Assert.AreEqual(path.GetLength(iMaster.Length), 1);

            Assert.AreEqual(path.Extract(iMaster).Length, 2);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_04() {
            var data = PinTestData.data[4];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 0);
            Assert.AreEqual(path.v1.masterMileStone.index, 1);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 1);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iSlave[2]);
            Assert.AreEqual(path.v1.point, iSlave[1]);

            Assert.AreEqual(path.Extract(iMaster).Length, 2);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_05() {
            var data = PinTestData.data[5];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 3);
            Assert.AreEqual(path.v1.masterMileStone.index, 0);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 1);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iSlave[2]);
            Assert.AreEqual(path.v1.point, iSlave[1]);

            Assert.AreEqual(path.Extract(iMaster).Length, 2);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_06() {
            var data = PinTestData.data[6];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 2);
            Assert.AreEqual(path.v1.masterMileStone.index, 3);

            Assert.AreEqual(path.v0.slaveMileStone.index, 1);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 2);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iSlave[1]);
            Assert.AreEqual(path.v1.point, iSlave[2]);

            Assert.AreEqual(path.Extract(iMaster).Length, 2);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_07() {
            var data = PinTestData.data[7];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 1);
            Assert.AreEqual(path.v1.masterMileStone.index, 2);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 1);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v0.point, iSlave[2]);
            Assert.AreEqual(path.v1.point, iSlave[1]);

            Assert.AreEqual(path.Extract(iMaster).Length, 2);
        }

        [Test]
        public void Test_08() {
            var data = PinTestData.data[8];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 3);
            Assert.AreEqual(path.v1.masterMileStone.index, 1);

            Assert.AreEqual(path.v0.slaveMileStone.index, 3);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);
            
            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 5), new Vector2(-10, 10), new Vector2(10,10), new Vector2(10,5)
            });
        }

        [Test]
        public void Test_09() {
            var data = PinTestData.data[9];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 2);
            Assert.AreEqual(path.v1.masterMileStone.index, 0);

            Assert.AreEqual(path.v0.slaveMileStone.index, 3);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-5, -10), new Vector2(-10, -10), new Vector2(-10, 10), new Vector2(-5, 10)
            });
        }

        [Test]
        public void Test_10() {
            var data = PinTestData.data[10];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 1);
            Assert.AreEqual(path.v1.masterMileStone.index, 3);

            Assert.AreEqual(path.v0.slaveMileStone.index, 3);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(10, -5), new Vector2(10, -10), new Vector2(-10, -10), new Vector2(-10, -5)
            });
        }

        [Test]
        public void Test_11() {
            var data = PinTestData.data[11];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 0);
            Assert.AreEqual(path.v1.masterMileStone.index, 2);

            Assert.AreEqual(path.v0.slaveMileStone.index, 3);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(5, 10), new Vector2(10, 10), new Vector2(10, -10), new Vector2(5, -10)
            });
        }

        [Test]
        public void Test_12() {
            var data = PinTestData.data[12];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 0);
            Assert.AreEqual(path.v1.masterMileStone.index, 1);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(0, 10), new Vector2(10, 10), new Vector2(10, 0)
            });
        }

        [Test]
        public void Test_13() {
            var data = PinTestData.data[13];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 3);
            Assert.AreEqual(path.v1.masterMileStone.index, 0);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 0), new Vector2(-10, 10), new Vector2(0, 10)
            });
        }

        [Test]
        public void Test_14() {
            var data = PinTestData.data[14];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 2);
            Assert.AreEqual(path.v1.masterMileStone.index, 3);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(0, -10), new Vector2(-10, -10), new Vector2(-10, 0)
            });
        }

        [Test]
        public void Test_15() {
            var data = PinTestData.data[15];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 1);
            Assert.AreEqual(path.v1.masterMileStone.index, 2);

            Assert.AreEqual(path.v0.slaveMileStone.index, 2);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(10, 0), new Vector2(10, -10), new Vector2(0, -10)
            });
        }

        [Test]
        public void Test_16() {
            var data = PinTestData.data[16];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 1);
            Assert.AreEqual(path.v1.masterMileStone.index, 0);

            Assert.AreEqual(path.v0.slaveMileStone.index, 4);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(10, 0), new Vector2(10, -10), new Vector2(-10, -10), new Vector2(-10, 10), new Vector2(0, 10)
            });
        }

        [Test]
        public void Test_17() {
            var data = PinTestData.data[17];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 0);
            Assert.AreEqual(path.v1.masterMileStone.index, 3);

            Assert.AreEqual(path.v0.slaveMileStone.index, 4);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(0, 10), new Vector2(10, 10), new Vector2(10, -10), new Vector2(-10, -10), new Vector2(-10, 0)
            });
        }

        [Test]
        public void Test_18() {
            var data = PinTestData.data[18];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 3);
            Assert.AreEqual(path.v1.masterMileStone.index, 2);

            Assert.AreEqual(path.v0.slaveMileStone.index, 4);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 0), new Vector2(-10, 10), new Vector2(10, 10), new Vector2(10, -10), new Vector2(0, -10)
            });
        }

        [Test]
        public void Test_19() {
            var data = PinTestData.data[19];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.v0.masterMileStone.index, 2);
            Assert.AreEqual(path.v1.masterMileStone.index, 1);

            Assert.AreEqual(path.v0.slaveMileStone.index, 4);
            Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

            Assert.AreEqual(path.v1.slaveMileStone.index, 0);
            Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(0, -10), new Vector2(-10, -10), new Vector2(-10, 10), new Vector2(10, 10), new Vector2(10, 0)
            });
        }

        [Test]
        public void Test_20() {
            var master = new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            };
            var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

            var slave = new[] {
                new Vector2(10, 5),
                new Vector2(5, 10),
                new Vector2(-5, 10),
                new Vector2(-10, 5),
                new Vector2(-10, -5),
                new Vector2(-5, -10),
                new Vector2(5, -10),
                new Vector2(10, -5)
            };

            var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

            Assert.AreEqual(result.pinPathArray.Length, 4);

            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_21() {
            var master = new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            };
            var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

            var slave = new[] {
                new Vector2(10, 5),
                new Vector2(10, 10),
                new Vector2(5, 10),
                new Vector2(0, 5),
                new Vector2(-5, 10),

                new Vector2(-10, 10),
                new Vector2(-10, 5),
                new Vector2(-5, 0),
                new Vector2(-10, -5),
                new Vector2(-10, -10),

                new Vector2(-5, -10),
                new Vector2(0, -5),
                new Vector2(5, -10),
                new Vector2(10, -10),
                new Vector2(10, -5),
                new Vector2(5, 0)
            };

            var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

            Assert.AreEqual(result.pinPathArray.Length, 4);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            iMaster.Dispose();
            iSlave.Dispose();
        }
    }

}