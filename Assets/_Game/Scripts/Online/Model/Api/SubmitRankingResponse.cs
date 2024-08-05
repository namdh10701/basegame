using System.Collections.Generic;
using _Game.Features.Ranking;

namespace Online.Model.ApiRequest
{
    public class SubmitRankingResponse : BaseResponse
    {
        public UserRankInfo UserRankInfo { get; set; }
        public List<RankReward> Rewards { get; set; }
    }
}