using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skeleton Sword/Set State")]
    public class SetState : Leaf
    {
        public EnemyState enemyState;
        public SkeletonSwordController skeletonSword;

        public override NodeResult Execute()
        {
            skeletonSword.SetState(enemyState);
            return NodeResult.success;
        }

    }
}