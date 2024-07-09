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
            if (effect is UnstackableEffect newUnStackableEffect)
            {
                foreach (Effect ef in effects.ToArray())
                {
                    if (ef is UnstackableEffect exist && exist.Id == newUnStackableEffect.Id)
                    {
                        exist.RefreshEffect(newUnStackableEffect);
                        Destroy(newUnStackableEffect.gameObject);
                        return;
                    }
                }
            }
            effect.Apply(EffectTaker);
            effects.Add(effect);
        }

        private void OnEffectEnded(Effect effect)
        {
            if (effects.Contains(effect))
            {
                effects.Remove(effect);
            }
        }
    }

}