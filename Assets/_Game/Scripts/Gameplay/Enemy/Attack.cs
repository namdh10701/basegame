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

        [SerializeField] GridPicker gridPicker;
        GridAttackHandler attackHandler;

        protected EnemyAttackData EnemyAttackData;
        [SerializeField] SpineAnimationEnemyHandler _spineAnimationEnemyHandler;
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

        public void PlayAttackSequence(Action onCompleted)
        {
            StartCoroutine(AttackSequence(onCompleted));
        }

        public IEnumerator AttackSequence(Action onCompleted)
        {
            SelectCells(out EnemyAttackData);
            yield return new WaitForSeconds(2);
            _spineAnimationEnemyHandler.PlayAttackAnim(false);
            onCompleted?.Invoke();
        }
        public abstract void DoAttack();

    }
}