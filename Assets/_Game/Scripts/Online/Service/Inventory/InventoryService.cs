using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Online.Enum;
using Online.Interface;
using Online.Model.RequestAPI.Inventory;
using Online.Model.ResponseAPI.Inventory;
using PlayFab;
using PlayFab.ClientModels;

namespace Online.Service
{
	public class InventoryService : BaseOnlineService
	{
		/// <summary>
		/// Trigger when a currency amount has changed
		/// </summary>
		public event Action<EVirtualCurrency, int> OnCurrencyChanged;

		public Dictionary<EVirtualCurrency, int> Currencies { get; private set; }
		public List<ItemData> Items { get; private set; }

		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);

			Items = new();
			Currencies = new Dictionary<EVirtualCurrency, int>()
			{
				{
					EVirtualCurrency.Gold, 0
				},
				{
					EVirtualCurrency.Gem, 0
				},
				{
					EVirtualCurrency.Energy, 0
				},
				{
					EVirtualCurrency.Ticket, 0
				},
				{
					EVirtualCurrency.Diamond, 0
				},
				{
					EVirtualCurrency.RealMoney, 0
				},
				{
					EVirtualCurrency.Key, 0
				},
				{
					EVirtualCurrency.FreeTicket, 0
				},
			};
		}

		[Obsolete("Use RequestInventoryAsync instead")]
		public void RequestInventory(System.Action<bool> cb = null)
		{
			PlayFabClientAPI.GetUserInventory(new(), result =>
			{
				LoadVirtualCurrency(result.VirtualCurrency);
				LoadItems(result.Inventory);
				cb?.Invoke(true);
			}, error =>
			{
				LogError(error.ErrorMessage);
				cb?.Invoke(false);
			});
		}

		public async Task<bool> RequestInventoryAsync()
		{
			var resp = await PlayFabAsync.PlayFabClientAPI.GetUserInventoryAsync(new());
			if (resp.IsError)
			{
				LogError(resp.Error.ErrorMessage);
				return false;
			}

			LoadVirtualCurrency(resp.Result.VirtualCurrency);
			LoadItems(resp.Result.Inventory);
			return true;
		}

		public void LoadVirtualCurrency(Dictionary<string, int> virtualCurrency)
		{
			if (virtualCurrency == null) return;
			foreach (EVirtualCurrency currency in System.Enum.GetValues(typeof(EVirtualCurrency)))
			{
				if (virtualCurrency.TryGetValue(currency.GetCode(), out int value))
				{
					Currencies[currency] = value;
					OnCurrencyChanged?.Invoke(currency, value);
				}
			}
		}

		public void LoadItems(List<ItemInstance> items)
		{
			Items = items.ToItemData();
		}

		public void RevokeBlueprints(List<string> revokeBlueprints)
		{
			if (revokeBlueprints == null) return;
			Items.RemoveAll(val => revokeBlueprints.Contains(val.OwnItemId));
		}

		public void RefundBlueprints(List<ItemInstance> refundBlueprints)
		{
			// if (revokeBlueprints == null) return;
			// Items.RemoveAll(val => revokeBlueprints.Contains(val.OwnItemId));
		}

		public async UniTask<UpgradeItemResponse> UpgradeItem(string instanceId)
		{
			UniTaskCompletionSource<UpgradeItemResponse> signal = new UniTaskCompletionSource<UpgradeItemResponse>();
			var itemData = Items.Find(val => val.OwnItemId == instanceId);
			if (itemData != null)
			{
				PlayFabClientAPI.ExecuteCloudScript(new()
				{
					FunctionName = C.CloudFunction.UpgradeItem,
					FunctionParameter = new UpgradeItemRequest()
					{
						ItemInstanceId = instanceId
					}
				}, result =>
				{
					LogSuccess("Upgraded Item!");
					signal.TrySetResult(JsonConvert.DeserializeObject<UpgradeItemResponse>(result.FunctionResult.ToString()));
				}, error =>
				{
					LogError(error.ErrorMessage);
					signal.TrySetResult(new UpgradeItemResponse()
					{
						Result = false,
						Error = EErrorCode.PlayfabError
					});
				});
			}
			return await signal.Task;
		}

		public UniTask<CombineItemsResponse> CombineItems(List<string> itemInstanceIds)
		{
			UniTaskCompletionSource<CombineItemsResponse> signal = new UniTaskCompletionSource<CombineItemsResponse>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.CombineItems,
				FunctionParameter = new CombineItemsRequest()
				{
					ItemInstanceIds = itemInstanceIds
				}
			}, result =>
			{
				LogSuccess("Combine Item!");
				signal.TrySetResult(JsonConvert.DeserializeObject<CombineItemsResponse>(result.FunctionResult.ToString()));
			}, error =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new()
				{
					Result = false,
					Error = EErrorCode.PlayfabError
				});
			});
			return signal.Task;
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Inventory");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Inventory");
		}
	}
}