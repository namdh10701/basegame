using _Game.Features.Inventory;
using _Game.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay
{
    [Binding]
    public class AppViewModel: RootViewModel 
    {
        #region Binding Prop: CanvasGroupAlpha

        private float _canvasGroupAlpha = 1f;

        [Binding]
        public float CanvasGroupAlpha
        {
            get => _canvasGroupAlpha;
            set
            {
                _canvasGroupAlpha = value;

                OnPropertyChanged(nameof(CanvasGroupAlpha));
            }
        }

        #endregion
        
        #region Binding Prop: ActiveMainNavIndex

        private int _activeMainNavIndex = 0;

        [Binding]
        public int ActiveMainNavIndex
        {
            get => _activeMainNavIndex;
            set
            {
                if (_activeMainNavIndex == value)
                {
                    return;
                }

                _activeMainNavIndex = value;

                OnPropertyChanged(nameof(ActiveMainNavIndex));

                if (value == 0)
                {
                    ActiveTabContentContent = HomeViewModel;
                } 
                else if (value == 1)
                {
                    ActiveTabContentContent = ShopViewModel;
                }
            }
        }

        #endregion

        #region Binding: HomeViewModel

        private SubViewModel _homeViewModel = new SubViewModel();
        
        [Binding]
        public SubViewModel HomeViewModel
        {
            get => _homeViewModel;
            set
            {
                if (_activeTabContent == value)
                {
                    return;
                }

                _homeViewModel = value;

                OnPropertyChanged(nameof(HomeViewModel));
            }
        }

        #endregion
        
        #region Binding: ShopViewModel

        private InventoryViewModel _shopViewModel = new InventoryViewModel();
        
        [Binding]
        public InventoryViewModel ShopViewModel
        {
            get => _shopViewModel;
            set
            {
                if (_activeTabContent == value)
                {
                    return;
                }

                _shopViewModel = value;

                OnPropertyChanged(nameof(ShopViewModel));
            }
        }

        #endregion

        #region Binding: ActiveTabContentContent

        [Binding]
        public SubViewModel ActiveTabContentContent
        {
            get
            {
                // Lazy init
                if (_activeTabContent == null)
                {
                    _activeTabContent = HomeViewModel;
                }

                return _activeTabContent;
            }
            set
            {
                if (_activeTabContent == value)
                {
                    return;
                }

                _activeTabContent = value;

                OnPropertyChanged(nameof(ActiveTabContentContent));
            }
        }
        private SubViewModel _activeTabContent = null;

        #endregion
    }
}