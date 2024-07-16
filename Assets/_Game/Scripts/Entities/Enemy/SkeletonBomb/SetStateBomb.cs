using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Skeleton Bomb/Set State")]
    public class SetStateBomb : Leaf
    {
        public EnemyState enemyState;
        public SkeletonBombController skeletonBomb;

        public override NodeResult Execute()
        {
            skeletonBomb.SetState(enemyState);
            return NodeResult.success;
        }

    }
}