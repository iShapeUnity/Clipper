using iShape.Geometry;
using UnityEngine;

namespace Tests.Clipper.Data {

    public struct SimplifyTestData {
        
        internal struct Polygon {
            internal IntVector[] points;
            internal bool isClockWise;

            internal Polygon(IntVector[] points, bool isClockWise) {
                this.points = points;
                this.isClockWise = isClockWise;
            }

        }

        internal static readonly Polygon[] data = {
            // 0
            new Polygon(new[] {
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(10, -10)
            }.toInt(), true),
            // 1
            new Polygon(new[] {
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, -10),
                new Vector2(10, 10)
            }.toInt(), true),
            // 2
            new Polygon(new[] {
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(-15, -15)
            }.toInt(), true),
            // 3
            new Polygon(new[] {
                new Vector2(0, -10),
                new Vector2(0, 10),
                new Vector2(10, 10),
                new Vector2(-10, 10)
            }.toInt(), true),
            // 4
            new Polygon(new[] {
                new Vector2(0, -10),
                new Vector2(0, 10),
                new Vector2(0, 0),
                new Vector2(-10, 0)
            }.toInt(), true),
            // 5
            new Polygon(new[] {
                new Vector2(0, -10),
                new Vector2(0, 10),
                new Vector2(10, 0),
                new Vector2(0, 0)
            }.toInt(), true),
            // 6
            new Polygon(new[] {
                new Vector2(0, -5),
                new Vector2(0, 5),
                new Vector2(0, 10),
                new Vector2(0, -10)
            }.toInt(), true),
            // 7
            new Polygon(new[] {
                new Vector2(-10, -10),
                new Vector2(-10, 10),
                new Vector2(10, 10),
                new Vector2(-10, -10)
            }.toInt(), true),
            // 8
            new Polygon(new[] {
                new Vector2(0, -10),
                new Vector2(0, 10),
                new Vector2(0, 10),
                new Vector2(10, -10)
            }.toInt(), true),
            // 9
            new Polygon(new[] {
                new Vector2(0, -10),
                new Vector2(0, 10),
                new Vector2(10, 10),
                new Vector2(0, 10)
            }.toInt(), true),
            // 10
            new Polygon(new[] {
                new Vector2(-10, 0),
                new Vector2(10, 0),
                new Vector2(-5, 0),
                new Vector2(5, 0)
            }.toInt(), true),
            // 11
            new Polygon(new[] {
                new Vector2(-10, -15),
                new Vector2(-20, -15),
                new Vector2(-20, -10),
                new Vector2(-15, -10), 
                new Vector2(-15, -5), 
                new Vector2(-20, -5),
                new Vector2(-20, 0),
                new Vector2(-15, 0),
                new Vector2(-15, 5),
                new Vector2(-20, 5),
                new Vector2(-20, 15),
                new Vector2(20, 15),
                new Vector2(20, 5),
                new Vector2(15, 5),
                new Vector2(15, 0),
                new Vector2(20, 0),
                new Vector2(20, -5),
                new Vector2(15, -5),
                new Vector2(15, -10),
                new Vector2(20, -10),
                new Vector2(20, -15),
                new Vector2(10, -15)
            }.toInt(), true)
        };
    }

}