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

        public void Deactivate();
        public void OnFixed();

        public bool IsBroken { get; set; }
    }
}
