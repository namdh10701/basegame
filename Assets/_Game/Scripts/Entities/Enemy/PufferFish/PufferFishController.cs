using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Entities;
using DG.Tweening;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class PufferFishController : EnemyController
    {
        PufferFishView pufferFishAnim;
        public PufferFishMove pufferFishMove;
        public DamageArea damageArea;

        bool isAttacking;

        void ReducePoise()
        {
            EnemyStats stats = enemyModel.Stats as EnemyStats;
            stats.Poise.BaseValue = 0;
        }

        public void Move()
        {
            pufferFishMove.Move();
        }
    }
}