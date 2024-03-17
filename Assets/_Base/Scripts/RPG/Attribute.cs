using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public interface IAttribute
    {
        event EventHandler<AttributeEventArgs> OnChanged;

        void NotifyChanged();
    }

    [Serializable]
    public class Test
    {
        
    }

    [Serializable]
    public abstract class Attribute: MonoBehaviour, IAttribute
    {
        public virtual event EventHandler<AttributeEventArgs> OnChanged;
        public abstract void NotifyChanged();
        public abstract object GetValue();
    }
    
    [Serializable]
    public abstract class Attribute<T>: Attribute
    {

        [field: SerializeField] 
        public virtual T Value { get; set; }
        
        public override event EventHandler<AttributeEventArgs> OnChanged;
        
        public override void NotifyChanged()
        {
            OnChanged?.Invoke(this, new AttributeEventArgs());
        }

        public override object GetValue()
        {
            return Value;
        }
    }
}