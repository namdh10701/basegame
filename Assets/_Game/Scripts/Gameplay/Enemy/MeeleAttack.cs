using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class MeeleAttack : EnemyAttackBehaviour
    {
        public GridAttackHandler GridAttackHandler;
        private void Start()
        {
            GridAttackHandler = FindAnyObjectByType<GridAttackHandler>();
        }
        public override void DoAttack()
        {
            Debug.Log("MElleATK");
            GridAttackHandler.ProcessAttack(targetCells, new DecreaseHealthEffect(2));
        }
    }
}