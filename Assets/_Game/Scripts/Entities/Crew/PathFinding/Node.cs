using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinding
{
    public enum WorkingSlotState
    {
         Occupied, Free
    }
    public class Node : MonoBehaviour
    {
        public NodeRenderer NodeRenderer;
        public WorkingSlotState State;
        public Vector2 position => transform.position;
        public List<Node> neighbors;
        public float gCost;
        public float hCost;
        public float fCost { get { return gCost + hCost; } }
        public Node parent;

        public Cell cell;
        public bool Walkable
        {
            get
            {
                if (cell != null)
                {
                    return cell.GridItem == null;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}
