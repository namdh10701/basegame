using _Base.Scripts.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.PathFinding
{
    public class MoveData : MonoBehaviour
    {
        public ShipSetup ShipSetup;
        public CrewController CrewController;

        public Node GetFreeNode()
        {
            List<IWorkLocation> workLocations = ShipSetup.WorkLocations;
            List<Node> freeCells = new List<Node>(ShipSetup.NodeGraph.nodes);
            List<Crew> crews = CrewController.crews;
            foreach (IWorkLocation workLocation in workLocations)
            {
                foreach (Node slot in workLocation.WorkingSlots)
                {
                     if (slot.State == NodeState.Occupied)
                     {
                         if (freeCells.Contains(slot))
                         {
                             freeCells.Remove(slot);
                         }
                     }
                }
            }
            foreach (Crew crew in crews)
            {
                foreach (Node node in crew.OccupyingNodes)
                {
                    if (freeCells.Contains(node))
                    {
                        freeCells.Remove(node);
                    }
                }
            }
            return freeCells.GetRandom();
        }
    }
}