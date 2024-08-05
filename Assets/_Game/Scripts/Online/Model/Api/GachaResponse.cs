using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using PlayFab.ClientModels;
namespace Online.Model.ApiRequest
{
	public class GachaResponse : BaseResponse
	{
		public ItemInstance[] Items { get; set; }
		public Dictionary<string, int> VirtualCurrency { get; set; }
		
		public ItemData[] GetItemDatas()
		{
			return Items.ParseToItemDatas();
		}
	}
}