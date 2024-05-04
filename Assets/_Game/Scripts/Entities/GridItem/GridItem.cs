using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public interface IGridItem { 
    
        public static Dictionary<int, int[,]> ShapeDic = new Dictionary<int, int[,]>()
    {
        {
            1,
            new int[,]
            {
                { 1, 1 },
                { 1, 1 }
            }
        },{
            2,
            new int[,]
            {
                { 1, 0 },
                { 1, 1 }
            }
        },{
            3,
            new int[,]
            {
                { 1, 1, 1, 1 },
                { 1, 0, 0, 0 }
            }
        },{
            4,
            new int[,]
            {
                { 1, 1, 1 },
                { 1 ,0, 1 },
                { 1 ,0, 1 }
            }
        }
    };

        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get; set; }
        public Transform Behaviour { get; set;}
    }
}
