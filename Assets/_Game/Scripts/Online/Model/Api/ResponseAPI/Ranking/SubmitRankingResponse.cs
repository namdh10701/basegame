using System.Collections.Generic;
using _Game.Features.Ranking;

namespace Online.Model.ResponseAPI.Ranking
{
    public class SubmitRankingResponse : BaseResponse
    {
        public RankInfo RankInfo { get; set; }
        public List<RankReward> Rewards { get; set; }
    }
}