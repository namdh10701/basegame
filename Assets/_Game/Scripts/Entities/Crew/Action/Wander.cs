using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Wander : CrewActionBase
    {
        Crew crew;
        MoveData moveData;
        public Wander(Crew crew, MoveData moveData)
        {
            Name = "WANDER";
            this.crew = crew;
            this.moveData = moveData;
            this.Execute = DoExecute();
            this.Interupt = DoInterupt();
        }
        public IEnumerator DoExecute()
        {
            Debug.Log("EXECUTE");
            Node node = moveData.GetFreeNode();
            crew.OccupyingNodes.Clear();
            crew.OccupyingNodes.Add(node);
            yield return crew.CrewMovement.MoveTo(node.transform.position);
            yield return new WaitForSeconds(2);
        }

        public IEnumerator DoInterupt()
        {
            crew.CrewMovement.Velocity = Vector2.zero;
            yield break;
        }

        public override void ReBuild(Crew crew)
        {
            this.Execute = DoExecute();
            this.Interupt = DoInterupt();
        }
    }
}