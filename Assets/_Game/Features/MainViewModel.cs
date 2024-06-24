using System.Threading.Tasks;
using _Base.Scripts.UI.Managers;
using _Base.Scripts.Utils;
using _Game.Features.Home;
using _Game.Features.Inventory;
using _Game.Features.WorldMap;
using _Game.Scripts.GD;
using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Scripts.Gameplay
{
    [Binding]
    public class MainViewModel: RootViewModel 
    {
        public enum Nav
        {
            HOME,
            SHOP,
            SHIP,
            INVENTORY,
            RUG,
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

                // NavTo((Nav)value);
            }
        }

        #endregion

        private void OnActiveMainNavIndexChanged(int navIndex)
        {
            // var nav = (Nav)navIndex;
            // switch (nav)
            // {
            //     // case Nav.HOME:
            //     //     ActiveViewModel = _homeViewModel;
            //     //     break;
            //     
            //     case Nav.INVENTORY:
            //         // ActiveViewModel = _inventoryViewModel;
            //         break;
            //     
            //     case Nav.WORLD_MAP:
            //         ActiveViewModel = _WorldMapViewModel;
            //         break;
            // }
            //
            // if (!ActiveViewModel.IsInitialized)
            // {
            //     ActiveViewModel.Initialize();
            // }
            //
            // OnPropertyChanged(nameof(IsMainNavVisible));
            //
            // // if (nav > Nav.RUG)
            // // {
            // //     
            // // }
        }

        private async void Awake()
        {
            // IOC.Register(this);
            // // GDConfigLoader.Instance.OnLoaded += Init;
            // await GDConfigLoader.Instance.Load();
        }

        // private void Init()
        // {
        //     
        // }

        // [Binding]
        // public void NavToWorldMap()
        // {
        //     // PopupManager.Instance.ShowPopup<GearInfoPopup>();
        //     NavTo(Nav.WORLD_MAP);
        // }
        //
        // [Binding]
        // public void NavToSeaMap()
        // {
        //     NavTo(Nav.SEA_MAP);
        // }
        //
        // [Binding]
        // public void NavToHome()
        // {
        //     NavTo(Nav.HOME);
        // }
        //
        // public async void NavTo(Nav targetNav)
        // {
        //     _currentNav = targetNav;
        //     switch (targetNav)
        //     {
        //         // case Nav.HOME:
        //         //     ActiveViewModel = _homeViewModel;
        //         //     break;
        //         
        //         case Nav.INVENTORY:
        //             // ActiveViewModel = _inventoryViewModel;
        //             break;
        //         
        //         case Nav.WORLD_MAP:
        //             ActiveViewModel = _WorldMapViewModel;
        //             break;
        //     }
        //     
        //     if (ActiveViewModel != null && !ActiveViewModel.IsInitialized)
        //     {
        //         ActiveViewModel.Initialize();
        //     }
        //     
        //     await ScreenContainer.Of(transform).PreloadAsync("HomeView");
        //     
        //     OnPropertyChanged(nameof(IsMainNavVisible));
        // }
    }
}