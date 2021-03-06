using iShape.Clipper.Collision.Primitive;
using iShape.Geometry;
using Unity.Collections;

namespace iShape.Clipper.Collision.Navigation {
    public struct PinNavigator {
        
        // for s in slavePath { nodeArray[s] }  iterate all pins in counter clockwise order by slave path
        private NativeArray<int> slavePath;

        // pinPathArray[nodeArray[i].index] return PinPath for this pin
        public NativeArray<PinPath> pinPathArray { get; }
        
        // supply array for nodeArray[i].index return PinPoint for this pin
        public NativeArray<PinPoint> pinPointArray { get; }

        // keep info about each pin node, also for n in nodeArray iterate all pins in clockwise order by master path
        internal NativeArray<PinNode> nodeArray;

        internal readonly bool isEqual;
        internal readonly bool hasContacts;
        internal readonly Util.Rect masterBox;
        internal readonly Util.Rect slaveBox;

        internal PinNavigator(NativeArray<int> slavePath, NativeArray<PinPath> pinPathArray, NativeArray<PinPoint> pinPointArray, NativeArray<PinNode> nodeArray, bool hasContacts, Util.Rect masterBox, Util.Rect slaveBox) {
            this.slavePath = slavePath;
            this.pinPathArray = pinPathArray;
            this.pinPointArray = pinPointArray;
            this.nodeArray = nodeArray;
            this.isEqual = false;
            this.hasContacts = hasContacts;
            this.masterBox = masterBox;
            this.slaveBox = slaveBox;
        }
        
        internal PinNavigator(bool isEqual) {
            this.slavePath = new NativeArray<int>(0, Allocator.Temp);
            this.pinPathArray = new NativeArray<PinPath>(0, Allocator.Temp);
            this.pinPointArray = new NativeArray<PinPoint>(0, Allocator.Temp);
            this.nodeArray = new NativeArray<PinNode>(0, Allocator.Temp);
            this.isEqual = isEqual;
            this.hasContacts = true;
            this.masterBox = new Util.Rect();
            this.slaveBox = new Util.Rect();
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

            if (!nextNode.isPinPath) {
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

            if (!nextNode.isPinPath) {
                var pin = pinPointArray[nextNode.index];
                return new Cursor(pin.type, index);
            }

            var path = pinPathArray[nextNode.index];
            return new Cursor(path.v0.type, index);
        }
        
        internal Cursor PrevMaster(Cursor cursor) {
            var node = nodeArray[cursor.index];

            int n = nodeArray.Length;
            int prevMasterIndex = (node.masterIndex - 1 + n) % n;
            int index = prevMasterIndex;
            var prevNode = nodeArray[index];

            if (!prevNode.isPinPath) {
                var pin = pinPointArray[prevNode.index];
                return new Cursor(pin.type, index);
            }

            var path = pinPathArray[prevNode.index];
            return new Cursor(path.v0.type, index);
        }

        internal PathMileStone MasterStartStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (!node.isPinPath) {
                var pin = pinPointArray[node.index];
                return pin.masterMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v0.masterMileStone;
        }


        internal PathMileStone MasterEndStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (!node.isPinPath) {
                var pin = pinPointArray[node.index];
                return pin.masterMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v1.masterMileStone;
        }

        internal PathMileStone SlaveStartStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (!node.isPinPath) {
                var pin = pinPointArray[node.index];
                return pin.slaveMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v0.slaveMileStone;
        }


        internal IntVector SlaveStartPoint(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (!node.isPinPath) {
                var pin = pinPointArray[node.index];
                return pin.point;
            }

            var path = pinPathArray[node.index];
            return path.v0.point;
        }


        internal PathMileStone SlaveEndStone(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (!node.isPinPath) {
                var pin = pinPointArray[node.index];
                return pin.slaveMileStone;
            }

            var path = pinPathArray[node.index];
            return path.v1.slaveMileStone;
        }


        internal IntVector SlaveEndPoint(Cursor cursor) {
            var node = nodeArray[cursor.index];
            if (!node.isPinPath) {
                var pin = pinPointArray[node.index];
                return pin.point;
            }

            var path = pinPathArray[node.index];
            return path.v1.point;
        }

        public void Dispose() {
            slavePath.Dispose();
            pinPathArray.Dispose();
            pinPointArray.Dispose();
            nodeArray.Dispose();
        }

    }
}