using System.Collections;
using System.Collections.Generic;
using Online.Interface;
using Online.Model.GooglePurchase;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace Online.Service
{
	public class ShopService : BaseOnlineService, IDetailedStoreListener
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

		private IStoreController _storeController = null;
		private IExtensionProvider _extensionProvider = null;
		private IGooglePlayStoreExtensions _googlePlayStoreExtensions;
		private IAppleExtensions _appleExtensions;
		private Dictionary<string, string> _packageLocalPrices = new Dictionary<string, string>();

		#endregion

		public bool IsInitialized => _storeController != null && _extensionProvider != null;

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
			foreach (var gem in GemPackages)
			{
				builder.AddProduct(gem.ItemId, ProductType.Consumable);
			}
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

		public void BuyStoreItem(string storeId, System.Action<bool> cb = null)
		{
			if (!IsInitialized)
			{
				LogError("UnityIAP not initialized!");
				cb?.Invoke(false);
				return;
			}

			var storeItem = GemPackages.Find(val => val.ItemId == storeId);
			if (storeItem != null)
			{

			}
			else
			{
				LogError($"Store Item {storeId} not initialized!");
				cb?.Invoke(false);
			}
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

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			LogSuccess("UnitIAP, Initialized!");
			_storeController = controller;
			_extensionProvider = extensions;

			_packageLocalPrices.Clear();
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			LogError("UnitIAP, Init failed: " + error);
		}

		public void OnInitializeFailed(InitializationFailureReason error, string message)
		{
			LogError("UnitIAP, Init failed: " + error + ", " + message);
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
		{
			if (!IsInitialized)
				LogError("ProcessPurchase, UnityIAP not initialized!");

			if (purchaseEvent.purchasedProduct == null)
			{
				LogError("ProcessPurchase, Unknow purchase product!");
				return PurchaseProcessingResult.Complete;
			}

#if UNITY_ANDROID
			var googleReceipt = GooglePurchase.FromJson(purchaseEvent.purchasedProduct.receipt);
#else
#endif

			return PurchaseProcessingResult.Complete;
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			LogError("OnPurchaseFailed, ProductId: " + product.definition.id + ", Reason: " + failureReason.ToString());
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
		{
			LogError("OnPurchaseFailed, ProductId: " + product.definition.id + ", Reason: " + failureDescription.reason + ", Message: " + failureDescription.message);
		}

		#endregion
	}
}