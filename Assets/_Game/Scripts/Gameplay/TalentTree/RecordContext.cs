using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts.Gameplay.TalentTree
{
    /// <summary>
    ///   Item context for the items in the Collection example.
    /// </summary>
    public class RecordContext : Context
    {
        #region Binding Prop: NormalNode

        /// <summary>
        /// NormalNode
        /// </summary>

        public NodeContext NormalNode
        {
            get => _normalNodeProperty.Value;
            set => _normalNodeProperty.Value = value;
        }

        private readonly Property<NodeContext> _normalNodeProperty = new();

        #endregion

        #region Binding Prop: LevelNode

        /// <summary>
        /// LevelNode
        /// </summary>

        public NodeContext LevelNode
        {
            get => _levelNodeProperty.Value;
            set => _levelNodeProperty.Value = value;
        }

        private readonly Property<NodeContext> _levelNodeProperty = new();

        #endregion

        #region Binding Prop: PremiumNode

        /// <summary>
        /// PremiumNode
        /// </summary>

        public NodeContext PremiumNode
        {
            get => _premiumNodeProperty.Value;
            set => _premiumNodeProperty.Value = value;
        }

        private readonly Property<NodeContext> _premiumNodeProperty = new();

        #endregion
    }
}