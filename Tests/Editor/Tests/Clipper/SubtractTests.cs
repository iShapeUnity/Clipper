using iShape.Clipper.Intersection;
using iShape.Clipper.Intersection.Primitive;
using iShape.Geometry;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper {

    public class SubtractTests {

        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var master = new[] {
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10),
                new Vector2(-10, -10)
            };

            var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

            var slave = new[] {
                new Vector2(0, 0),
                new Vector2(5, 10),
                new Vector2(-5, 10)
            };
            var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

            var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

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


            Assert.AreEqual(path.v0.point, iGeom.Int(new Vector2(-5, 10)));
            Assert.AreEqual(path.v1.point, iGeom.Int(new Vector2(5, 10)));

            iMaster.Dispose();
            iSlave.Dispose();
        }
    }

}