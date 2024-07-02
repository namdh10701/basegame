using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.UI;
using _Base.Scripts.UI.Managers;
using _Game.Features.Equipment;
using _Game.Features.Home;
using _Game.Features.Inventory;
using _Game.Features.InventoryItemInfo;
using _Game.Scripts.GD;
using _Game.Scripts.UI;
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
            await MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet);
        }

        [Binding]
        public async void NavBackWithParams()
        {
            var selectedItems = EquipmentViewModel.Items.Where(v => v.IsSelected).ToList();
            var output = new EquipmentSheetOutputData(selectedItems);

            await MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet, output);
        }

        [Binding]
        public async void ShowInfo()
        {
            var selectedItems = EquipmentViewModel.Items.Where(v => v.IsSelected).ToList();
            await ModalContainer.Find(ContainerKey.Modals).PushAsync(nameof(InventoryItemInfoModal), selectedItems.FirstOrDefault());
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
            EquipmentViewModel.IgnoreIdList.AddRange(inputInventoryItemList.Select(v => v.Id));
            return UniTask.CompletedTask;
        }
    }
}