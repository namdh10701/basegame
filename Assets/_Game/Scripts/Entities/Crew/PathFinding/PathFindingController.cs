using _Game.Features.Gameplay;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class PathfindingController : MonoBehaviour
    {
        public AStarPathFinding pathfinder;
        public NodeGraph NodeGraph;

        public List<Cell> cells;
        public List<Node> nodes;
        public void Initialize()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                nodes[i].cell = cells[i];
            }
            pathfinder = new AStarPathFinding(NodeGraph);
        }
        public List<Vector3> GetPath(Vector3 startPos, Vector3 targetPos)
        {
            return pathfinder.FindPath(startPos, targetPos);
        }
    }
}