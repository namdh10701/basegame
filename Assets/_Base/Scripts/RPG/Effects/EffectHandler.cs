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
            if (effect is SlowEffect slowEf)
            {
                foreach (Effect ef in EffectTaker.EffectHandler.effects.ToArray())
                {
                    if (ef is SlowEffect slowef)
                    {
                        if (slowef.id == slowEf.id)
                        {
                            slowef.OnEnd(EffectTaker);
                        }
                    }
                }
            }
            effect.OnEnded += OnEffectEnded;
            effects.Add(effect);
            effect.Apply(EffectTaker);
        }

        private void OnEffectEnded(Effect effect)
        {
            foreach (Effect ef in effects.ToArray())
            {
                if (ef == effect)
                {
                    effects.Remove(effect);
                }
            }
        }
    }

}