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

		public Dictionary<EVirtualCurrency, int> Currencies { get; private set; } = new();
		public Dictionary<string, ItemData> ItemMaps { get; private set; } = new();

		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);
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

		public void UpdateItemData(ItemInstance newItem)
		{
			var newItemData = newItem.GetItemData();
			if (ItemMaps.TryGetValue(newItem.ItemInstanceId, out var itemData))
			{
				itemData.Level = newItemData.Level;
				itemData.RarityLevel = newItemData.RarityLevel;
				itemData.Rarity = newItemData.Rarity;
			}
			else
			{
				ItemMaps.Add(newItem.ItemInstanceId, newItem.GetItemData());
			}
		}

		public void LoadItems(List<ItemInstance> items)
		{
			ItemMaps.Clear();
			foreach (var item in items)
			{
				ItemMaps.Add(item.ItemInstanceId, item.GetItemData());
			}
		}

		public void AddItems(List<ItemInstance> newItems)
		{
			foreach (var item in newItems)
			{
				ItemMaps.Add(item.ItemInstanceId, item.GetItemData());
			}
		}

		public void RemoveItems(List<string> removeItems)
		{
			if (removeItems == null) return;
			foreach (var item in removeItems)
			{
				ItemMaps.Remove(item);
			}
		}

		public void RefundBlueprints(List<ItemInstance> refundBlueprints)
		{
			foreach (var blueprint in refundBlueprints)
			{
				if (!ItemMaps.TryGetValue(blueprint.ItemInstanceId, out var _))
				{
					ItemMaps.Add(blueprint.ItemInstanceId, blueprint.GetItemData());
				}
			}
		}

		/// <summary>
		/// Enhance level of an item
		/// </summary>
		/// <param name="instanceId">User instance item id </param>
		/// <returns>
		/// Result: true if succeed, false if failed
		/// Error: Error code if failed, such as: ItemNotMatch, NotEnoughGold, NotEnoughBlueprint, PlayfabError
		/// ItemUpgrade: Item data after upgrade
		/// VirtualCurrency: Virtual Currency amount changed
		/// RevokeBlueprintIDs: List of blueprint id that need to be revoked
		/// </returns>
		public async UniTask<EnhanceItemResponse> EnhanceItem(string instanceId)
		{
			var signal = new UniTaskCompletionSource<EnhanceItemResponse>();
			if (ItemMaps.TryGetValue(instanceId, out var itemData))
			{
				PlayFabClientAPI.ExecuteCloudScript(new()
				{
					FunctionName = C.CloudFunction.EnhanceItem,
					FunctionParameter = new EnhanceItemRequest()
					{
						ItemInstanceId = instanceId
					}
				}, result =>
				{
					LogSuccess("UpgradeItem, Succeed!");
					signal.TrySetResult(JsonConvert.DeserializeObject<EnhanceItemResponse>(result.FunctionResult.ToString()));
				}, error =>
				{
					LogError("UpgradeItem, Error: " + error.ErrorMessage);
					signal.TrySetResult(new EnhanceItemResponse()
					{
						Result = false,
						Error = EErrorCode.PlayfabError
					});
				});
			}
			return await signal.Task;
		}

		public UniTask<CombineItemRespons> CombineItems(List<string> itemInstanceIds)
		{
			UniTaskCompletionSource<CombineItemRespons> signal = new UniTaskCompletionSource<CombineItemRespons>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.CombineItems,
				FunctionParameter = new CombineItemRequest()
				{
					ItemInstanceIds = itemInstanceIds
				}
			}, result =>
			{
				LogSuccess("Combine Item!");
				signal.TrySetResult(JsonConvert.DeserializeObject<CombineItemRespons>(result.FunctionResult.ToString()));
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