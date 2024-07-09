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

        public List<Effect> effects = new List<Effect>();

        public virtual void Apply(Effect effect)
        {
            effect.OnEnded += OnEffectEnded;
            effect.Apply(EffectTaker);
            effects.Add(effect);
        }

        private void OnEffectEnded(Effect effect)
        {
            foreach (Effect ef in effects.ToArray())
            {
                if (ef == effect)
                {
                    effect.OnEnded -= OnEffectEnded;
                    effects.Remove(effect);
                }
            }
        }
    }

}