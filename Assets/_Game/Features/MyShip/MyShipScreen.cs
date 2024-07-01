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

        }
    }
}