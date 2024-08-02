using System.Collections.Generic;
using PlayFab.ClientModels;
namespace Online.Model.ApiRequest
{
	public class UpgradeItemResponse : BaseResponse
	{
		public ItemInstance ItemUpgrade { get; set; }
		public Dictionary<string, int> VirtualCurrency { get; set; }
		public List<string> RevokeBlueprintIDs { get; set; }
	}
}