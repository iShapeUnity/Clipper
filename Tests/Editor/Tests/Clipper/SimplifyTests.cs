using iShape.Clipper.Collision;
using iShape.Collections;
using iShape.Geometry;
using NUnit.Framework;
using Tests.Clipper.Data;
using Unity.Collections;

namespace Tests.Clipper {

    public class SimplifyTests {

        private IntGeom iGeom = IntGeom.DefGeom;

        [Test]
        public void Test_00() {
            var data = SimplifyTestData.data[0];
            var origin = data.points;
            var points = new DynamicArray<IntVector>(origin, Allocator.Temp);
            points.Simplify();
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }

        [Test]
        public void Test_01() {
            var data = SimplifyTestData.data[1];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(-100000, -100000), 
                new IntVector(-100000, 100000),
                new IntVector(0, 0)
            };

            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_02() {
            var data = SimplifyTestData.data[2];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(-100000, -100000),
                new IntVector(-150000, -150000)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_03() {
            var data = SimplifyTestData.data[3];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(0, -100000),
                new IntVector(0, 100000),
                new IntVector(-100000, 100000)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_04() {
            var data = SimplifyTestData.data[4];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(0, -100000),
                new IntVector(0, 0),
                new IntVector(-100000, 0)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_05() {
            var data = SimplifyTestData.data[5];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(0, -100000),
                new IntVector(0, 0)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_06() {
            var data = SimplifyTestData.data[6];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(0, -50000),
                new IntVector(0, -100000)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_07() {
            var data = SimplifyTestData.data[7];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(-100000, -100000)
            };

            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_08() {
            var data = SimplifyTestData.data[8];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(0, -100000),
                new IntVector(0, 100000),
                new IntVector(100000, -100000)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_09() {
            var data = SimplifyTestData.data[9];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(0, -100000),
                new IntVector(0, 100000)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        [Test]
        public void Test_10() {
            var data = SimplifyTestData.data[10];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = new [] {
                new IntVector(-100000, 0),
                new IntVector(50000, 0)
            };
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
        
        [Test]
        public void Test_11() {
            var data = SimplifyTestData.data[11];
            var points = new DynamicArray<IntVector>(data.points, Allocator.Temp);
            points.Simplify();

            var origin = data.points;
            
            var result = points.ConvertToArray();
            Assert.AreEqual(origin, result);
        }
    }
}