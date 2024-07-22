using Online.Interface;
namespace Online.Service.Leaderboard
{
	public class LeaderboardService : IOnlineService
	{
		public IPlayfabManager Manager { get; private set; }
		
		public void Initialize(IPlayfabManager manager)
		{
			Manager = manager;
		}
	}
}