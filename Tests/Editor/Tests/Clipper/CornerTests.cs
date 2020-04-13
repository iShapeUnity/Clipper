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
                iGeom.Int(new Vector2(10, 0))
            );

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(5, y: 5)), false), Corner.Result.absent);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(5, y: 5)), true), Corner.Result.contain);

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(5, y: -5)), false), Corner.Result.contain);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(5, y: -5)), true), Corner.Result.absent);

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(-5, y: -5)), false), Corner.Result.contain);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(-5, y: -5)), true), Corner.Result.absent);

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(0, y: -5)), false), Corner.Result.contain);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(0, y: -5)), true), Corner.Result.absent);

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(-5, y: 5)), false), Corner.Result.contain);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(-5, y: 5)), true), Corner.Result.absent);

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(-5, y: 0)), false), Corner.Result.contain);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(-5, y: 0)), true), Corner.Result.absent);

            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(0, y: 5)), false), Corner.Result.onBoarder);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(0, y: 5)), true), Corner.Result.onBoarder);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(5, y: 0)), false), Corner.Result.onBoarder);
            Assert.AreEqual(corner.IsBetweenIntVersion(iGeom.Int(new Vector2(5, y: 0)), true), Corner.Result.onBoarder);
        }

    }

}