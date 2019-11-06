namespace iShape.Clipper.Intersection.Navigation {
 
    internal struct PinNode {
        internal int isPinPath; // 0 - false, 1 - true
        internal int masterIndex; // index in master path array
        internal int slaveIndex; // index in slave path array
        internal int marker; // 0 - present, 1 - removed
        internal int index; // index in supply array (PinPoints or PinPaths)

    }
}