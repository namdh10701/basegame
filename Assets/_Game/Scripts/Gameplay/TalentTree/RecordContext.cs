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

        public NodeViewModel NormalNode
        {
            get => _normalNodeProperty.Value;
            set => _normalNodeProperty.Value = value;
        }

        private readonly Property<NodeViewModel> _normalNodeProperty = new(new NodeViewModel());

        #endregion

        #region Binding Prop: LevelNode

        /// <summary>
        /// LevelNode
        /// </summary>

        public NodeViewModel LevelNode
        {
            get => _levelNodeProperty.Value;
            set => _levelNodeProperty.Value = value;
        }

        private readonly Property<NodeViewModel> _levelNodeProperty = new(new NodeViewModel());

        #endregion

        #region Binding Prop: PremiumNode

        /// <summary>
        /// PremiumNode
        /// </summary>

        public NodeViewModel PremiumNode
        {
            get => _premiumNodeProperty.Value;
            set => _premiumNodeProperty.Value = value;
        }

        private readonly Property<NodeViewModel> _premiumNodeProperty = new(new NodeViewModel());

        #endregion
    }
}