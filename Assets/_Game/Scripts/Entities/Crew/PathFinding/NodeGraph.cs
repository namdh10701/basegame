using _Base.Scripts.Utils.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinding
{
    public class NodeGraph : MonoBehaviour
    {
        public List<Node> nodes;


        public NodeGraph()
        {
            nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
        }

        public void ConnectNodes(Node nodeA, Node nodeB)
        {
            if (!nodeA.neighbors.Contains(nodeB))
                nodeA.neighbors.Add(nodeB);

            if (!nodeB.neighbors.Contains(nodeA))
                nodeB.neighbors.Add(nodeA);
        }

        public List<Node> GetNeighbors(Node node)
        {
            return node.neighbors;
        }
        public Node getRandomFreeNode()
        {
            List<Node> Frees = new List<Node>();

            foreach (Node node in nodes)
            {
                if (node.State == NodeState.Free)
                {
                    Frees.Add(node);
                }
            }
            return Frees.GetRandom();
        }
    }
}