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
            if (effect is UnstackableEffect)
            {
                effect = Instantiate(effect, null);
                effect.enabled = true;
            }
            if (effect is IProbabilityEffect probEffect)
            {
                float rand = UnityEngine.Random.Range(0.0f, 1.0f);
                if (rand > probEffect.Prob)
                {
                    return;
                }
            }
            if (effect is UnstackableEffect newUnStackableEffect)
            {
                foreach (Effect ef in effects.ToArray())
                {
                    if (ef is UnstackableEffect exist && exist.Id == newUnStackableEffect.Id)
                    {
                        exist.RefreshEffect(newUnStackableEffect);
                        newUnStackableEffect.transform.parent = null;
                        Destroy(newUnStackableEffect.gameObject);
                        return;
                    }
                }
            }
            effect.OnEnded += OnEffectEnded;
            effect.Apply(EffectTaker);

            if (effect is TimeoutEffect)
            {
                effects.Add(effect);
            }
        }

        protected void OnEffectEnded(Effect effect)
        {
            if (effects.Contains(effect))
            {
                effects.Remove(effect);
            }
        }

        public void Clear()
        {
            foreach (Effect ef in effects.ToArray())
            {
                if (!ef.IsDone)
                    ef.OnEnd(EffectTaker);
            }
        }
    }

}