using System.Collections.Generic;
namespace Online.Model.ResponseAPI.Ranking
{
	public class RankTicketResponse : BaseResponse
	{
		public string TicketId { get; set; } = "";
		public Dictionary<string, int> VirtualCurrency { get; set; } = new();
	}
}