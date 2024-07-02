using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPG.Attributes
{
    public interface IAttribute
    {
        public object Value { get; set; }
        
        public object MinValue { get; set; }
        
        public object MaxValue { get; set; }
        event EventHandler<AttributeEventArgs> OnChanged;

        void NotifyChanged();
    }
    
    public interface IAttribute<T>
    {
        public T Value { get; set; }
        
        public T MinValue { get; set; }
        
        public T MaxValue { get; set; }
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
        [field: SerializeField] 
        public virtual object Value { get; set; }
        
        [field: SerializeField] 
        public virtual object MinValue { get; set; }
        
        [field: SerializeField] 
        public virtual object MaxValue { get; set; }
        public virtual event EventHandler<AttributeEventArgs> OnChanged;
        public abstract void NotifyChanged();
        public abstract object GetValue();
    }
    
    [Serializable]
    public abstract class Attribute<T>: Attribute
    {
        
        [field: SerializeField] 
        public new virtual T Value { get; set; }
        
        [field: SerializeField] 
        public new virtual T MinValue { get; set; }
        
        [field: SerializeField] 
        public new virtual T MaxValue { get; set; }

        [field: SerializeField]
        public virtual T CalculatedValue => GetFinalValue(Value, Modifiers);

        [field: SerializeField] 
        public virtual List<AttributeModifier<T>> Modifiers { get; set; } = new ();

        protected abstract T GetFinalValue(T baseValue, List<AttributeModifier<T>> modifiers);
        
        // public override event EventHandler<AttributeEventArgs> OnChanged;
        //
        // public override void NotifyChanged()
        // {
        //     IList;
        //     IList<int>;
        //     List<int>;
        //     OnChanged?.Invoke(this, new AttributeEventArgs());
        // }

        // public override object GetValue()
        // {
        //     return CalculatedValue;
        // }

        protected Attribute()
        {
        }

        protected Attribute(T value, T minValue, T maxValue)
        {
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}