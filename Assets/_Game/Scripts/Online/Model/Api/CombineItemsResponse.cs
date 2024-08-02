using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Model.ApiRequest
{
	public class CombineItemsResponse : BaseResponse
	{
		public ItemInstance Item { get; set; }
		public List<ItemInstance> RefundBlueprints { get; set; }
	}
}