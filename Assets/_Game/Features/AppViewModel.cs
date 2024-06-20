using System.Threading.Tasks;
using _Game.Features.Home;
using _Game.Features.Inventory;
using _Game.Scripts.GD;
using _Game.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay
{
    [Binding]
    public class AppViewModel: RootViewModel 
    {
        public enum Nav
        {
            HOME,
            SHOP,
            SHIP,
            INVENTORY,
            RUG
        }
        
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

                OnActiveMainNavIndexChanged(value);
            }
        }

        #endregion

        #region Binding: HomeViewModel

        private HomeViewModel _homeViewModel = new HomeViewModel();
        
        [Binding]
        public HomeViewModel HomeViewModel
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
        
        #region Binding: InventoryViewModel

        private InventoryViewModel _inventoryViewModel = new InventoryViewModel();
        
        [Binding]
        public InventoryViewModel InventoryViewModel
        {
            get => _inventoryViewModel;
            set
            {
                if (_activeTabContent == value)
                {
                    return;
                }

                _inventoryViewModel = value;

                OnPropertyChanged(nameof(InventoryViewModel));
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
        
        
        private void OnActiveMainNavIndexChanged(int navIndex)
        {
            var nav = (Nav)navIndex;
            switch (nav)
            {
                case Nav.HOME:
                    ActiveTabContentContent = HomeViewModel;
                    break;
                
                case Nav.INVENTORY:
                    ActiveTabContentContent = InventoryViewModel;
                    break;
            }
            
            if (!ActiveTabContentContent.IsInitialized)
            {
                ActiveTabContentContent.Initialize();
            }
        }

        private async void Awake()
        {
            // GDConfigLoader.Instance.OnLoaded += Init;
            await GDConfigLoader.Instance.Load();
        }

        // private void Init()
        // {
        //     
        // }

        [Binding]
        public void NavToWorldMap()
        {
            
        }
    }
}