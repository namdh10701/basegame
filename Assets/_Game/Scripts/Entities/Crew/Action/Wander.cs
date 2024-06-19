using _Game.Scripts.Gameplay.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Wander : CrewAction
    {
        Crew crew;
        MoveData moveData;
        public Wander(Crew crew, MoveData moveData)
        {
            this.crew = crew;
            this.moveData = moveData;
            this.Execute = DoExecute();
            this.Interupt = DoInterupt();
        }
        public IEnumerator DoExecute()
        {
            Cell cell = moveData.GetFreeCell();
            List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, cell.transform.position);
            if (path == null)
            {
                yield break;
            }
            crew.OccupyCells = new List<Cell> { cell };
            yield return crew.CrewMovement.MoveByPath(path);
            yield return new WaitForSeconds(2);
            Debug.Log("Done");
        }

        public IEnumerator DoInterupt()
        {
            yield break;
        }
    }
}