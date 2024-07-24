using _Game.Features.Gameplay;
using _Game.Scripts.PathFinding;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class Wander : CrewActionBase
    {
        Crew crew;
        MoveData moveData;
        Node targetNode;
        public Wander(Crew crew, MoveData moveData)
        {
            Name = "WANDER";
            this.crew = crew;
            this.moveData = moveData;
        }

        public override bool IsAbleToDo => true;

        public override IEnumerator DoExecute()
        {
            targetNode = moveData.GetFreeNode();
            crew.OccupyingNodes.Clear();
            crew.OccupyingNodes.Add(targetNode);
            yield return crew.CrewMovement.MoveTo(targetNode.transform.position);
            crew.State = CrewState.Idle;
            crew.CrewMovement.Velocity = Vector2.zero;
            yield return new WaitForSeconds(2);
        }


        public override void Interupt()
        {
            targetNode.State = NodeState.Free;
            crew.CrewMovement.Velocity = Vector2.zero;
        }

        public override void OnExit()
        {

            crew.State = CrewState.Idle;
        }

        public override void OnStart()
        {
            crew.State = CrewState.Moving;
        }
    }
}