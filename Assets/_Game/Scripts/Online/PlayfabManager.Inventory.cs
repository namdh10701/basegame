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
		public Dictionary<string, ItemData> ItemMaps => Inventory.ItemMaps;
		public int Gold => Inventory.Currencies[EVirtualCurrency.Gold];
		public int Gem => Inventory.Currencies[EVirtualCurrency.Gem];
		public int Energy => Inventory.Currencies[EVirtualCurrency.Energy];
		public int Ticket => Inventory.Currencies[EVirtualCurrency.Ticket];
		public int Diamond => Inventory.Currencies[EVirtualCurrency.Diamond];
		public int Key => Inventory.Currencies[EVirtualCurrency.Key];
		public int FreeTicket => Inventory.Currencies[EVirtualCurrency.FreeTicket];
		
		public async UniTask<bool> RequestInventoryAsync()
		{
			return await Inventory.RequestInventoryAsync();
		}
	}
}