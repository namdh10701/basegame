using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public class EffectHandler : MonoBehaviour
    {
        public IEffectTaker EffectTaker;

        [HideInInspector] public List<Effect> effects = new List<Effect>();

        public virtual void Apply(Effect effect)
        {
            effects.Add(effect);
            effect.Apply(EffectTaker);
            if (effect.IsDone)
            {
                effects.Remove(effect);
            }
        }
    }

}