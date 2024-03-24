using System;
using UnityEngine;

namespace _Base.Scripts.RPG.Attributes
{
    [Serializable]
    public class AttributeModifier<T>
    {
        public AttributeModifier(T value)
        {
            Value = value;
        }

        [field: SerializeField] 
        public virtual T Value { get; set; }

    }
}