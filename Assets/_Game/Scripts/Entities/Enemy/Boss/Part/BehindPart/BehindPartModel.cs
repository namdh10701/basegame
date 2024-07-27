using _Base.Scripts.RPG.Effects;
using _Game.Features.Gameplay;
using _Game.Scripts.Attributes;
using _Game.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BehindPartModel : PartModel
    {
        BehindPartView lowerPartView;
        public Transform shootPos;
        public EnemyAttackData enemyAttackData;
        public System.Action OnAttack;
        public JellyFishProjectile projectilePrefab;
        public AttackPatternProfile AttackPatternProfile;
        public override void DoAttack()
        {
            base.DoAttack();
            OnAttack?.Invoke();
            SelectCells();
            JellyFishProjectile projectile = Instantiate(projectilePrefab);
            projectile.SetData(enemyAttackData, shootPos.transform.position, -15, 100);
            projectile.Launch();
            State = PartState.Idle;
        }
        void SelectCells()
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, AttackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = 100;// Take from boss stats;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };
        }
    }
}