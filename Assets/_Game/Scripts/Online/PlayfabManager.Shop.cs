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

		public bool TryGetLocalizePrice(string packageId, out string priceString)
		{
			return _shopService.PackageLocalizePrices.TryGetValue(packageId, out priceString);
		}

		public void BuyStoreItem(string storeId, System.Action<bool> cb = null)
		{
			_shopService.BuyStoreItem(storeId, (succeed) =>
			{
				if (succeed)
				{
					_inventoryService.RequestInventory(cb);
				}
				else
				{
					cb?.Invoke(false);
				}
			});
		}
	}
}