namespace Online.Model.ApiRequest
{
    public class RankInfoResponse : BaseResponse
    {
        public RankInfo RankInfo { get; set; }
        public UserRankInfo UserRankInfo { get; set; }
    }
}