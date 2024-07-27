using System.Collections.Generic;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
namespace Online.Service.Leaderboard
{
	public class InventoryService : BaseOnlineService
	{
		public Dictionary<EVirtualCurrency, int> Currencies { get; private set; }
		public List<ItemInstance> Items { get; private set; }

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
			Items = items;
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