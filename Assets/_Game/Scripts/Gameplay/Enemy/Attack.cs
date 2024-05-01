using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public abstract class EnemyAttackBehaviour : MonoBehaviour
    {
        [SerializeField] protected AttackPatternProfile AttackPatternProfile;

        [SerializeField] protected List<Cell> targetCells;
        [SerializeField] protected Cell centerCell;

        [SerializeField] GridPicker gridPicker;

        private void Awake()
        {
            gridPicker = FindAnyObjectByType<GridPicker>();
        }
        public void SelectCells()
        {
            targetCells = gridPicker.PickCells(transform, AttackPatternProfile, out Cell centerCell);
            this.centerCell = centerCell;
        }

        public abstract void DoAttack();
    }
}