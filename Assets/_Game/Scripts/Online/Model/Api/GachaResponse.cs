using System.Collections.Generic;
using PlayFab.ClientModels;
namespace Online.Model.ApiRequest
{
	public class GachaResponse : BaseResponse
	{
		public ItemInstance[] Items { get; set; }
		public Dictionary<string, int> VirtualCurrency { get; set; }
	}
}