using iShape.Clipper.Intersection;
using iShape.Clipper.Intersection.Primitive;
using iShape.Geometry;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

public class PinPathTests {

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
    
    [Test]
    public void Test_01() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };

        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var slave = new[] {
            new Vector2(0, 0),
            new Vector2(-10, 5),
            new Vector2(-10, -5)
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

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
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_02() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-5, -10);
        var pt1 = new Vector2(5, -10);

        var slave = new[] {
            new Vector2(0, 0),
            pt0,
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 2);
        Assert.AreEqual(path.v1.masterMileStone.index, 2);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 1);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_03() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(10, -5);
        var pt1 = new Vector2(10, 5);

        var slave = new[] {
            new Vector2(0, 0),
            pt0,
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 1);
        Assert.AreEqual(path.v1.masterMileStone.index, 1);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 1);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_04() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(10, 10);
        var pt1 = new Vector2(-10, 10);

        var slave = new[] {
            new Vector2(0, 0),
            pt0,
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 0);
        Assert.AreEqual(path.v1.masterMileStone.index, 1);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 1);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_05() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-10, 10);
        var pt1 = new Vector2(-10, -10);

        var slave = new[] {
            new Vector2(0, 0),
            pt0,
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 3);
        Assert.AreEqual(path.v1.masterMileStone.index, 0);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 1);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_06() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-10, -10);
        var pt1 = new Vector2(10, -10);

        var slave = new[] {
            new Vector2(0, 0),
            pt0,
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 2);
        Assert.AreEqual(path.v1.masterMileStone.index, 3);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 1);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_07() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(10, -10);
        var pt1 = new Vector2(10, 10);

        var slave = new[] {
            new Vector2(0, 0),
            pt0,
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 1);
        Assert.AreEqual(path.v1.masterMileStone.index, 2);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 1);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_08() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(10, 5);
        var pt1 = new Vector2(-10, 5);

        var slave = new[] {
            pt0,
            new Vector2(10, 10),
            new Vector2(-10, 10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 3);
        Assert.AreEqual(path.v1.masterMileStone.index, 1);

        Assert.AreEqual(path.v0.slaveMileStone.index, 3);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_09() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-5, 10);
        var pt1 = new Vector2(-5, -10);

        var slave = new[] {
            pt0,
            new Vector2(-10, 10),
            new Vector2(-10, -10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 2);
        Assert.AreEqual(path.v1.masterMileStone.index, 0);

        Assert.AreEqual(path.v0.slaveMileStone.index, 3);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_10() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-10, -5);
        var pt1 = new Vector2(10, -5);

        var slave = new[] {
            pt0,
            new Vector2(-10, -10),
            new Vector2(10, -10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 1);
        Assert.AreEqual(path.v1.masterMileStone.index, 3);

        Assert.AreEqual(path.v0.slaveMileStone.index, 3);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_11() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(5, -10);
        var pt1 = new Vector2(5, 10);

        var slave = new[] {
            pt0,
            new Vector2(10, -10),
            new Vector2(10, 10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 0);
        Assert.AreEqual(path.v1.masterMileStone.index, 2);

        Assert.AreEqual(path.v0.slaveMileStone.index, 3);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_12() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(10, 0);
        var pt1 = new Vector2(0, 10);

        var slave = new[] {
            pt0,
            new Vector2(10, 10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 0);
        Assert.AreEqual(path.v1.masterMileStone.index, 1);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_13() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(0, 10);
        var pt1 = new Vector2(-10, 0);

        var slave = new[] {
            pt0,
            new Vector2(-10, 10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 3);
        Assert.AreEqual(path.v1.masterMileStone.index, 0);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_14() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-10, 0);
        var pt1 = new Vector2(0, -10);

        var slave = new[] {
            pt0,
            new Vector2(-10, -10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 2);
        Assert.AreEqual(path.v1.masterMileStone.index, 3);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_15() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(0, -10);
        var pt1 = new Vector2(10, 0);

        var slave = new[] {
            pt0,
            new Vector2(10, -10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 1);
        Assert.AreEqual(path.v1.masterMileStone.index, 2);

        Assert.AreEqual(path.v0.slaveMileStone.index, 2);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_16() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(0, 10);
        var pt1 = new Vector2(10, 0);

        var slave = new[] {
            pt0,
            new Vector2(-10, 10),
            new Vector2(-10, -10),
            new Vector2(10, -10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 1);
        Assert.AreEqual(path.v1.masterMileStone.index, 0);

        Assert.AreEqual(path.v0.slaveMileStone.index, 4);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_17() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(-10, 0);
        var pt1 = new Vector2(0, 10);

        var slave = new[] {
            pt0,
            new Vector2(-10, -10),
            new Vector2(10, -10),
            new Vector2(10, 10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 0);
        Assert.AreEqual(path.v1.masterMileStone.index, 3);

        Assert.AreEqual(path.v0.slaveMileStone.index, 4);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_18() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(0, -10);
        var pt1 = new Vector2(-10, 0);

        var slave = new[] {
            pt0,
            new Vector2(10, -10),
            new Vector2(10, 10),
            new Vector2(-10, 10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 3);
        Assert.AreEqual(path.v1.masterMileStone.index, 2);

        Assert.AreEqual(path.v0.slaveMileStone.index, 4);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
    }
    
    [Test]
    public void Test_19() {
        var master = new[] {
            new Vector2(-10, 10),
            new Vector2(10, 10),
            new Vector2(10, -10),
            new Vector2(-10, -10)
        };
        var iMaster = new NativeArray<IntVector>(iGeom.Int(master), Allocator.Temp);

        var pt0 = new Vector2(10, 0);
        var pt1 = new Vector2(0, -10);

        var slave = new[] {
            pt0,
            new Vector2(10, 10),
            new Vector2(-10, 10),
            new Vector2(-10, -10),
            pt1
        };
        var iSlave = new NativeArray<IntVector>(iGeom.Int(slave), Allocator.Temp);

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 1);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        var path = result.pinPathArray[0];

        Assert.AreEqual(path.v0.masterMileStone.index, 2);
        Assert.AreEqual(path.v1.masterMileStone.index, 1);

        Assert.AreEqual(path.v0.slaveMileStone.index, 4);
        Assert.AreEqual(path.v0.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v1.slaveMileStone.index, 0);
        Assert.AreEqual(path.v1.slaveMileStone.offset, 0);

        Assert.AreEqual(path.v0.point, iGeom.Int(pt1));
        Assert.AreEqual(path.v1.point, iGeom.Int(pt0));
        
        iMaster.Dispose();
        iSlave.Dispose();
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

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

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

        var result = Intersector.FindPins(iMaster, iSlave, iGeom, PinPoint.PinType.nil);

        Assert.AreEqual(result.pinPathArray.Length, 4);
        Assert.AreEqual(result.pinPointArray.Length, 0);

        iMaster.Dispose();
        iSlave.Dispose();
    }
}
