using System;
using UnityEngine;

namespace _Base.Scripts.RPG.Stats
{
	[Serializable]
	public class RangedStat
	{
		public event Action<RangedStat> OnValueChanged;
		
		[SerializeField]
		protected Stat _value;

		public Stat StatValue => _value;

		private float _calculatedValue;
		public virtual float Value {
			get {
				// for debug only
				if (Application.isEditor)
				{
					UpdateValue();
				}
				return _calculatedValue;
			}
		}
		
		private void UpdateValue()
		{
			var lastValue = _calculatedValue;
			_calculatedValue = Math.Max(Math.Min(_value.Value, MaxValue), MinValue);
			if (lastValue != _calculatedValue)
			{
				OnValueChanged?.Invoke(this);
			}
		}

		public float PercentageValue => _value.Value / MaxValue;
		public bool IsFull => PercentageValue >= 1;

		[SerializeField]
		protected Stat _minValue;
		public Stat MinStatValue => _minValue;

		public float MinValue => _minValue.Value;

		[SerializeField]
		protected Stat _maxValue;
		public Stat MaxStatValue => _maxValue;
		public float MaxValue => _maxValue.Value;
		
		public RangedStat()
		{
			
		}

		public RangedStat(float value, float minValue = float.MinValue, float maxValue = float.MaxValue) : this()
		{
			_minValue = new Stat(minValue);
			_maxValue = new Stat(maxValue);
			_value = new Stat(value);

			_minValue.OnValueChanged += OnAnyValueChanged;
			_maxValue.OnValueChanged += OnAnyValueChanged;
			_value.OnValueChanged += OnAnyValueChanged;
		}

		private void OnAnyValueChanged(Stat stat)
		{
			Debug.Log("Ranged dirty");
			UpdateValue();
		}

		~RangedStat()
		{
			if (_minValue != null) _minValue.OnValueChanged -= OnAnyValueChanged;
			if (_maxValue != null) _maxValue.OnValueChanged -= OnAnyValueChanged;
			if (_value != null) _value.OnValueChanged -= OnAnyValueChanged;
		}

		public RangedStat Clone()
		{
			var cloned = new RangedStat(Value, MinValue, MaxValue);
			// cloned.Value.StatModifiers
			return cloned;
		}
	}
}
