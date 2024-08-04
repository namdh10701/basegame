using _Game.Features.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinding
{
    public enum NodeState
    {
        Occupied, Free, Disabled
    }
    public class Node : MonoBehaviour
    {
        public NodeState State;
        public Vector2 position => transform.position;
        public List<Node> neighbors;

        [HideInInspector] public Node parent;
        [HideInInspector] public float gCost;
        [HideInInspector] public float hCost;
        public float fCost { get { return gCost + hCost; } }

        public Cell cell;
        public bool Walkable
        {
            get
            {
                if (cell != null)
                {
                    return (cell.GridItem == null || cell.GridItem.IsWalkAble) && State != NodeState.Disabled;
                }
                else
                {
                    return State != NodeState.Disabled;
                }
            }
        }
        public void ConnectNeighbors(Node[,] grid, int x, int y, int width, int height)
        {
            // Check and connect neighbors
            if (x > 0) neighbors.Add(grid[x - 1, y]); // Left
            if (x < width - 1) neighbors.Add(grid[x + 1, y]); // Right
            if (y > 0) neighbors.Add(grid[x, y - 1]); // Down
            if (y < height - 1) neighbors.Add(grid[x, y + 1]); // Up
        }
    }
}
