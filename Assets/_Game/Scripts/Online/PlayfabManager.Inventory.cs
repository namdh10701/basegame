using System.Collections.Generic;
using Online.Enum;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IInventoryService
	{
		public List<ItemInstance> Items => Inventory.Items;
		public int Coin => Inventory.Currencies[EVirtualCurrency.Coin];
		public int Gem => Inventory.Currencies[EVirtualCurrency.Gem];

		public void RequestInventory(System.Action<bool> cb = null)
		{
			Inventory.RequestInventory(cb);
		}
	}
}