using System;

namespace _Base.Scripts.RPG.Stats
{
	public enum StatModType
	{
		Flat = 100,
		PercentAdd = 200,
		PercentMult = 300,
	}

	[Serializable]
	public class StatModifier
	{
		public float Value;
		public StatModType Type;
		public int Order;
		public object Source;

		public StatModifier(float value, StatModType type, int order, object source)
		{
			Value = value;
			Type = type;
			Order = order;
			Source = source;
		}

		public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

		public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

		public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }

		public static StatModifier Flat(float value) => new(value, StatModType.Flat);
		public static StatModifier Flat(float value, object source) => new(value, StatModType.Flat, source);
		public static StatModifier PercentAdd(float value) => new(value, StatModType.PercentAdd);
		public static StatModifier PercentAdd(float value, object source) => new(value, StatModType.PercentAdd, source);
		public static StatModifier PercentMult(float value) => new(value, StatModType.PercentMult);
		public static StatModifier PercentMult(float value, object source) => new(value, StatModType.PercentMult, source);
	}
}
