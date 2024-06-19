using Fusion;
using MBT;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Scripts.CrewSystem
{
    [MBTNode("Crew/Wander")]
    public class Wander : Leaf
    {
        public CrewAniamtionHandler crewAniamtionHandler;
        public Crew crew;
        public CellReference destination;
        float elapsedTime;
        bool isFinished;
        List<Vector3> path;
        public override void OnEnter()
        {
            base.OnEnter();
            isFinished = false;
            crewAniamtionHandler.PlayMove();
            destination.Value = GetRandomCell();
            path = crew.pathfinder.GetPath(transform.position, destination.Value.transform.position);
            StartCoroutine(WanderCoroutine());
        }

        public override NodeResult Execute()
        {
            return isFinished ? NodeResult.success : NodeResult.running;
        }

        IEnumerator WanderCoroutine()
        {
            foreach (Vector3 waypoint in path)
            {
                Vector3 direction = (waypoint - transform.position).normalized;
                if (direction.x > 0)
                {
                    crewAniamtionHandler.Flip(Direction.Right);
                }
                else if (direction.x < 0)
                {
                    crewAniamtionHandler.Flip(Direction.Left);
                }
                while (Vector3.Distance(transform.position, waypoint) > 0.1f)
                {
                    crew.body.velocity = direction * crew.stats.MoveSpeed.Value;
                    yield return null;
                }
                crew.body.velocity = Vector3.zero;
            }
        }

        Cell GetRandomCell()
        {
            Cell cell = null;
            return cell;
        }
    }
}
