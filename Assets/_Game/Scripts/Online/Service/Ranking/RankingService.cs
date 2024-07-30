using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model;
using Random = UnityEngine.Random;

namespace Online.Service
{
	public class RankingService : BaseOnlineService
	{
		public UserRankInfo UserRankInfo { get; private set; }
		
		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);
		}
		
		public async UniTask<UserRankInfo> LoadUserRankInfo()
		{
			var signal = new UniTaskCompletionSource<UserRankInfo>();

			//TODO: DNGUYEN - ranking loading
			// dummy data >>>>
			var rankInfo = new UserRankInfo
			{
				SeasonNo = "99",
				SeasonName = "Cyborg Octopus",
				Rank = UserRank.Hunter,
			};
			
			for (int i = 0; i < 50; i++)
			{
				var rec = new RankRecord();
				rec.No = i + 1;
				rec.Username = $"User{rec.No}";
				rec.Score = (int)Random.Range(100f, 10000f);
				rankInfo.Records.Add(rec);
			}
			// <<<< dummy data

			UserRankInfo = rankInfo;
			signal.TrySetResult(rankInfo);
			return await signal.Task;
		}

		public async UniTask<RewardBundleInfo> LoadRewardBundleInfo()
		{
			var signal = new UniTaskCompletionSource<RewardBundleInfo>();
			
			//TODO: DNGUYEN - reward loading
			// dummy data >>>>
			var bundles = new RewardBundleInfo();
			var bundle = new ClaimRewardBundle();
			for (int i = 0; i < 5; i++)
			{
				bundle.Records.Add(new RankReward()
				{
					ItemId = "Cannon_0001",
					Amount = 10
				});
			}
			bundles.Bundles.Add(bundle);
			// <<<< dummy data

			signal.TrySetResult(bundles);
			return await signal.Task;
		}

		public async UniTask<UserRankInfo> StartBattle()
		{
			throw new NotImplementedException();
		}
		
		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "RankingService");
		}
		
		public override void LogError(string error)
		{
			LogEvent(true, error, "RankingService");
		}
	}
}