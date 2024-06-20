using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.PathFinding
{
    public enum WorkingSlotState
    {
        Disabled, Occupied, Free
    }
    public class Node : MonoBehaviour
    {
        public WorkingSlotState State = WorkingSlotState.Disabled;
        public Vector2 position => transform.position;
        public List<Node> neighbors;
        public float gCost;
        public float hCost;
        public float fCost { get { return gCost + hCost; } }
        public Node parent;

        public Cell cell;
        public bool walkable;
        public bool Walkable
        {
            get
            {
                if (cell != null)
                {
                    return cell.GridItem == null && walkable;
                }
                else
                {
                    return walkable;
                }
            }
        }

    }
}
