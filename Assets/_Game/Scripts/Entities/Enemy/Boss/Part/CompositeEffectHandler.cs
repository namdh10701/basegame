
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public class CompositeEffectHandler : EffectHandler
    {
        public IEffectTaker Other;
        public override void Apply(Effect effect)
        {
            base.Apply(effect);
            Other.EffectHandler.Apply(effect);
        }
    }
}