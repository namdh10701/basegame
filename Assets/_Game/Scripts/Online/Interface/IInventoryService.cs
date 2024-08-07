using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;

namespace Online.Interface
{
	public interface IInventoryService
	{
		public int Gold { get; }
		public int Gem { get; }
		public int Energy { get; }
		public Dictionary<string, ItemData> ItemMaps { get; }

		public UniTask<bool> RequestInventoryAsync();
	}
}