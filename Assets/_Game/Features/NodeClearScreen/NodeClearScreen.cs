using System;
using System.Collections.Generic;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.NodeClear
{
    [Binding]
    public class NodeClearScreen : ZBase.UnityScreenNavigator.Core.Screens.Screen
    {
        public List<Toggle> Position = new List<Toggle>();
        private int _lenght = 36;
        InventoryItem inventoryItemDrop;

        #region Binding: ItemsDrop
        private ObservableList<ItemDrop> itemsDrop = new ObservableList<ItemDrop>();

        [Binding]
        public ObservableList<ItemDrop> ItemsDrop => itemsDrop;
        #endregion

        public override async UniTask Initialize(Memory<object> args)
        {
            GeneratePosDropItem();
        }

        private void GeneratePosDropItem()
        {
            for (int i = 0; i < _lenght; i++)
            {
                ItemDrop itemDrop = new ItemDrop()
                {
                    IsActiveItem = false
                };
                itemDrop.Setup(this);
                ItemsDrop.Add(itemDrop);
            }
        }

        public void SetDataDropItemInventory(List<InventoryItem> inventoryItems)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                var index = UnityEngine.Random.Range(0, _lenght);
                ItemsDrop[index].InventoryItem = inventoryItems[i];
            }
        }



        public void SetDropItemInventory(InventoryItem inventoryItem)
        {
            inventoryItemDrop = inventoryItem;
        }
    }
}
