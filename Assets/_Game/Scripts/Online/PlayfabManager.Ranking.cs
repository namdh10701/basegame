using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model;

namespace Online
{
	public partial class PlayfabManager : IRankingService
	{
		#region Ranking Service

		public SeasonInfo SeasonInfo => Ranking.SeasonInfo;
		public RankInfo RankInfo => Ranking.RankInfo;
		
		public async Task LoadUserRankInfoAsync()
		{
			await Ranking.RequestUserRankAsync();
		}

		public async Task LoadRewardBundleInfo()
		{
			await Ranking.LoadRewardBundleInfo();
		}
		
		#endregion
	}
}