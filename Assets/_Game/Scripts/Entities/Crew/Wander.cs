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
            yield return crew.CrewMovement.MoveByPath(path);
            yield return new WaitForSeconds(2);
        }

        public override IEnumerator Interupt()
        {
            yield break;
        }
    }
}