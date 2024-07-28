using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Interface
{
    public interface IShopService
	{
		public List<StoreItem> GemPackages { get; }
		public List<StoreItem> GoldPackages { get; }
		public List<StoreItem> EnergyPackages { get; }
		
		public void LoadShop();
		
		public bool TryGetLocalizePrice(string packageId, out string priceString);

		public void BuyStoreItem(string storeId, System.Action<bool> cb = null);
	}
}