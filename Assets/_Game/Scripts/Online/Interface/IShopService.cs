using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Online.Model.ApiRequest;
using PlayFab.ClientModels;

namespace Online.Interface
{
    public interface IShopService
	{
		public List<StoreItem> GemPackages { get; }
		public List<StoreItem> GoldPackages { get; }
		public List<StoreItem> EnergyPackages { get; }

		public UniTask<bool> LoadShopAsync();
		
		public bool TryGetLocalizePrice(string packageId, out string priceString);

		public UniTask<bool> BuyStoreItem(string storeId);

		public UniTask<GachaResponse> GachaAsync(string gachaId);
	}
}