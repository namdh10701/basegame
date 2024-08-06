using System.Collections.Generic;

namespace Online.Model.RequestAPI.Inventory
{
	public class CombineItemsRequest  : BaseRequest
	{
		public List<string> ItemInstanceIds { get; set; }
	}
}