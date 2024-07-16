using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [AddComponentMenu("")]
    [MBTNode("Enemy Model/Set State")]
    public class SetState : Leaf
    {
        public EnemyState enemyState;
        public EnemyReference enemyReference;

        public override NodeResult Execute()
        {
            enemyReference.Value.State = enemyState;
            return NodeResult.success;
        }

    }
}