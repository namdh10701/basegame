using System.Collections.Generic;

namespace Online.Model.RequestAPI.Inventory
{
	public class CombineItemRequest  : BaseRequest
	{
		public List<string> ItemInstanceIds { get; set; }
	}
}