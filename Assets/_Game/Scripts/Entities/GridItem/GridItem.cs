using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class GridItem : Entity
    {
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

        public List<Cell> cells;
        protected override void Awake()
        {
            base.Awake();
            if (Def.ShapeId != 0)
            {
                Shape = ShapeDic[Def.ShapeId];
            }
        }
        public GridItemDef Def;
        GridItemStats gridItemStats;
        public int[,] Shape;
        public Transform behaviour;
        public override Stats Stats => gridItemStats;
    }
}
