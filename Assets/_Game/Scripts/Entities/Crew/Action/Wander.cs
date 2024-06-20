using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.PathFinding;
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
            Node node = moveData.GetFreeNode();
            List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, node.transform.position);
            if (path == null)
            {
                yield break;
            }
            crew.OccupyingNodes.Clear();
            crew.OccupyingNodes.Add(node);
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