namespace UnityFoundation.Code.Algorithms
{
    public partial class PathFinding {
        public struct PathNode {
            public int x;
            public int y;

            public int index;

            // Custo de andar um nó
            public int gCost;
            // Custo da heurística até o endNode
            public int hCost;
            // Custo total do nó
            private int fCost;
            public int FCost {
                get { return gCost + hCost; }
            }

            public bool isWalkable;

            public int cameFromNodeIndex;

            public override string ToString() {
                return $"index: {index}\n - x: {x} - y: {y}\n - gCost: {gCost} - hCost: {hCost}\n - fCost: {FCost}";
            }
        }
    }
}