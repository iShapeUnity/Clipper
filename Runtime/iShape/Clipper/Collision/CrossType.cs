namespace iShape.Clipper.Collision {
    internal enum CrossType {
        not_cross,  // -1 - no intersections
        same_line,  //  0 - same line
        pure,       //  1 - simple intersection with no overlaps
        edge_cross, //  2 - one of the end is lying on others edge    
        common_end  //  3 - first master end is equal to one of slave ends
    }
}