using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace _Base.Scripts.RPG.Stats
{
    // [System.Serializable]
    // public class SimpleEvent : UnityEvent { };
    //
    [Serializable]
    public class Stat
    {
        // [SerializeField]
        // public SimpleEvent SimpleEvent;
        public event Action<Stat> OnValueChanged;

        public void TriggerValueChanged()
        {
            OnValueChanged?.Invoke(this);
        }

        public float BaseValue
        {
            get => _baseValue;
            set
            {
                _baseValue = value;
                UpdateValue();
            }
        }

        protected float lastBaseValue;

        protected float _value;
        public virtual float Value
        {
            get
            {
                if (Application.isEditor)
                {
                    UpdateValue();
                }
                return _value;
            }
        }

        private void UpdateValue()
        {
            lastBaseValue = BaseValue;
            var lastValue = _value;
            _value = CalculateFinalValue();
            if (lastValue != _value)
            {
                OnValueChanged?.Invoke(this);
            }
        }

        [SerializeField]
        private List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;
        [SerializeField]
        private float _baseValue;

        private readonly string name;

        public Stat(string name = "")
        {
            this.name = name;
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public Stat(float baseValue, string name = "") : this(name)
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            statModifiers.Add(mod);
            UpdateValue();
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                UpdateValue();
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

            if (numRemovals > 0)
            {
                UpdateValue();
                return true;
            }
            return false;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; //if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            statModifiers.Sort(CompareModifierOrder);

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            var result = (float)Math.Round(finalValue, 4);
            return result;
        }
    }
}
