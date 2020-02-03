namespace iShape.Clipper.Collision {
    internal enum CrossType {
        not_cross      = -1,    // no intersections
        same_line      =  0,
        pure           =  1,    // simple intersection with no overlaps
        end_a0         =  4,
        end_a1         =  5,
        end_b0         =  6,
        end_b1         =  7,
        end_a0_b0      =  8,
        end_a0_b1      =  9,
        end_a1_b0      =  10,
        end_a1_b1      =  11
    }
}