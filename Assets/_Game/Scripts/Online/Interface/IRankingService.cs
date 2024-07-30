using Online.Model;

namespace Online.Interface
{
    public interface IRankingService
	{
		public UserRankInfo UserRankInfo { get; }
		
		public void LoadUserRankInfo();
	}
}