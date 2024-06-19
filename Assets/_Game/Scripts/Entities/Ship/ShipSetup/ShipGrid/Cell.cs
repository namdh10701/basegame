using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [System.Serializable]
    public class Cell : Entity, IEffectTaker, IWorkLocation
    {
        public CellRenderer CellRenderer;
        public int X;
        public int Y;
        public Grid Grid;
        public IGridItem GridItem;
        public EffectHandler effectHandler;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;
        public CellStats stats;
        public CellStatsTemplate template;
        public override Stats Stats => stats;

        public List<WorkingSlot> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<WorkingSlot> workingSlots;
        public EffectTakerCollider EffectCollider;
        protected override void Awake()
        {
            base.Awake();
            EffectCollider.Taker = this;
            workingSlots = new List<WorkingSlot>()
            {
                new WorkingSlot(this)
        };
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        protected override void ApplyStats()
        {

        }

        protected override void LoadModifiers()
        {

        }

        protected override void LoadStats()
        {

        }

        public void OnBroken()
        {
            GlobalEvent<Cell>.Send("Broken", this);
        }
        public void OnFixed()
        {
            CellRenderer.OnFixed();
        }
    }
}


