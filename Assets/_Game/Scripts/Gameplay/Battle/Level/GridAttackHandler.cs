using _Base.Scripts.RPG.Effects;
using _Base.Scripts.UI;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class GridAttackHandler : MonoBehaviour
    {
        public Fx fxPrefab;
        public Fx fxATKPrefab;
        public _Game.Scripts.Gameplay.Ship.Ship ship;
        public void ProcessTargeting(List<Cell> cells)
        {
            PlayTargetingFx(cells);
        }

        public void ProcessAttack(List<Cell> cells, List<Effect> effect)
        {
            List<IGridItem> placements = new List<IGridItem>();
            List<Cell> emptyCells = new List<Cell>();

            foreach (Effect ef in effect)
            {
                ApplyEffect(placements, ef);
                ApplyEffectEmptyCells(cells, ef);
            }
            PlayFx(cells);
        }

        public void ApplyEffectEmptyCells(List<Cell> cells, Effect effect)
        {
            foreach (Cell cell in cells)
            {
                ship.EffectHandler.Apply(effect);
            }
        }

        public void ApplyEffect(List<IGridItem> placements, Effect effect)
        {
            foreach (IEffectTaker placement in placements)
            {
                placement.EffectHandler.Apply(effect);
            }
        }
        public void ApplyEffect(IEffectTaker placement, Effect effect)
        {
            placement.EffectHandler.Apply(effect);
        }

        public void PlayFx(List<Cell> cells)
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

        public void PlayTargetingFx(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                Fx go = Instantiate(fxPrefab, null);
                go.transform.position = cell.transform.position;
            }
        }
    }
}