using _Game.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Features.WorldMap
{
    [Binding]
    public class WorldMapViewModel : SubViewModel
    {
        #region Binding: Items

        private ObservableList<WorldMapNode> nodes = new ObservableList<WorldMapNode>();

        [Binding]
        public ObservableList<WorldMapNode> Nodes => nodes;

        #endregion
    }
}