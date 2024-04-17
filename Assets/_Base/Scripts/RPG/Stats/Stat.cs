using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace _Base.Scripts.RPG.Stats
{
	[Serializable]
	public class Stat
	{
		public event Action<Stat> OnValueChanged;
		public float BaseValue;
		public float? MinValue;
		public float? MaxValue;

		protected bool isDirty = true;
		protected float lastBaseValue;
		protected float? lastMinValue;
		protected float? lastMaxValue;

		protected float _value;
		public virtual float Value {
			get {
				if(isDirty || lastBaseValue != BaseValue || lastMinValue != MinValue || lastMaxValue != MaxValue)
				{
					lastBaseValue = BaseValue;
					lastMinValue = MinValue;
					lastMaxValue = MaxValue;
					_value = CalculateFinalValue();
					isDirty = false;
					
					OnValueChanged?.Invoke(this);
				}
				return _value;
			}
		}

		[SerializeField]
		private List<StatModifier> statModifiers;
		public readonly ReadOnlyCollection<StatModifier> StatModifiers;

		public Stat()
		{
			statModifiers = new List<StatModifier>();
			StatModifiers = statModifiers.AsReadOnly();
		}

		public Stat(float baseValue, float? minValue = null, float? maxValue = null) : this()
		{
			BaseValue = baseValue;
			MinValue = minValue;
			MaxValue = maxValue;
		}

		public virtual void AddModifier(StatModifier mod)
		{
			isDirty = true;
			statModifiers.Add(mod);
		}

		public virtual bool RemoveModifier(StatModifier mod)
		{
			if (statModifiers.Remove(mod))
			{
				isDirty = true;
				return true;
			}
			return false;
		}

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

			if (numRemovals > 0)
			{
				isDirty = true;
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

			if (MaxValue.HasValue)
			{
				result = Mathf.Min(result, MaxValue.Value);
			}

			if (MinValue.HasValue)
			{
				result = Mathf.Max(result, MinValue.Value);
			}
			return result;
		}
	}
}
