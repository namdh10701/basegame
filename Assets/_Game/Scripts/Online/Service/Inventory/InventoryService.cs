using System.Collections.Generic;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.EconomyModels;
using UnityEngine;

namespace Online.Service.Leaderboard
{
	public class InventoryService : IOnlineService
	{
		public Dictionary<EVirtualCurrency, int> Currencies { get; private set; }
		public List<InventoryItem> Items { get; private set; }

		public IPlayfabManager Manager { get; private set; }

		public void Initialize(IPlayfabManager manager)
		{
			Manager = manager;
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

		public void LoadInventory(System.Action<bool> cb = null)
		{
			PlayFabEconomyAPI.GetInventoryItems(new(), result =>
			{
				Items.Clear();
				foreach (var item in result.Items)
				{
					if (item.Type == "currency")
					{
						
					}
					else if (item.Type == "catalogItem")
					{
						Items.Add(item);
					}
					cb?.Invoke(true);
				}
			}, error =>
			{
				cb?.Invoke(false);
			});
		}
	}
}