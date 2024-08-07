using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using PlayFab.ClientModels;
namespace Online.Model.ResponseAPI.Inventory
{
	public class EnhanceItemResponse : BaseResponse
	{
		public ItemInstance ItemUpgrade { get; set; }
		public Dictionary<string, int> VirtualCurrency { get; set; }
		public List<string> RevokeBlueprintIDs { get; set; }
	}
}