using Online.Interface;
using Online.Model;

namespace Online
{
	public partial class PlayfabManager : IRankingService
	{
		#region Ranking Service

		public UserRankInfo UserRankInfo => Ranking.UserRankInfo;
		
		public async void LoadUserRankInfo()
		{
			await Ranking.LoadUserRankInfo();
		}

		public async void LoadRewardBundleInfo()
		{
			await Ranking.LoadRewardBundleInfo();
		}
		
		#endregion
	}
}