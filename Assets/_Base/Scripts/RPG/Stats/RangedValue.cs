using System;
using UnityEngine;

namespace _Base.Scripts.RPG.Stats
{
	[Serializable]
	public class RangedValue
	{
		public event Action<RangedValue> OnValueChanged;

		[SerializeField]
		protected float _value;
		public float Value
		{
			get => _value;

			set
			{
				_value = value;
				UpdateValue();
			}
		}
		
		public float PercentageValue => _value / MaxValue;
		public bool IsFull => PercentageValue >= 1;

		[SerializeField]
		protected float _minValue;
		public float MinValue
		{
			get => _minValue;

			set
			{
				_minValue = value;
				UpdateValue();
			}
		}
		
		[SerializeField]
		protected float _maxValue;
		public float MaxValue
		{
			get => _maxValue;

			set
			{
				_maxValue = value;
				UpdateValue();
			}
		}

		private void UpdateValue()
		{
			var before = _value;
			
			// if (MaxValue.HasValue)
			// {
			// 	_value = Mathf.Min(_value, MaxValue.Value);
			// }
			//
			// if (MinValue.HasValue)
			// {
			// 	_value = Mathf.Max(_value, MinValue.Value);
			// }
			
			_value = Mathf.Min(_value, MaxValue);
			_value = Mathf.Max(_value, MinValue);
			
			// Workaround: Fix float calc error
			_value = (float)Math.Round(_value, 4);
			
			if (before != _value)
			{
				OnValueChanged?.Invoke(Clone());
			}
		}
		
		public RangedValue()
		{
			
		}

		public RangedValue(float value, float minValue = float.MinValue, float maxValue = float.MaxValue) : this()
		{
			MinValue = minValue;
			MaxValue = maxValue;
			Value = value;
		}

		public RangedValue Clone() => new RangedValue(Value, MinValue, MaxValue);
	}
}
