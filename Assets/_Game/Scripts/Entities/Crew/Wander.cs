using _Game.Scripts.Gameplay.Ship;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace _Game.Scripts
{
    public class Wander : CrewAction
    {
        CrewAniamtionHandler Animation;
        Crew crew;
        WanderData wanderData;
        public Wander(Crew crew, WanderData wanderData)
        {
            this.crew = crew;
            this.Animation = this.crew.Animation;
            this.wanderData = wanderData;
        }
        public override IEnumerator Execute()
        {
            Cell cell = wanderData.GetFreeCell();
            List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, cell.transform.position);
            if (path == null)
            {
                yield break;
            }
            crew.OccupyCells = new List<Cell> { cell };
            yield return MoveCoroutine(path);
        }

        IEnumerator MoveCoroutine(List<Vector3> path)
        {
            Animation.PlayMove();
            foreach (Vector3 waypoint in path)
            {
                Vector3 direction = (waypoint - crew.transform.position).normalized;
                if (direction.x > 0)
                {
                    Animation.Flip(Direction.Right);
                }
                else if (direction.x < 0)
                {
                    Animation.Flip(Direction.Left);
                }
                while (Vector3.Distance(crew.transform.position, waypoint) > 0.1f)
                {
                    crew.body.velocity = direction * crew.stats.MoveSpeed.Value;
                    yield return null;
                }
                crew.body.velocity = Vector3.zero;
            }
            Animation.PlayIdle();
            yield return new WaitForSeconds(2);
        }

        public override IEnumerator Interupt()
        {
            yield break;
        }
    }
}