using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public abstract class EffectProcessor: MonoBehaviour
    {
        public void Process(Entity entity)
        {
            // foreach (var attr in entity.Attributes)
            // {
            //     // attr
            // }

            foreach (var effect in entity.Effects)
            {
                effect.Apply();
            }
        }
    }
}