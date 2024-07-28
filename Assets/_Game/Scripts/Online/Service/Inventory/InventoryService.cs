using System;
using System.Collections.Generic;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.SaveLoad;
using Online.Enum;
using Online.Interface;
using Online.Model.ApiRequest;
using PlayFab;
using PlayFab.ClientModels;
namespace Online.Service.Leaderboard
{
	public class InventoryService : BaseOnlineService
	{
		public Dictionary<EVirtualCurrency, int> Currencies { get; private set; }
		public List<ItemData> Items { get; private set; }

		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);

			Items = new();
			Currencies = new Dictionary<EVirtualCurrency, int>()
			{
				{
					EVirtualCurrency.Coin, 0
				},
				{
					EVirtualCurrency.Gem, 0
				},
				{
					EVirtualCurrency.Energy, 0
				}
			};
		}

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

		public void LoadVirtualCurrency(Dictionary<string, int> virtualCurrency)
		{
			foreach (EVirtualCurrency currency in System.Enum.GetValues(typeof(EVirtualCurrency)))
			{
				if (virtualCurrency.TryGetValue(currency.GetCode(), out int value))
				{
					Currencies[currency] = value;
				}
			}
		}

		public void LoadItems(List<ItemInstance> items)
		{
			Items.Clear();
			foreach (var itemData in items)
			{
				string[] idParts = itemData.ItemId.Split('_');
				var itemType = idParts[0].GetItemType();
				var itemId = idParts[1];

				int level = 1;
				if (itemData.CustomData != null && itemData.CustomData.TryGetValue(C.NameConfigs.Level, out var levelData))
				{
					level = Convert.ToInt32(levelData);
				}

				int rarityLevel = 0;
				switch (itemType)
				{
					case ItemType.CANNON:
						rarityLevel = GameData.CannonTable.FindById(itemId)?.RarityLevel ?? 0;
						break;

					case ItemType.AMMO:
						rarityLevel = GameData.AmmoTable.FindById(itemId)?.RarityLevel ?? 0;
						break;
				}

				Items.Add(new ItemData()
				{
					ItemType = itemType,
					ItemId = itemId,
					OwnItemId = itemData.ItemInstanceId,
					Level = level,
					RarityLevel = rarityLevel
				});
			}
		}

		public void UpgradeItem(string instanceId, System.Action<object> succeed = null, System.Action<EErrorCode> failed = null)
		{
			var itemData = Items.Find(val => val.OwnItemId == instanceId);
			if (itemData != null)
			{
				PlayFabClientAPI.ExecuteCloudScript(new()
				{
					FunctionName = C.CloudFunction.UpgradeItem,
					FunctionParameter = new RequestUpgradeItemModel()
					{
						ItemInstanceId = instanceId
					}
				}, result =>
				{
					LogSuccess("Upgraded Item!");
					succeed?.Invoke(result.FunctionResult);
				}, error =>
				{
					LogError(error.ErrorMessage);
					failed?.Invoke(EErrorCode.PlayfabError);
				});
			}
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