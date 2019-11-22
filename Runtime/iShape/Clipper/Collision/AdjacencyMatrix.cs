using iShape.Collections;
using Unity.Collections;

namespace iShape.Clipper.Collision {
    
    internal struct AdjacencyMatrix {
        
        internal DynamicArray<int> masterIndices;
        internal DynamicArray<int> slaveIndices;

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