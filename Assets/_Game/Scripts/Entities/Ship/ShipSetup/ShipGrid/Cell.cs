using _Base.Scripts.RPG.Effects;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [System.Serializable]
    public class Cell : MonoBehaviour, IEffectTaker
    {
        public CellRenderer CellRenderer;
        public int X;
        public int Y;
        public Grid Grid;
        public IGridItem GridItem;
        public CellEffectHandler effectHandler;
        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        private void Start()
        {
            effectHandler = GetComponent<CellEffectHandler>();
            GetComponent<EffectTakerCollider>().Taker = this;
        }
    }
}


