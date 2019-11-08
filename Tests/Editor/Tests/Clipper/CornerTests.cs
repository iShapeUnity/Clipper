using iShape.Clipper.Util;
using iShape.Geometry;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Clipper {

    public class CornerTests {

        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var corner = new Corner(iGeom.Int(new Vector2(0, 0)),
                iGeom.Int(new Vector2(0, 10)),
                iGeom.Int(new Vector2(10, 0)),
                iGeom
            );

            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, -5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, -5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(0, -5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, 5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, 0))), true);
        }

        [Test]
        public void Test_01() {
            var corner = new Corner(iGeom.Int(new Vector2(0, 0)),
                iGeom.Int(new Vector2(-10, 0)),
                iGeom.Int(new Vector2(10, 0)),
                iGeom
            );

            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(0, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, -5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(0, -5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, -5))), true);
        }

        [Test]
        public void Test_02() {
            var corner = new Corner(iGeom.Int(new Vector2(0, 0)),
                iGeom.Int(new Vector2(-10, -10)),
                iGeom.Int(new Vector2(10, -10)),
                iGeom
            );

            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(10, 0))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(0, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-10, 0))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(0, -10))), true);
        }

        [Test]
        public void Test_03() {
            var corner = new Corner(iGeom.Int(new Vector2(0, 0)),
                iGeom.Int(new Vector2(-10, 0)),
                iGeom.Int(new Vector2(0, -10)),
                iGeom
            );

            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, -5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 0))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(0, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, 5))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(-5, 0))), false);
        }

        [Test]
        public void Test_04() {
            var corner = new Corner(iGeom.Int(new Vector2(10, 10)),
                iGeom.Int(new Vector2(10, 20)),
                iGeom.Int(new Vector2(20, 10)),
                iGeom
            );

            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(15, 15))), false);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(15, 5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(10, 5))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 15))), true);
            Assert.AreEqual(corner.IsBetween(iGeom.Int(new Vector2(5, 10))), true);
        }
    }

}