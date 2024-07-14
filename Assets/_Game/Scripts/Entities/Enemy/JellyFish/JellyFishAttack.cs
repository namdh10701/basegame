using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class JellyFishAttack : MonoBehaviour
    {
        public bool IsLeftHandAttack;
        public Transform leftShootPos;
        public Transform rightShootPos;
        public JellyFishProjectile projectilePrefab;
        public EnemyAttackData enemyAttackData;

        public AttackPatternProfile AttackPatternProfile;
        public AttackPatternProfile meeleAttackProfile;

        public GridAttackHandler gridAttackHandler;
        public GridPicker gridPicker;
        public Stat AttackDamage;
        public JellyFishModel JellyFish;
        public CameraShake cameraShake;
        private void Start()
        {
            gridAttackHandler = FindAnyObjectByType<GridAttackHandler>();
            gridPicker = FindAnyObjectByType<GridPicker>();
            AttackDamage = ((EnemyStats)JellyFish.Stats).AttackDamage;

        }
        public void DoLeftAttack()
        {
            IsLeftHandAttack = false;
            SelectCells();
            JellyFishProjectile projectile = Instantiate(projectilePrefab);
            projectile.SetData(enemyAttackData, leftShootPos.transform.position, 15, AttackDamage.Value);
            projectile.Launch();
        }

        public void DoRightAttack()
        {
            IsLeftHandAttack = true;
            SelectCells();
            JellyFishProjectile projectile = Instantiate(projectilePrefab);
            projectile.SetData(enemyAttackData, rightShootPos.transform.position, -15, AttackDamage.Value);
            projectile.Launch();
        }

        void SelectCells()
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, AttackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = AttackDamage.Value;// Take from boss stats;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };
        }


        public void DoLeftMeleeAttack()
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(leftShootPos, meeleAttackProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;

            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = AttackDamage.Value;// Take from boss stats;
            enemyAttackData.Effects = new List<Effect> { decreaseHp };

            gridAttackHandler.ProcessAttack(enemyAttackData);
            cameraShake.Shake(.2f);
        }

        public void DoRightMelleAttack()
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(rightShootPos, meeleAttackProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
            DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
            decreaseHp.Amount = AttackDamage.Value;// Take from boss stats;

            enemyAttackData.Effects = new List<Effect> { decreaseHp };

            gridAttackHandler.ProcessAttack(enemyAttackData);
            cameraShake.Shake(.2f);
        }
    }
}