using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts.Gameplay.TalentTree
{
    /// <summary>
    ///   Item context for the items in the Collection example.
    /// </summary>
    public class NodeContext : Context
    {
        #region Binding Prop: NodeType

        /// <summary>
        /// NodeType
        /// </summary>

        public NodeType NodeType
        {
            get => _nodeTypeProperty.Value;
            set => _nodeTypeProperty.Value = value;
        }

        private readonly Property<NodeType> _nodeTypeProperty = new();

        #endregion
    }
}