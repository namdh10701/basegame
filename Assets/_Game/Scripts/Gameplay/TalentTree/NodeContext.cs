using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts.Gameplay.TalentTree
{
    /// <summary>
    ///   Item context for the items in the Collection example.
    /// </summary>
    public class NodeContext : Context
    {
        // #region Binding Prop: NodeType
        //
        // /// <summary>
        // /// NodeType
        // /// </summary>
        //
        // public NodeType NodeType
        // {
        //     get => _nodeTypeProperty.Value;
        //     set => _nodeTypeProperty.Value = value;
        // }
        //
        // private readonly Property<NodeType> _nodeTypeProperty = new(NodeType.Normal);
        //
        // #endregion
        
        #region Binding Prop: IsVisible

        /// <summary>
        /// IsVisible
        /// </summary>

        public bool IsVisible
        {
            get => _isVisibleProperty.Value;
            set => _isVisibleProperty.Value = value;
        }

        private readonly Property<bool> _isVisibleProperty = new(true);

        #endregion

        // public float CanvasGroupAlpha => IsVisible ? 1 : 0;

        #region Binding Prop: CanvasGroupAlpha
        
        /// <summary>
        /// CanvasGroupAlpha
        /// </summary>
        
        public float CanvasGroupAlpha
        {
            get => _canvasGroupAlphaProperty.Value;
            set => _canvasGroupAlphaProperty.Value = value;
        }
        
        private readonly Property<float> _canvasGroupAlphaProperty = new(1f);
        
        #endregion

    }
}