using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public class EffectHandler: MonoBehaviour
    {
        // public List<StatModifier> StatModifiers;
        [SerializeField]
        private Entity entity;

        [SerializeField]
        private List<EffectX> effects = new List<EffectX>();

        public void Apply(EffectX effect)
        {
            effects.Add(effect);
            effect.Apply(entity);
            if (effect.IsDone)
            {
                effects.Remove(effect);
            }
        }
    }

}