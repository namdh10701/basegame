using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.CrewSystem
{
    [MBTNode("Crew/Idle")]
    public class Idle : Leaf
    {
        public CrewAniamtionHandler crewAniamtionHandler;
        public DeviableFloat time;
        float elapsedTime;
        public override void OnEnter()
        {
            base.OnEnter();
            crewAniamtionHandler.PlayIdle();
            time.RefreshValue();

        }

        public override NodeResult Execute()
        {
            elapsedTime += Time.fixedDeltaTime;
            return elapsedTime < time.Value ? NodeResult.running : NodeResult.success;
        }
        public override void OnExit()
        {
            base.OnExit();
            elapsedTime = 0;
        }
    }
}
