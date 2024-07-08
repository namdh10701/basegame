using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Sheets;
using Task = System.Threading.Tasks.Task;

namespace _Game.Features.MyShipScreen
{
    public enum Sheets
    {
        NewShipEditSheet,
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
            await ShowSheet(Sheets.NewShipEditSheet);

        }
    }
}