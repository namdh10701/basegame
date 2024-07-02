using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Equipment;
using _Game.Features.Home;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using InventoryItem = _Game.Features.Inventory.InventoryItem;

namespace _Game.Features.MyShip
{
    [Binding]
    public class EquipmentSheet : SheetWithViewModel
    {
        public class EquipmentSheetOutputData : MainShipSheet.InputData<List<InventoryItem>>
        {
            public EquipmentSheetOutputData(List<InventoryItem> data) : base(data)
            {
            }
        }

        public EquipmentViewModel EquipmentViewModel;

        [Binding]
        public async void NavBack()
        {
            await MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet);
        }
        
        [Binding]
        public async void NavBackWithParams()
        {
            var selectedItems = EquipmentViewModel.Items.Where(v => v.IsSelected).ToList();
            var output = new EquipmentSheetOutputData(selectedItems);
            
            await MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet, output);
        }
        
        public override UniTask WillEnter(Memory<object> args)
        {
            var inputInventoryItemList = args.ToArray().FirstOrDefault() as List<InventoryItemData>;
            Debug.Log("inventoryItemList: " + inputInventoryItemList?.Count);

            if (inputInventoryItemList == null)
            {
                return UniTask.CompletedTask;
            }
            
            EquipmentViewModel.IgnoreIdList.Clear();
            EquipmentViewModel.IgnoreIdList.AddRange(inputInventoryItemList.Select(v => v.gridItemDef.Id));
            return UniTask.CompletedTask;
        }
    }
}