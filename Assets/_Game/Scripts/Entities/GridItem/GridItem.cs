using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public interface IGridItem { 
    
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get;}
        public Transform Behaviour { get;}
    }
}
