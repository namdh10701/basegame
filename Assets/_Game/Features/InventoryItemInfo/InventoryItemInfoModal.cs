using System;
using System.Linq;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;

namespace _Game.Features.InventoryItemInfo
{
    public class InventoryItemInfoModal : ModalWithViewModel
    {
        [Binding]
        public InventoryItem InventoryItem { get; set; }
        
        public override UniTask WillPushEnter(Memory<object> args)
        {
            var receivedObj = args.ToArray().FirstOrDefault();
            if (receivedObj is not InventoryItem item)
            {
                return UniTask.CompletedTask;
            }

            InventoryItem = item;
            //
            // if (item.Type == ItemType.CANNON)
            // {
            //     var cannon = GDConfigLoader.Instance.Cannons[item.Id];
            // } 
            // else if (item.Type == ItemType.AMMO)
            // {
            //     var ammo = GDConfigLoader.Instance.Ammos[item.Id];
            // }
            // else if (item.Type == ItemType.CREW)
            // {
            //     var crew = GDConfigLoader.Instance.Ammos[item.Id];
            // }
            
            return UniTask.CompletedTask;
        }
    }
}