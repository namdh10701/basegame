using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PlayFab.ClientModels;

namespace Online.Interface
{
    public interface IShopService
	{
		public List<StoreItem> GemPackages { get; }
		public List<StoreItem> GoldPackages { get; }
		public List<StoreItem> EnergyPackages { get; }
		
		[Obsolete("Use LoadShopAsync instead")]
		public void LoadShop();
		
		public Task LoadShopAsync();
		
		public bool TryGetLocalizePrice(string packageId, out string priceString);

		public UniTask<bool> BuyStoreItem(string storeId);
	}
}