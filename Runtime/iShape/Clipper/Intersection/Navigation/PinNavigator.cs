using System.Drawing;
using iShape.Clipper.Intersection.Primitive;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Intersection.Navigation {
  internal struct PinNavigator {
        
        // for s in slavePath { nodeArray[s] }  iterate all pins in counter clockwise order by slave path
        private readonly NativeArray<int> slavePath;

        // pinPathArray[nodeArray[i].index] return PinPath for this pin
        private readonly NativeArray<PinPath> pinPathArray;
        
        // supply array for nodeArray[i].index return PinPoint for this pin
        private readonly NativeArray<PinPoint> pinPointArray;

        // keep info about each pin node, also for n in nodeArray iterate all pins in clockwise order by master path
        private NativeArray<PinNode> nodeArray;

        internal readonly bool isEqual;
        internal readonly bool hasContacts;

        internal PinNavigator(NativeArray<int> slavePath, NativeArray<PinPath> pinPathArray, NativeArray<PinPoint> pinPointArray, NativeArray<PinNode> nodeArray, bool hasContacts) {
            this.slavePath = slavePath;
            this.pinPathArray = pinPathArray;
            this.pinPointArray = pinPointArray;
            this.nodeArray = nodeArray;
            this.isEqual = false;
            this.hasContacts = hasContacts;
        }
        
        internal PinNavigator(bool hasContacts) {
            this.slavePath = new NativeArray<int>(0, Allocator.Temp);
            this.pinPathArray = new NativeArray<PinPath>(0, Allocator.Temp);
            this.pinPointArray = new NativeArray<PinPoint>(0, Allocator.Temp);
            this.nodeArray = new NativeArray<PinNode>(0, Allocator.Temp);
            this.isEqual = true;
            this.hasContacts = hasContacts;
        }

        internal Cursor Next(Cursor cursor) {
            return Next(cursor.index);
        }

        internal Cursor Next() {
            return this.nodeArray.Length != 0 ? Next(0) : Cursor.empty;
        }


        private Cursor Next(int index) {
            var i = index;
            var n = nodeArray.Length;
            do {
                var node = nodeArray[i];
                if (node.marker == 0) {
                    PinPoint.PinType type;
                    if (node.isPinPath == 0) {
                        var pin = pinPointArray[node.index];
                        type = pin.type;
                    } else {
                        var path = pinPathArray[node.index];
                        type = path.v0.type;
                    }

                    return new Cursor(type, i);
                }

                i = (i + 1) % n;
            } while (i != index);

            return Cursor.empty;
        }


        internal void Mark(Cursor cursor) {
            var node = nodeArray[cursor.index];
            node.marker = 1;
            nodeArray[cursor.index] = node;
        }


        internal Cursor NextSlave(Cursor cursor) {
            var node = nodeArray[cursor.index];

            var n = slavePath.Length;
            var nextSlaveIndex = (node.slaveIndex + 1) % n;
            var index = slavePath[nextSlaveIndex];
            var nextNode = nodeArray[index];

            if (nextNode.isPinPath == 0) {
                var pin = pinPointArray[nextNode.index];
                return new Cursor(pin.type, index);
            }

            var path = pinPathArray[nextNode.index];
            return new Cursor(path.v0.type, index);
        }


        internal Cursor NextMaster(Cursor cursor) {
            var node = nodeArray[cursor.index];

            var n = nodeArray.Length;
            var nextMasterIndex = (node.masterIndex + 1) % n;
            var index = nextMasterIndex;
            var nextNode = nodeArray[index];

            if (nextNode.isPinPath == 0) {
                var pin = pinPointArray[nextNode.index];
                return new Cursor(pin.type, index);
            }

            var path = pinPathArray[nextNode.index];
            return new Cursor(path.v0.type, index);
        }

        internal PathMileStone MasterStartStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.masterMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v0.masterMileStone;
        }


        internal PathMileStone MasterEndStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.masterMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v1.masterMileStone;
        }

        internal IntVector MasterStartPoint(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.point;
            }

            var path = pinPathArray[node.index];
            return path.v0.point;
        }


        internal IntVector MasterEndPoint(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.point;
            }

            var path = pinPathArray[node.index];
            return path.v1.point;
        }


        internal PathMileStone SlaveStartStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.slaveMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v0.slaveMileStone;
        }


        internal IntVector SlaveStartPoint(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.point;
            }

            var path = pinPathArray[node.index];
            return path.v0.point;
        }


        internal PathMileStone SlaveEndStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.slaveMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v1.slaveMileStone;
        }


        internal IntVector SlaveEndPoint(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (node.isPinPath == 0) {
                var pin = pinPointArray[node.index];
                return pin.point;
            }

            var path = pinPathArray[node.index];
            return path.v1.point;
        }

        internal void Dispose() {
            slavePath.Dispose();
            pinPathArray.Dispose();
            pinPointArray.Dispose();
            nodeArray.Dispose();
        }

    }
}