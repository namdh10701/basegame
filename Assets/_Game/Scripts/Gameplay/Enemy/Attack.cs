using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public abstract class EnemyAttackBehaviour : MonoBehaviour
    {
        [SerializeField] protected AttackPatternProfile AttackPatternProfile;
        protected GridPicker gridPicker;
        protected GridAttackHandler attackHandler;

        protected EnemyAttackData EnemyAttackData;
        [SerializeField] SpineAnimationEnemyHandler _spineAnimationEnemyHandler;

        CooldownBehaviour cooldownBehaviour;
        public bool IsAbleToAttack;

        private void Awake()
        {
            gridPicker = FindAnyObjectByType<GridPicker>();
            attackHandler = FindAnyObjectByType<GridAttackHandler>();
        }
        public void SelectCells(out EnemyAttackData enemyAttackData)
        {
            enemyAttackData = new EnemyAttackData();
            enemyAttackData.TargetCells = gridPicker.PickCells(transform, AttackPatternProfile, out Cell centerCell);
            enemyAttackData.CenterCell = centerCell;
            enemyAttackData.Effect = new DecreaseHealthEffect(2);
            attackHandler.PlayTargetingFx(enemyAttackData.TargetCells);
        }

        public IEnumerator AttackSequence()
        {
            SelectCells(out EnemyAttackData);
            yield return new WaitForSeconds(2);
            _spineAnimationEnemyHandler.PlayAnim(Anim.Attack, false, () =>
            {
                cooldownBehaviour.StartCooldown();
            });

        }
        public abstract void DoAttack();

    }
}