using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class Node
    {
        public Point point;
        public List<Point> incoming = new List<Point>();
        public List<Point> outgoing = new List<Point>();
        public NodeType nodeType;
        public string blueprintName;
        public Vector2 position;

        public Node(NodeType nodeType, string blueprintName, Point point)
        {
            this.nodeType = nodeType;
            this.blueprintName = blueprintName;
            this.point = point;
        }

        public Node(Point point)
        {
            this.point = point;
        }

        public void AssignLocation(NodeType nodeType, string blueprintName)
        {
            this.nodeType = nodeType;
            this.blueprintName = blueprintName;
        }


        public void AddIncoming(Point p)
        {
            if (incoming.Any(element => element.Equals(p)))
                return;

            incoming.Add(p);
        }

        public void AddOutgoing(Point p)
        {
            if (outgoing.Any(element => element.Equals(p)))
                return;

            outgoing.Add(p);
        }

        public void RemoveIncoming(Point p)
        {
            incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(Point p)
        {
            outgoing.RemoveAll(element => element.Equals(p));
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}