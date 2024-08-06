using System.Collections.Generic;
using _Game.Features.Ranking;

namespace Online.Model.ResponseAPI.Ranking
{
	public class FinishRankBattleResponse : BaseResponse
	{
		public int Score { get; set; } = 0;
		public List<RankReward> Rewards { get; set; } = new();
	}
}