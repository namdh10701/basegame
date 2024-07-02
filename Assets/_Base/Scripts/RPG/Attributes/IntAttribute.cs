using System;
using System.Collections.Generic;
using System.Linq;

namespace _Base.Scripts.RPG.Attributes
{

    [Serializable]
    public class IntAttribute: Attribute<int>
    {
        protected override int GetFinalValue(int baseValue, List<AttributeModifier<int>> modifiers)
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