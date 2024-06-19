using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Bullet : MonoBehaviour, IGridItem, IWorkLocation
    {
        public Projectile Projectile;

        [SerializeField] private GridItemDef def;

        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; }
        public Transform Behaviour { get => null; }
        public string GridId { get; set; }
        public List<WorkingSlot> workingSlots = new List<WorkingSlot>();
        public List<WorkingSlot> WorkingSlots { get => workingSlots; set => workingSlots = value; }
    }
}