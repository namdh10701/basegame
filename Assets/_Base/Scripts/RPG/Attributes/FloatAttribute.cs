using System;
using System.Collections.Generic;
using System.Linq;

namespace _Base.Scripts.RPG.Attributes
{

    [Serializable]
    public class FloatAttribute: Attribute<float>
    {
        protected override float GetFinalValue(float baseValue, List<AttributeModifier<float>> modifiers)
        {
            return modifiers.Sum(v => v.Value) + baseValue;
        }

        public override void NotifyChanged()
        {
            
        }

        public override object GetValue()
        {
            return null;
        }
    }
}