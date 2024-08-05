using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;

namespace Online
{
	public partial class PlayfabManager : IInventoryService
	{
		public List<ItemData> Items => Inventory.Items;
		public int Gold => Inventory.Currencies[EVirtualCurrency.Gold];
		public int Gem => Inventory.Currencies[EVirtualCurrency.Gem];
		public int Energy => Inventory.Currencies[EVirtualCurrency.Energy];
		public int Ticket => Inventory.Currencies[EVirtualCurrency.Ticket];
		public int Diamond => Inventory.Currencies[EVirtualCurrency.Diamond];
		public int VipKey => Inventory.Currencies[EVirtualCurrency.VipKey];
		
		public async UniTask<bool> RequestInventoryAsync()
		{
			return await Inventory.RequestInventoryAsync();
		}
	}
}