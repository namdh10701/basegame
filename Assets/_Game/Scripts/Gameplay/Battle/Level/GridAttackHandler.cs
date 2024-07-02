using _Base.Scripts.RPG.Effects;
using _Base.Scripts.UI;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class GridAttackHandler : MonoBehaviour
    {
        public Fx fxATKPrefab;
        public _Game.Scripts.Gameplay.Ship.Ship ship;
        public void ProcessAttack(EnemyAttackData enemyAttackData)
        {
            ProcessAttack(enemyAttackData.TargetCells, enemyAttackData.Effects);
        }

        void ProcessAttack(List<Cell> cells, List<Effect> effects)
        {
            foreach (Effect effect in effects)
            {
                ship.EffectHandler.Apply(effect);
                foreach (Cell cell in cells)
                {
                    if (cell != null)
                    {
                        cell.EffectHandler.Apply(effect);
                    }
                }
            }
            PlayFx(cells);
        }

        void PlayFx(List<Cell> cells)
        {
            if (fxATKPrefab == null)
            {
                return;
            }
            foreach (Cell cell in cells)
            {
                Fx go = Instantiate(fxATKPrefab, null);
                go.transform.position = cell.transform.position;
            }
        }
    }
}