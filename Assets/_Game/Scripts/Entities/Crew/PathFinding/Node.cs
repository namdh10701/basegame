using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinding
{
    public class Node : MonoBehaviour
    {
        public Vector2 position => transform.position;
        public List<Node> neighbors;
        public Cell cell;
        // A* specific variables
        public float gCost;
        public float hCost;
        public float fCost { get { return gCost + hCost; } }
        public Node parent;

        public bool walkable => cell.GridItem == null;

    }
}
