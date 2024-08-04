using System.Collections.Generic;
using Online.Interface;
using Online.Model;

namespace Online
{
	public partial class PlayfabManager : IRankingService
	{
		#region Ranking Service

		public RankInfo RankInfo => Ranking.RankInfo;
		public UserRankInfo UserRankInfo => Ranking.UserRankInfo;
		
		public async void LoadUserRankInfo()
		{
			await Ranking.RequestUserRankAsync();
		}

		public async void LoadRewardBundleInfo()
		{
			await Ranking.LoadRewardBundleInfo();
		}
		
		#endregion
	}
}