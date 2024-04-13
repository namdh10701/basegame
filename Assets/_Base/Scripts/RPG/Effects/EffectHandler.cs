using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public class EffectHandler: MonoBehaviour
    {
        // public List<StatModifier> StatModifiers;
        [SerializeField]
        private Entity entity;

        public List<EffectX> effects = new List<EffectX>();

        public void Apply(EffectX effect)
        {
            effects.Add(effect);
            effect.Apply(entity);
            if (effect.IsDone)
            {
                // effects.Remove(effect);
            }
        }
    }

}