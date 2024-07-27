using Online.Interface;
namespace Online.Service.Leaderboard
{
	public class LeaderboardService : BaseOnlineService
	{
		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);
		}
		
		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Leaderboard");
		}
		
		public override void LogError(string error)
		{
			LogEvent(true, error, "Leaderboard");
		}
	}
}