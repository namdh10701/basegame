using _Base.Scripts.RPG.Stats;
using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts.Gameplay.TalentTree
{
    public class TreeContext: Context
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
    }
}