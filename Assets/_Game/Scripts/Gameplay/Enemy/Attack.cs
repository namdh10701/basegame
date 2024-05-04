using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public abstract class EnemyAttackBehaviour : MonoBehaviour
    {
        [SerializeField] protected AttackPatternProfile AttackPatternProfile;

        [SerializeField] GridPicker gridPicker;
        GridAttackHandler attackHandler;

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
            attackHandler.PlayTargetingFx(enemyAttackData.TargetCells);
        }
        public IEnumerator PlayAttackSequence()
        {
            SelectCells(out EnemyAttackData enemyAttackData);
            yield return new WaitForSeconds(2);
            DoAttack(enemyAttackData);
        }
        public abstract void DoAttack(EnemyAttackData enemyAttackData);
    }
}