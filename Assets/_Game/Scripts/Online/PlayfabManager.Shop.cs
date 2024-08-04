using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IShopService
	{
		public List<StoreItem> GemPackages => _shopService.GemPackages;
		public List<StoreItem> GoldPackages => _shopService.GoldPackages;
		public List<StoreItem> EnergyPackages => _shopService.EnergyPackages;

		public async void LoadShop()
		{
			await _shopService.LoadAllStore();
		}

		public async Task LoadShopAsync()
		{
			await _shopService.LoadAllStore();
		}

		public bool TryGetLocalizePrice(string packageId, out string priceString)
		{
			return _shopService.PackageLocalizePrices.TryGetValue(packageId, out priceString);
		}

		public async UniTask<bool> BuyStoreItem(string storeId)
		{
			var result = await _shopService.BuyStoreItem(storeId);
			await RequestInventoryAsync();
			return result;
		}
	}
}