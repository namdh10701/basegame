using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [System.Serializable]
    public class Cell : Entity, IEffectTaker
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
        public EffectTakerCollider EffectCollider;
        protected override void Awake()
        {
            base.Awake();
            EffectCollider.Taker = this;
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
    }
}


