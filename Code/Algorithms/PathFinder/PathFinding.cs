using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Math = UnityEngine;

namespace UnityFoundation.Code.Algorithms
{
    public partial class PathFinding
    {
        public const int MOVE_DIAGONAL_COST = 14;
        public const int MOVE_STRAIGHT_COST = 10;

        public bool debug;

        public GridSize grid;

        public List<int> blockablePosisitions;

        public Int2 startPosition;
        public Int2 endPosition;

        public PathFinding(GridSize grid, bool debug = false)
        {
            this.grid = grid;
            blockablePosisitions = new List<int>();
            this.debug = debug;
        }

        public void AddBlocked(Int2 position)
        {
            blockablePosisitions.Add(GridIndex(position.X, position.Y));
        }

        public IEnumerable<Int2> FindPath(Int2 startPosition, Int2 endPosition)
        {
            var gridNodes = new NativeArray<PathNode>(grid.Height * grid.Width, Allocator.Temp);

            for(int x = 0; x < grid.Width; x++)
            {
                for(int y = 0; y < grid.Height; y++)
                {
                    var gridIndex = y + x * grid.Height;
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
            while(openNodes.Count > 0)
            {
                var currentNode = GetNodeWithLowestCost(gridNodes, openNodes);

                if(currentNode.index == endNodeIndex)
                    break;

                openNodes.Remove(currentNode.index);
                closedNodes.Add(currentNode.index);

                foreach(var item in OpenNeighbors(currentNode, gridNodes, closedNodes))
                {
                    var neighbor = item;

                    if(!openNodes.Contains(neighbor.index))
                        openNodes.Add(neighbor.index);

                    var tentativeCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbor);
                    if(neighbor.gCost > tentativeCost)
                    {
                        neighbor.gCost = tentativeCost;
                        neighbor.cameFromNodeIndex = currentNode.index;
                    }

                    gridNodes[neighbor.index] = neighbor;
                }
            }

            if(gridNodes[endNodeIndex].cameFromNodeIndex == -1)
            {
                yield return new Int2(-1, -1);
                yield break;
            }

            var nodeIndex = endNodeIndex;
            var path = new List<int>();
            while(nodeIndex != -1)
            {
                path.Insert(0, nodeIndex);
                nodeIndex = gridNodes[nodeIndex].cameFromNodeIndex;
            }

            if(debug)
            {
                Debug.Log(
                    path.Aggregate(
                        "",
                        (fullPath, node) => fullPath + " => " + $"({gridNodes[node].x}, {gridNodes[node].y})"
                    )
                );
            }

            foreach(var node in path)
            {
                yield return GridPosition(node);
            }
        }

        public Int2 GridPosition(int nodeIndex)
        {
            var xPos = nodeIndex / grid.Height;
            return new Int2(
                xPos,
                nodeIndex - xPos * grid.Height
            );
        }

        private IEnumerable<PathNode> OpenNeighbors(
            PathNode currentNode, 
            NativeArray<PathNode> gridNodes, 
            List<int> closedNodes
        )
        {
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
            foreach(var neighbor in neighbors)
            {
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

        private bool IsInsideGrid(int x, int y)
        {
            return x >= 0 && x < grid.Width
                && y >= 0 && y < grid.Height;
        }

        public PathNode GetNodeWithLowestCost(NativeArray<PathNode> gridNodes, List<int> openNodes)
        {
            var node = gridNodes[openNodes.First()];
            for(int index = 0; index < openNodes.Count; index++)
            {
                var gridIndex = openNodes[index];
                if(gridNodes[gridIndex].FCost < node.FCost)
                {
                    node = gridNodes[gridIndex];
                }
            }

            if(debug) Debug.Log("LowestCost: " + node);

            return node;
        }

        private int GridIndex(int x, int y)
        {
            return y + x * grid.Height;
        }

        private int CalculateDistanceCost(Int2 startPosition, Int2 endPosition)
        {
            var distanceX = Mathf.Abs(endPosition.X - startPosition.X);
            var distanceY = Mathf.Abs(endPosition.Y - startPosition.Y);

            var distanceStraight = Mathf.Abs(distanceX - distanceY);

            return MOVE_DIAGONAL_COST * Mathf.Min(distanceX, distanceY) + MOVE_STRAIGHT_COST * distanceStraight;
        }

        private int CalculateDistanceCost(PathNode startNode, PathNode endNode)
        {
            var distanceX = Mathf.Abs(endNode.x - startNode.x);
            var distanceY = Mathf.Abs(endNode.y - startNode.y);

            var distanceStraight = Mathf.Abs(distanceX - distanceY);

            return MOVE_DIAGONAL_COST * Mathf.Min(distanceX, distanceY) + MOVE_STRAIGHT_COST * distanceStraight;
        }

        public string GetStringRepresentation()
        {
            var str = "Grid representation\n";
            str += "=>  _ is walkable\n";
            str += "=>  # is blocked\n\n";

            for(int y = 0; y < grid.Height; y++)
            {
                var line = "";
                for(int x = 0; x < grid.Width; x++)
                {
                    line += blockablePosisitions.Contains(GridIndex(x, y)) ? "# " : "_  ";
                }
                str += line + "\n";
            }
            
            return str;
        }
    }
}