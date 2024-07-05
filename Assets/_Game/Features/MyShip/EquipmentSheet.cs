using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Game.Features.Equipment;
using _Game.Features.MyShipScreen;
using _Game.Features.InventoryItemInfo;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;
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
            await MyShipScreen.MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet);
        }

        [Binding]
        public async void NavBackWithParams()
        {
            var selectedItems = EquipmentViewModel.SelectedItems;
            var output = new EquipmentSheetOutputData(selectedItems);

            await MyShipScreen.MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet, output);
        }

        [Binding]
        public async void ShowInfo()
        {
            var selectedItems = EquipmentViewModel.SelectedItems.ToList();
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(nameof(InventoryItemInfoModal), selectedItems.LastOrDefault());
        }

        public override UniTask WillEnter(Memory<object> args)
        {
            // clear selected items
            var inputInventoryItemList = args.ToArray().FirstOrDefault() as List<InventoryItemData>;
            Debug.Log("inventoryItemList: " + inputInventoryItemList?.Count);

            if (inputInventoryItemList == null)
            {
                return UniTask.CompletedTask;
            }

            EquipmentViewModel.IgnoreIdList.Clear();
            EquipmentViewModel.IgnoreIdList.AddRange(inputInventoryItemList.Select(v => (v.Type + v.Id)));
            EquipmentViewModel.DoFilter(true);
            return UniTask.CompletedTask;
        }
    }
}