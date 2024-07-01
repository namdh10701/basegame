using System;
using _Base.Scripts.UI;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Views;

namespace _Game.Features.Home
{
    [Binding]
    public class MyShipScreen : RootViewModel
    {
        [SerializeField] ShipConfig _shipsConfig;
        [SerializeField] GameObject _tabEditShip;
        [SerializeField] Button _btnShipSelection;
        [SerializeField] Button _btnShipEdit;
        [SerializeField] Transform _parentShip;

        GridManager _gridManager;

        void Awake()
        {
            Initialize("0001");
        }

        void OnEnable()
        {
            _gridManager.Initialize();
            _gridManager.LoadInventoryItems();
            EnableDragItem(false);

            _btnShipSelection.onClick.AddListener(OnShipSelectionClick);
            _btnShipEdit.onClick.AddListener(OnShipEditClick);
        }

        void OnDisable()
        {
            _btnShipSelection.onClick.RemoveListener(OnShipSelectionClick);
            _btnShipEdit.onClick.RemoveListener(OnShipEditClick);
        }

        void Initialize(string shipID)
        {
            foreach (var ship in _shipsConfig.ships)
            {
                if (ship.id == shipID)
                {
                    var shipObject = Instantiate(ship.ship);
                    shipObject.transform.SetParent(_parentShip, false);
                    _gridManager = shipObject.GetComponentInChildren<GridManager>();
                }
            }
        }

        private void EnableDragItem(bool enable)
        {
            _gridManager.EnableDragItem(enable);
            _tabEditShip.SetActive(enable);
        }

        private void OnShipEditClick()
        {
            EnableDragItem(true);
        }

        private void OnShipSelectionClick()
        {
            // _tabEditShip.SetActive(false);
            // _gridImage.SetActive(false);
            // _tash.SetActive(false);
        }



        [Binding]
        public async void NavToShipSelection()
        {
            var options = new ViewOptions("ShipSelectionScreen");

            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }

        [Binding]
        public async void NavToEquipmentScreen()
        {
            var options = new ViewOptions("EquipmentScreen");

            await ScreenContainer.Find(ContainerKey.Screens).PushAsync(options);
        }
    }
}