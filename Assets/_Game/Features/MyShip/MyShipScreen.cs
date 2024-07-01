using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Base.Scripts.UI;
using _Game.Scripts.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
using ZBase.UnityScreenNavigator.Core.Screens;
using ZBase.UnityScreenNavigator.Core.Sheets;
using ZBase.UnityScreenNavigator.Core.Views;
using Task = System.Threading.Tasks.Task;

namespace _Game.Features.Home
{
    public enum Sheets
    {
        MainShipSheet,
        EquipmentSheet,
        ShipSelectionSheet,
    }
    
    [Binding]
    public class MyShipScreen : RootViewModel
    {
        [SerializeField] ShipConfig _shipsConfig;
        [SerializeField] GameObject _tabEditShip;
        [SerializeField] Button _btnShipSelection;
        [SerializeField] Button _btnShipEdit;
        [SerializeField] Transform _parentShip;


        GridManager _gridManager;



        #region Sheet manager

        public SheetContainer SheetContainer;
        private Dictionary<Enum, int> _sheetIds = new Dictionary<Enum, int>();

        private async Task RegisterSheets(Type enumType)
        {
            var sheets = Enum.GetValues(enumType).Cast<Enum>();
            foreach (var sheet in sheets)
            {
                _sheetIds[sheet] = await SheetContainer.RegisterAsync(sheet.ToString());
            }
        }

        public async Task ShowSheet(Sheets sheets, params object[] args)
        {
            await SheetContainer.ShowAsync(_sheetIds[sheets], false, args);
        }

        #endregion

        public static MyShipScreen Instance;

        async void Awake()
        {
            Instance = this;
            await RegisterSheets(typeof(Sheets));
            await ShowSheet(Sheets.MainShipSheet);
            
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
    }
}