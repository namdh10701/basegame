using _Base.Scripts.RPG.Effects;
using _Game.Features.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public enum GridItemState
    {
        Active, Broken
    }

    public interface IGridItem
    {
        public string GridId { get; set; }
        public List<Cell> OccupyCells { get; set; }
        public Transform Transform { get; }

        public GridItemStateManager GridItemStateManager { get; }

        public void OnBroken();
        public void Active();
        public bool IsBroken { get; set; }
    }
}
