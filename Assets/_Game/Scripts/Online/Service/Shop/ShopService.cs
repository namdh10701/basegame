using System.Collections;
using System.Collections.Generic;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Purchasing;

namespace Online.Service
{
	public class ShopService : BaseOnlineService, IStoreListener
	{
		#region Shop ID

		private const string GEM_PACKAGES_ID = "GemPackages";
		private const string GOLD_PACKAGES_ID = "GoldPackages";
		private const string ENERGY_PACKAGES_ID = "EnergyPackages";

		#endregion

		#region Properties

		public List<StoreItem> GemPackages { get; private set; }
		public List<StoreItem> GoldPackages { get; private set; }
		public List<StoreItem> EnergyPackages { get; private set; }

		#endregion

		#region IAP

		private IStoreController controller = null;
		private IExtensionProvider extensions = null;

		#endregion

		public void LoadAllStore()
		{
			LoadGemPackages();
			LoadGoldPackages();
			LoadEnergyPackages();

			Manager.RunCoroutine(InitIAP());
		}
		public IEnumerator InitIAP()
		{
			while (GemPackages == null)
			{
				yield return null;
			}

			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			UnityPurchasing.Initialize(this, builder);
		}

		public void LoadGemPackages(System.Action<bool> cb = null)
		{
			LoadStore(GEM_PACKAGES_ID, result =>
			{
				GemPackages = result;
				cb?.Invoke(GemPackages != null);
			});
		}

		public void LoadGoldPackages(System.Action<bool> cb = null)
		{
			LoadStore(GOLD_PACKAGES_ID, result =>
			{
				GoldPackages = result;
				cb?.Invoke(GoldPackages != null);
			});
		}

		public void LoadEnergyPackages(System.Action<bool> cb = null)
		{
			LoadStore(ENERGY_PACKAGES_ID, result =>
			{
				EnergyPackages = result;
				cb?.Invoke(EnergyPackages != null);
			});
		}

		private void LoadStore(string storeId, System.Action<List<StoreItem>> cb = null)
		{
			PlayFabClientAPI.GetStoreItems(new()
			{
				StoreId = storeId
			}, result =>
			{
				LogSuccess("Loaded store: " + storeId);
				cb?.Invoke(result.Store);
			}, error =>
			{
				LogError(error.ErrorMessage);
				cb?.Invoke(null);
			});
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Shop");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Shop");
		}

		#region Unity IAP

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			throw new System.NotImplementedException();
		}
		public void OnInitializeFailed(InitializationFailureReason error, string message)
		{
			throw new System.NotImplementedException();
		}
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
		{
			throw new System.NotImplementedException();
		}
		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			throw new System.NotImplementedException();
		}
		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}