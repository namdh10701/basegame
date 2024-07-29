using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Enum;
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

        public List<StoreItem> GemPackages { get; private set; } = new();
        public List<StoreItem> GoldPackages { get; private set; } = new();
        public List<StoreItem> EnergyPackages { get; private set; } = new();
        public Dictionary<string, string> PackageLocalizePrices { get; private set; } = new();

        private readonly Dictionary<string, StoreItem> _storeItems = new();

        #endregion

        #region IAP

        private IStoreController _storeController = null;
        private IExtensionProvider _extensionProvider = null;
        private IGooglePlayStoreExtensions _googlePlayStoreExtensions;
        private IAppleExtensions _appleExtensions;
        private System.Action<bool> _purchaseCallback = null;

        #endregion

        public bool IsInitialized => _storeController != null && _extensionProvider != null;

        public async UniTask LoadAllStore()
        {
            await UniTask.WhenAll(LoadGemPackages(), LoadGoldPackages(), LoadEnergyPackages());

            InitIAP();
        }

        void InitIAP()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (var gem in GemPackages)
            {
                builder.AddProduct(gem.ItemId, ProductType.Consumable);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public async UniTask<List<StoreItem>> LoadGemPackages()
        {
            var storeResult = await LoadStore(GEM_PACKAGES_ID);
            GemPackages = storeResult?.Store ?? null;
            return GemPackages;
        }

        public async UniTask<List<StoreItem>> LoadGoldPackages()
        {
            var storeResult = await LoadStore(GOLD_PACKAGES_ID);
            GoldPackages = storeResult?.Store ?? null;
            return GoldPackages;
        }

        public async UniTask<List<StoreItem>> LoadEnergyPackages()
        {
            var storeResult = await LoadStore(ENERGY_PACKAGES_ID);
            EnergyPackages = storeResult?.Store ?? null;
            return EnergyPackages;
        }

        private async UniTask<GetStoreItemsResult> LoadStore(string storeId)
        {
            UniTaskCompletionSource<GetStoreItemsResult> signal = new UniTaskCompletionSource<GetStoreItemsResult>();
            PlayFabClientAPI.GetStoreItems(new()
            {
                StoreId = storeId
            }, result =>
            {
                foreach (var storeItem in result.Store)
                {
                    if (_storeItems.TryGetValue(storeItem.ItemId, out var item))
                    {
                        _storeItems[storeItem.ItemId] = storeItem;
                    }
                    else
                    {
                        _storeItems.Add(storeItem.ItemId, storeItem);
                    }
                }

                LogSuccess("Loaded store: " + storeId);
                signal.TrySetResult(result);
            }, error =>
            {
                LogError(error.ErrorMessage);
                signal.TrySetResult(null);
            });
            return await signal.Task;
        }

        public async UniTask<bool> BuyStoreItem(string storeId)
        {
            if (_storeItems.TryGetValue(storeId, out var storeItem))
            {
                if (storeItem.VirtualCurrencyPrices.TryGetValue(EVirtualCurrency.Gem.GetCode(), out var gemPrice))
                {
                    return await BuyByGem(storeItem);
                }
                else if (storeItem.VirtualCurrencyPrices.TryGetValue(EVirtualCurrency.Gold.GetCode(), out var goldPrice))
                {
                    return await BuyByGold(storeItem);
                }
                else
                {
                    return await BuyInAppItem(storeId);
                }
            }
            else
            {
                return false;
            }
        }

        public async UniTask<bool> BuyInAppItem(string storeId)
        {
            UniTaskCompletionSource<bool> signal = new UniTaskCompletionSource<bool>();
            if (!IsInitialized)
            {
                LogError("UnityIAP not initialized!");
                signal.TrySetResult(false);
            }
            else
            {
                Product product = _storeController.products.WithID(storeId);
                if (product != null)
                {
                    _storeController.InitiatePurchase(product);
                    _purchaseCallback = (result) => signal.TrySetResult(result);
                }
                else
                {
                    LogError($"Store Item {storeId} not initialized!");
                    signal.TrySetResult(false);
                }
            }

            return await signal.Task;
        }

        public async UniTask<bool> BuyByGem(StoreItem storeItem, System.Action<bool> cb = null)
        {
            var vcCode = EVirtualCurrency.Gem.GetCode();
            string storeId = GoldPackages.Contains(storeItem) ? GOLD_PACKAGES_ID : ENERGY_PACKAGES_ID;

            UniTaskCompletionSource<bool> signal = new UniTaskCompletionSource<bool>();
            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
            {
                ItemId = storeItem.ItemId,
                StoreId = storeId,
                VirtualCurrency = vcCode,
                Price = (int)storeItem.VirtualCurrencyPrices[vcCode]
            }, result =>
            {
                LogSuccess($"Bought {result.Items.Count} items" + storeItem.ItemId);
                signal.TrySetResult(true);
            }, error =>
            {
                LogError(error.ErrorMessage);
                signal.TrySetResult(false);
            });
            return await signal.Task;
        }

        public async UniTask<bool> BuyByGold(StoreItem storeItem, System.Action<bool> cb = null)
        {
            var vcCode = EVirtualCurrency.Gold.GetCode();
            string storeId = GoldPackages.Contains(storeItem) ? GOLD_PACKAGES_ID : ENERGY_PACKAGES_ID;

            UniTaskCompletionSource<bool> signal = new UniTaskCompletionSource<bool>();
            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest()
            {
                ItemId = storeItem.ItemId,
                StoreId = storeId,
                VirtualCurrency = vcCode,
                Price = (int)storeItem.VirtualCurrencyPrices[vcCode]
            }, result =>
            {
                LogSuccess($"Bought {result.Items.Count} items" + storeItem.ItemId);
                signal.TrySetResult(true);
            }, error =>
            {
                LogError(error.ErrorMessage);
                signal.TrySetResult(false);
            });
            return await signal.Task;
        }

        public override void LogSuccess(string message)
        {
            LogEvent(false, message, "Shop");
        }

        public override void LogError(string error)
        {
            LogEvent(true, error, "Shop");
        }

        public void ValidateGooglePlayPurchase(string currencyCode, uint price, PayloadData receiptJson, System.Action<bool> cb = null)
        {
            PlayFabClientAPI.ValidateGooglePlayPurchase(new ValidateGooglePlayPurchaseRequest()
            {
                CurrencyCode = currencyCode,
                PurchasePrice = price,
                ReceiptJson = receiptJson.json,
                Signature = receiptJson.signature
            }, result =>
            {
                LogSuccess("Validate Purchase!");
                cb?.Invoke(true);
            }, error =>
            {
                LogError(error.ErrorMessage);
                cb?.Invoke(false);
            });
        }

        public void ValidateApplePurchase(string receiptData, System.Action<bool> cb = null)
        {
            PlayFabClientAPI.ValidateIOSReceipt(new ValidateIOSReceiptRequest()
            {
                ReceiptData = receiptData
            }, result =>
            {
                LogSuccess("Validate Purchase!");
                cb?.Invoke(true);
            }, error =>
            {
                LogError(error.ErrorMessage);
                cb?.Invoke(false);
            });
        }

        #region Unity IAP

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            LogSuccess("UnitIAP, Initialized!");
            _storeController = controller;
            _extensionProvider = extensions;

            PackageLocalizePrices.Clear();
            foreach (var product in _storeController.products.all)
            {
                PackageLocalizePrices.Add(product.definition.id, product.metadata.localizedPriceString);
            }
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
            var product = purchaseEvent.purchasedProduct;
            var googleReceipt = GooglePurchase.FromJson(purchaseEvent.purchasedProduct.receipt);
            ValidateGooglePlayPurchase(product.metadata.isoCurrencyCode, (uint)product.metadata.localizedPrice * 100, googleReceipt.PayloadData, _purchaseCallback);
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