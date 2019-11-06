using iShape.Support;
using Unity.Collections;

namespace iShape.Clipper.Intersection {
    
    internal struct AdjacencyMatrix {
        
        internal readonly DynamicArray<int> masterIndices;
        internal readonly DynamicArray<int> slaveIndices;

        internal AdjacencyMatrix(int size, Allocator allocator) {
            slaveIndices = new DynamicArray<int>(size, allocator);
            masterIndices = new DynamicArray<int>(size, allocator);
        }

        internal void AddMate(int master, int slave) {
            slaveIndices.Add(slave);
            masterIndices.Add(master);
        }

        internal void Dispose() {
            slaveIndices.Dispose();
            masterIndices.Dispose();
        }
    }
}