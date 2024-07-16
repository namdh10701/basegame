using _Base.Scripts.RPG;
using _Game.Scripts.Battle;
using _Game.Scripts;
using _Game.Scripts.Entities;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace _Game.Features.Gameplay
{
    public abstract class EnemyController : MonoBehaviour
    {
        protected EnemyModel enemyModel;
        [SerializeField] protected EnemyView enemyView;
        public virtual void Initialize(EnemyModel enemyModel)
        {
            this.enemyModel = enemyModel;
            enemyView.OnAttack += enemyModel.PerformAttack;
        }

        protected virtual void PerformAttack()
        {
            enemyModel.PerformAttack();
        }

        public void SetState()
        {

        }
    }
}