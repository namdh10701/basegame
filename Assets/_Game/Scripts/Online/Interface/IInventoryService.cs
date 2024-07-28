using System.Collections.Generic;
using _Game.Scripts.SaveLoad;

namespace Online.Interface
{
	public interface IInventoryService
	{
		public int Gold { get; }
		public int Gem { get; }
		public int Energy { get; }
		public List<ItemData> Items { get; }

		public void RequestInventory(System.Action<bool> cb = null);
	}
}