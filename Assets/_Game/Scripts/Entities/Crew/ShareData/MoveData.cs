using _Base.Scripts.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class MoveData : MonoBehaviour
    {
        public ShipSetup ShipSetup;
        public CrewController CrewController;

        public Cell GetFreeCell()
        {
            List<IWorkLocation> workLocations = ShipSetup.WorkLocations;
            List<Cell> freeCells = new List<Cell>(ShipSetup.FreeCells);
            List<Crew> crews = CrewController.crews;
            foreach (IWorkLocation workLocation in workLocations)
            {
                foreach (WorkingSlot slot in workLocation.WorkingSlots)
                {
                    if (slot.State == WorkingSlotState.Occupied)
                    {
                        if (freeCells.Contains(slot.cell))
                        {
                            freeCells.Remove(slot.cell);
                        }
                    }
                }
            }
            foreach (Crew crew in crews)
            {
                foreach (Cell cell in crew.OccupyCells)
                {
                    if (freeCells.Contains(cell))
                    {
                        freeCells.Remove(cell);
                    }
                }
            }
            return freeCells.GetRandom();
        }
    }
}