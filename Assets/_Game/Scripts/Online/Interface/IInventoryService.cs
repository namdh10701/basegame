using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Interface
{
	public interface IInventoryService
	{
		public List<ItemInstance> Items { get; }

		public void RequestInventory(System.Action<bool> cb = null);
	}
}