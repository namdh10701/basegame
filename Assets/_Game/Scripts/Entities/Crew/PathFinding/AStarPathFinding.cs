using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace _Game.Scripts.PathFinding
{
    public class AStarPathFinding
    {
        public NodeGraph graph;

        public AStarPathFinding(NodeGraph graph)
        {
            this.graph = graph;
        }

        public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
        {
            List<Node> openList = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            Node startNode = FindClosestNode(startPos);
            Node targetNode = FindClosestNode(targetPos);

            if (startNode == null || targetNode == null)
            {
                Debug.LogWarning("Start or target node not found.");
                return null;
            }

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost ||
                        openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node neighbor in graph.GetNeighbors(currentNode))
                {
                    if (!neighbor.walkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    float newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            // No path found
            return null;
        }

        private float GetDistance(Node neighbor, Node targetNode)
        {
            return Vector3.Distance(neighbor.position, targetNode.position);
        }

        private List<Vector3> RetracePath(Node startNode, Node endNode)
        {
            List<Vector3> path = new List<Vector3>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.position);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            return path;
        }

        private Node FindClosestNode(Vector3 pos)
        {
            float closestDistance = float.MaxValue;
            Node closestNode = null;

            foreach (var node in graph.nodes)
            {
                float dist = Vector3.Distance(node.position, pos);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestNode = node;
                }
            }

            return closestNode;
        }
    }

}
