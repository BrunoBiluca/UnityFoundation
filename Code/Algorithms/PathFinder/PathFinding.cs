using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

namespace UnityFoundation.Code.Algorithms
{
    public class PathFinding {

        public const int MOVE_DIAGONAL_COST = 14;
        public const int MOVE_STRAIGHT_COST = 10;

        public bool debug;

        public Int2 grid;

        public List<int> blockablePosisitions;

        public Int2 startPosition;
        public Int2 endPosition;

        public PathFinding(Int2 grid, bool debug = false) {
            this.grid = grid;
            blockablePosisitions = new List<int>();
            this.debug = debug;
        }

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

        public void SetIsWalkable(Vector2 position, bool v) {
            blockablePosisitions.Add(GridIndex((int)position.x, (int)position.y));
        }

        public IEnumerable<Int2> FindPath(Int2 startPosition, Int2 endPosition) {
            var gridNodes = new NativeArray<PathNode>(grid.Y * grid.X, Allocator.Temp);

            for(int x = 0; x < grid.X; x++) {
                for(int y = 0; y < grid.Y; y++) {
                    var gridIndex = y + x * grid.Y;
                    var pathNode = new PathNode() {
                        x = x,
                        y = y,
                        index = gridIndex,
                        gCost = int.MaxValue,
                        hCost = CalculateDistanceCost(new Int2(x, y), endPosition),
                        isWalkable = !blockablePosisitions.Contains(gridIndex),
                        cameFromNodeIndex = -1
                    };

                    gridNodes[gridIndex] = pathNode;
                }
            }

            var openNodes = new List<int>();
            var closedNodes = new List<int>();

            // TODO: Encapsular o gridNodes em uma estrutura para melhorar a possibilidade de atualizar os valores
            var startIndex = GridIndex(startPosition.X, startPosition.Y);
            var startNode = gridNodes[startIndex];
            startNode.gCost = 0;
            gridNodes[startIndex] = startNode;

            openNodes.Add(startNode.index);

            var endNodeIndex = GridIndex(endPosition.X, endPosition.Y);
            while(openNodes.Count > 0) {
                var currentNode = GetNodeWithLowestCost(gridNodes, openNodes);

                if(currentNode.index == endNodeIndex)
                    break;

                openNodes.Remove(currentNode.index);
                closedNodes.Add(currentNode.index);

                foreach(var item in OpenNeighbors(currentNode, gridNodes, closedNodes)) {
                    var neighbor = item;

                    if(!openNodes.Contains(neighbor.index))
                        openNodes.Add(neighbor.index);

                    var tentativeCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbor);
                    if(neighbor.gCost > tentativeCost) {
                        neighbor.gCost = tentativeCost;
                        neighbor.cameFromNodeIndex = currentNode.index;
                    }

                    gridNodes[neighbor.index] = neighbor;
                }
            }

            if(gridNodes[endNodeIndex].cameFromNodeIndex == -1) {
                yield return new Int2(-1, -1);
                yield break;
            }

            var nodeIndex = endNodeIndex;
            var path = new List<int>();
            while(nodeIndex != -1) {
                path.Insert(0, nodeIndex);
                nodeIndex = gridNodes[nodeIndex].cameFromNodeIndex;
            }

            if(debug) {
                Debug.Log(
                    path.Aggregate(
                        "",
                        (fullPath, node) => fullPath + " => " + $"({gridNodes[node].x}, {gridNodes[node].y})"
                    )
                );
            }

            foreach(var node in path) {
                yield return GridPosition(node);
            }
        }

        public Int2 GridPosition(int nodeIndex) {
            var xPos = nodeIndex / grid.Y;
            return new Int2(
                xPos,
                nodeIndex - xPos * grid.Y
            );
        }

        private IEnumerable<PathNode> OpenNeighbors(PathNode currentNode, NativeArray<PathNode> gridNodes, List<int> closedNodes) {
            var neighbors = new List<Int2>() {
            new Int2(1, 0), // up
            new Int2(0, 1), // right
            new Int2(-1, 0), // down
            new Int2(0, -1), // left
            new Int2(1, 1), // up right
            new Int2(-1, 1), // down right
            new Int2(-1, -1), // down left
            new Int2(1, -1) // up left
        };
            foreach(var neighbor in neighbors) {
                var neighborPos = new Int2(
                    currentNode.x + neighbor.X, 
                    currentNode.y + neighbor.Y
                );
                if(!IsInsideGrid(neighborPos.X, neighborPos.Y)) continue;

                var neighborNode = gridNodes[GridIndex(neighborPos.X, neighborPos.Y)];
                if(closedNodes.Contains(neighborNode.index)) continue;
                if(!neighborNode.isWalkable) continue;

                yield return neighborNode;
            }
        }

        private bool IsInsideGrid(int x, int y) {
            return x >= 0 && x < grid.X
                && y >= 0 && y < grid.Y;
        }

        public PathNode GetNodeWithLowestCost(NativeArray<PathNode> gridNodes, List<int> openNodes) {
            var node = gridNodes[openNodes.First()];
            for(int index = 0; index < openNodes.Count; index++) {
                var gridIndex = openNodes[index];
                if(gridNodes[gridIndex].FCost < node.FCost) {
                    node = gridNodes[gridIndex];
                }
            }

            if(debug) Debug.Log("LowestCost: " + node);

            return node;
        }

        private int GridIndex(int x, int y) {
            return y + x * grid.Y;
        }

        private int CalculateDistanceCost(Int2 startPosition, Int2 endPosition) {
            var distanceX = Math.Abs(endPosition.X - startPosition.X);
            var distanceY = Math.Abs(endPosition.Y - startPosition.Y);

            var distanceStraight = Math.Abs(distanceX - distanceY);

            return MOVE_DIAGONAL_COST * Math.Min(distanceX, distanceY) + MOVE_STRAIGHT_COST * distanceStraight;
        }

        private int CalculateDistanceCost(PathNode startNode, PathNode endNode) {
            var distanceX = Math.Abs(endNode.x - startNode.x);
            var distanceY = Math.Abs(endNode.y - startNode.y);

            var distanceStraight = Math.Abs(distanceX - distanceY);

            return MOVE_DIAGONAL_COST * Math.Min(distanceX, distanceY) + MOVE_STRAIGHT_COST * distanceStraight;
        }
    }
}