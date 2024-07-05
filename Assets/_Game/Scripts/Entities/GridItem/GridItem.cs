using _Base.Scripts.RPG.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public interface IGridItem
    {
        public string GridId { get; set; }
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get; }
        public Transform Behaviour { get; }
        public Transform Transform { get; }
        public void Deactivate();
        public void OnFixed();

        public bool IsBroken { get; set; }

        public EffectHandler EffectHandler { get; }

        public bool IsAbleToTakeHit { get;}
    }
}
