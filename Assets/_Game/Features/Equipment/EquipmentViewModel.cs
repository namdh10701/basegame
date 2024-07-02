using System;
using System.Linq;
using _Game.Features.Inventory;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.Equipment
{
    [Binding]
    public class EquipmentViewModel : InventoryViewModel
    {
        [Binding]
        public async void NavBack()
        {
            var items = Items.Where(v => v.IsSelected).ToList();
            var args = new object[] { items }.AsMemory();
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true, args);
            
            // new SheetContainer().show
        }
        
        [Binding]
        public async void ShowInfo()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }
        
        [Binding]
        public async void Equip()
        {
            var items = Items.Where(v => v.IsSelected).ToList();
            var args = new object[] { items }.AsMemory();
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true, args);
        }
    }
}