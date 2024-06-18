using _Base.Scripts.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class WanderData : MonoBehaviour
    {
        public ShipSetup ShipSetup;
        public CrewController CrewController;

        public Cell GetFreeCell()
        {
            List<Cell> freeCells = new List<Cell>(ShipSetup.FreeCells);
            List<Crew> crews = CrewController.crews;

            foreach (Crew crew in crews)
            {
                foreach (Cell cell in crew.OccupyCells)
                {
                    freeCells.Remove(cell);
                }
            }
            return freeCells.GetRandom();
        }
    }
}