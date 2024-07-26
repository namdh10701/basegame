using System.Collections.Generic;
using Online.Interface;
using PlayFab.EconomyModels;

namespace Online
{
	public partial class PlayfabManager : IInventoryService
	{
		public List<InventoryItem> Items => Inventory.Items;

		public void LoadInventory(System.Action<bool> cb = null)
		{
			Inventory.LoadInventory(cb);
		}
	}
}