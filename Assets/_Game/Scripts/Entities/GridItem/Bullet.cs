using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Bullet : Entity, IGridItem
    {
        public Projectile Projectile;

        [SerializeField] private GridItemDef def;

        [SerializeField] private Transform behaviour;
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; }
        public Transform Behaviour { get => behaviour; }

        public CannonStats cs;
        public override Stats Stats => cs;
    }
}