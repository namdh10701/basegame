using System.Collections.Generic;
using PlayFab.EconomyModels;

namespace Online.Interface
{
	public interface IInventoryService
	{
		public List<InventoryItem> Items { get; }

		public void LoadInventory(System.Action<bool> cb = null);
	}
}