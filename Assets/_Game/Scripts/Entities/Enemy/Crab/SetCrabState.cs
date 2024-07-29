using _Game.Scripts;
using BehaviorDesigner.Runtime.Tasks;
using MBT;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Crab/Set Crab State")]

    public class SetCrabState : Leaf
    {
        public CrabState crabState;
        public Crab crab;


        public override NodeResult Execute()
        {
            crab.CrabState = crabState;
            return NodeResult.success;

        }
    }
}