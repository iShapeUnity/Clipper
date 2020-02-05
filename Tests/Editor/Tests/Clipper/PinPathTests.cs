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
        public void Test_000() {
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
        public void Test_001() {
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
        public void Test_002() {
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
        public void Test_003() {
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
        public void Test_004() {
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
        public void Test_005() {
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
        public void Test_006() {
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
        public void Test_007() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_008() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_009() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_010() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_011() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_012() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_013() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_014() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_015() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_016() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_017() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_018() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_019() {
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
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_020() {
            var data = PinTestData.data[20];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 10), new Vector2(-5, 10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }

        [Test]
        public void Test_021() {
            var data = PinTestData.data[21];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 1);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 10), new Vector2(-5, 10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_022() {
            var data = PinTestData.data[22];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(5, 10), new Vector2(10, 10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_023() {
            var data = PinTestData.data[23];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 1);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(5, 10), new Vector2(10, 10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_024() {
            var data = PinTestData.data[24];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 10), new Vector2(10, 10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_025() {
            var data = PinTestData.data[25];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 2);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path0 = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path0.Extract(iMaster)), new[] {
                new Vector2(-5, 10), new Vector2(5, 10)
            });
            
            var path1 = result.pinPathArray[1];

            Assert.AreEqual(iGeom.Float(path1.Extract(iMaster)), new[] {
                new Vector2(5, -10), new Vector2(-5, -10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_026() {
            var data = PinTestData.data[26];

            var iMaster = new NativeArray<IntVector>(iGeom.Int(data[0]), Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(iGeom.Int(data[1]), Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(-10, 0), new Vector2(-10, 10)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_027() {
            var iMaster = new NativeArray<IntVector>(iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, 0),
                new Vector2(-10, 0)
            }), Allocator.Temp);
            
            var iSlave = new NativeArray<IntVector>(new[] {
                new IntVector(-150_000, -300_000), 
                new IntVector(-150_000, 300_000),
                new IntVector(99_999, 300_000),
                new IntVector(100_001, -300_000)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            var path = result.pinPathArray[0];

            Assert.AreEqual(iGeom.Float(path.Extract(iMaster)), new[] {
                new Vector2(10, 10), new Vector2(10, 0)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_028() {
            var iMaster = new NativeArray<IntVector>(iGeom.Int(new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            }), Allocator.Temp);
            
            var iSlave = new NativeArray<IntVector>(new[] {
                new IntVector(-150_000, -300_000), 
                new IntVector(-150_000, 300_000),
                new IntVector(99_999, 300_000),
                new IntVector(100_001, -300_000)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);
            Assert.AreEqual(result.pinPointArray.Length, 0);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_100() {
            var iMaster = new NativeArray<IntVector>(new [] {
                new IntVector(-2, 2),
                new IntVector(2, 2),
                new IntVector(2, -2),
                new IntVector(-2, -2)
            }, Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(new [] {
                new IntVector(-4, 7),
                new IntVector(-4, -7),
                new IntVector(3, -7),
                new IntVector(1, 7)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.Extract(iMaster), new[] {
                new IntVector(2, 2), new IntVector(2, 0)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_101() {
            var iMaster = new NativeArray<IntVector>(new [] {
                new IntVector(-2, 2),
                new IntVector(2, 2),
                new IntVector(2, -2),
                new IntVector(-2, -2)
            }, Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(new [] {
                new IntVector(-4, 7),
                new IntVector(-4, -7),
                new IntVector(1, -7),
                new IntVector(3, 7)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.Extract(iMaster), new[] {
                new IntVector(2, 0), new IntVector(2, -2)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_102() {
            var iMaster = new NativeArray<IntVector>(new [] {
                new IntVector(-2, 2),
                new IntVector(2, 2),
                new IntVector(2, 0),
                new IntVector(-2, 0)
            }, Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(new [] {
                new IntVector(-4, 7),
                new IntVector(-4, -7),
                new IntVector(3, -7),
                new IntVector(1, 7)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPathArray.Length, 1);

            var path = result.pinPathArray[0];

            Assert.AreEqual(path.Extract(iMaster), new[] {
                new IntVector(2, 2), new IntVector(2, 0)
            });
            
            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_103() {
            var iMaster = new NativeArray<IntVector>(new [] {
                new IntVector(-2, 2),
                new IntVector(2, 2),
                new IntVector(2, 0),
                new IntVector(-2, 0)
            }, Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(new [] {
                new IntVector(-4, 4),
                new IntVector(-4, -4),
                new IntVector(7, -4),
                new IntVector(0, 4)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPointArray.Length, 1);
            Assert.AreEqual(result.pinPointArray[0].point, new IntVector(2, 2));
            Assert.AreEqual(result.pinPointArray[0].type, PinPoint.PinType.in_out);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
        
        [Test]
        public void Test_104() {
            var iMaster = new NativeArray<IntVector>(new [] {
                new IntVector(-2, 2),
                new IntVector(2, 2),
                new IntVector(2, 0),
                new IntVector(-2, 0)
            }, Allocator.Temp);
            var iSlave = new NativeArray<IntVector>(new [] {
                new IntVector(0, 4),
                new IntVector(7, -4),
                new IntVector(-4, -4),
                new IntVector(-4, 4)
            }, Allocator.Temp);
        
            var result = CrossDetector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);
            
            Assert.AreEqual(result.pinPointArray.Length, 1);
            Assert.AreEqual(result.pinPointArray[0].point, new IntVector(2, 2));
            Assert.AreEqual(result.pinPointArray[0].type, PinPoint.PinType.in_out);

            result.Dispose();
            iMaster.Dispose();
            iSlave.Dispose();
        }
    }

}