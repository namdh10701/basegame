using System.Collections.Generic;
using PlayFab.ClientModels;
namespace Online.Model.RequestAPI.Ranking
{
	public class FinishRankBattleRequest : BaseRequest
	{
		public int Damage { get; set; } = 0;
		public string TicketId { get; set; } = "";
	}
}