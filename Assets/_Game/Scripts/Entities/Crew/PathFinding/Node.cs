using _Game.Scripts.Entities;
using System.Collections;
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
                    return cell.GridItem == null && State != NodeState.Disabled;
                }
                else
                {
                    return State != NodeState.Disabled;
                }
            }
        }

    }
}
