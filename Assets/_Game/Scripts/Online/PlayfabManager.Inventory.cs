using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.SaveLoad;
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

		public void RequestInventory(System.Action<bool> cb = null)
		{
			Inventory.RequestInventory(cb);
		}
		
		public async Task<bool> RequestInventoryAsync()
		{
			return await Inventory.RequestInventoryAsync();
		}
	}
}