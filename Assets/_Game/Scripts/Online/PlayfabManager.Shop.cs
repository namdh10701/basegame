using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model.ApiRequest;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IShopService
	{
		public List<StoreItem> GemPackages => _shopService.GemPackages;
		public List<StoreItem> GoldPackages => _shopService.GoldPackages;
		public List<StoreItem> EnergyPackages => _shopService.EnergyPackages;

		public UniTask<bool> LoadShopAsync()
		{
			return _shopService.LoadAllStore();
		}
		
		public async UniTask<GachaResponse> GachaAsync(string gachaId)
		{
			return await _shopService.GachaAsync(gachaId);
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