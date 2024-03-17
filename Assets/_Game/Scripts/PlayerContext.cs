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

        public int HealthPoint
        {
            get => healthPointProperty.Value;
            set => healthPointProperty.Value = value;
        }

        private readonly Property<int> healthPointProperty = new(0);

        #endregion
        
        #region Binding Prop: ManaPoint

        /// <summary>
        /// ManaPoint
        /// </summary>

        public int ManaPoint
        {
            get => manaPointProperty.Value;
            set => manaPointProperty.Value = value;
        }

        private readonly Property<int> manaPointProperty = new();

        #endregion
    }
}