using Online.Model;

namespace Online.Interface
{
    public interface IRankingService
	{
		public RankInfo RankInfo { get; }
		public UserRankInfo UserRankInfo { get; }
		public void LoadUserRankInfo();
	}
}