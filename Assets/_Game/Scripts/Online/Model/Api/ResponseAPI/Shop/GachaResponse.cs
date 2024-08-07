using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using PlayFab.ClientModels;

namespace Online.Model.ResponseAPI
{
	public class GachaResponse : BaseResponse
	{
		public List<ItemInstance> Items { get; set; }
		public Dictionary<string, int> VirtualCurrency { get; set; }

		public List<ItemData> GetItems()
		{
			return Items.ToItemData();
		}
	}
}