using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Base.Scripts.RPG.Attributes
{

    [Serializable]
    public class FloatAttribute: Attribute<float>
    {
        protected override float GetFinalValue(float baseValue, List<AttributeModifier<float>> modifiers)
        {
            return modifiers.Sum(v => v.Value) + baseValue;
        }
    }
}