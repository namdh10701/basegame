using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class MeeleAttack : CellAttacker
    {
        public override void DoAttack()
        {
            attackHandler.ProcessAttack(enemyAttackData.TargetCells, new DecreaseHealthEffect(2));
        }

    }
}