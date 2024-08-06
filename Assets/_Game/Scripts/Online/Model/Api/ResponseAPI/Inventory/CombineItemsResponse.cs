using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Model.ResponseAPI.Inventory
{
	public class CombineItemsResponse : BaseResponse
	{
		public ItemInstance Item { get; set; }
		public List<ItemInstance> RefundBlueprints { get; set; }
	}
}