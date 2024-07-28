using System.Collections.Generic;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IShopService
	{
		public List<StoreItem> GemPackages => _shopService.GemPackages;
		public List<StoreItem> GoldPackages => _shopService.GoldPackages;
		public List<StoreItem> EnergyPackages => _shopService.EnergyPackages;

		public void LoadShop()
		{
			_shopService.LoadAllStore();
		}
	}
}