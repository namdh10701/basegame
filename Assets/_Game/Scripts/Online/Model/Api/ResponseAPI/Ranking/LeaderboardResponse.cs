using System.Collections.Generic;

namespace Online.Model.ResponseAPI.Ranking
{
	public class LeaderboardResponse : BaseResponse
	{
		public int Count { get; set; } = 0;
		public List<PlayerRankInfo> Players { get; set; } = new();
	}
}