using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Attributes;
using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts
{
    public class PlayerContext: Context
    {
        #region Binding Prop: HealthPoint

        /// <summary>
        /// HealthPoint
        /// </summary>

        public RangedValue HealthPoint
        {
            get => healthPointProperty.Value;
            set => healthPointProperty.Value = value;
        }

        private readonly Property<RangedValue> healthPointProperty = new(new RangedValue(0));

        #endregion
        
        #region Binding Prop: MaxHealthPoint

        /// <summary>
        /// MaxHealthPoint
        /// </summary>
        public float MaxHealthPoint
        {
            get => maxHealth.Value;
            set => maxHealth.Value = value;
        }

        private readonly Property<float> maxHealth = new(0);
        #endregion
        
        #region Binding Prop: ManaPoint

        /// <summary>
        /// ManaPoint
        /// </summary>

        public RangedValue ManaPoint
        {
            get => manaPointProperty.Value;
            set => manaPointProperty.Value = value;
        }

        private readonly Property<RangedValue> manaPointProperty = new(new RangedValue(0));

        #endregion

        #region Binding Prop: MaxManaPoint

        /// <summary>
        /// MaxManaPoint
        /// </summary>
        public float MaxManaPoint
        {
            get => maxMana.Value;
            set => maxMana.Value = value;
        }

        private readonly Property<float> maxMana = new(0);
        #endregion

        public string HealthStatus => $"{HealthPoint}/{MaxHealthPoint}";
        public string ManaStatus => $"{ManaPoint}/{MaxManaPoint}";
    }
}