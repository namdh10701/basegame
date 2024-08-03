using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model;
using PlayFab;
using Random = UnityEngine.Random;

namespace Online.Service
{
	public class RankingService : BaseOnlineService
	{
		public UserRankInfo UserRankInfo { get; private set; }
		public RewardBundleInfo RewardBundleInfo { get; private set; }
		
		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);
		}
		
		public async UniTask<UserRankInfo> LoadUserRankInfo()
		{
			var signal = new UniTaskCompletionSource<UserRankInfo>();
			PlayFabClientAPI.GetUserReadOnlyData(new ()
			{
				
			}, result =>
			{
				//TODO: DNGUYEN - xoá dummy data dưới đây, map dữ liệu từ result
				// dummy data >>>>>
				var rankInfo = new UserRankInfo
				{
					SeasonNo = "99",
					SeasonName = "Cyborg Octopus XXX",
					SeasonExpiredAt = DateTime.Now.AddMinutes(300),
					Rank = UserRank.Hunter,
				};
				
				for (int i = 0; i < 50; i++)
				{
					var rec = new RankRecord();
					rec.No = i + 1;
					rec.Username = $"User{rec.No}";
					rec.Score = (int)Random.Range(100f, 10000f);

					if (i == 14)
					{
						rec.PlayfabID = "1D038512E2F253AA";
					}

					rec.Rewards.Add(new RankReward()
					{
						ItemId="res_gold",
						Amount = 50,
						ItemType = ItemType.MISC
					});
					rec.Rewards.Add(new RankReward()
					{
						ItemId="res_gem",
						Amount = 2,
						ItemType = ItemType.MISC
					});
					rec.Rewards.Add(new RankReward()
					{
						ItemId="res_blueprint_cannon",
						Amount = 1,
						ItemType = ItemType.MISC
					});
					rankInfo.Records.Add(rec);
				}
				// <<<<< dummy data

				UserRankInfo = rankInfo;
				signal.TrySetResult(rankInfo);
			}, error =>
			{
				signal.TrySetException(new Exception(error.ErrorMessage));
			});
			return await signal.Task;
		}

		public async UniTask<RewardBundleInfo> LoadRewardBundleInfo()
		{
			var signal = new UniTaskCompletionSource<RewardBundleInfo>();
			
			//TODO: DNGUYEN - load danh sách reward, xoá dummy data dưới đây
			// dummy data >>>>
			var records = System.Enum.GetValues(typeof(UserRank)).Cast<UserRank>()
				.Reverse()
				.Select(v => new Online.Model.ClaimRewardBundle
				{
					Rank = v,
					IsCurrentRank = v == UserRank.Captain,
					IsClaimed = false,
					Rewards = new List<RankReward>
					{
						new()
						{
							ItemType = ItemType.CANNON,
							ItemId = "0012",
							Amount = 3
						},
						new()
						{
							ItemType = ItemType.MISC,
							ItemId = MiscItemId.blueprint_cannon,
							Amount = 1
						}
					}
				});
			
			// <<<< dummy data
			
			var bundles = new RewardBundleInfo();
			bundles.Bundles.AddRange(records);

			RewardBundleInfo = bundles;
			signal.TrySetResult(bundles);
			return await signal.Task;
		}

		public async UniTask<bool> StartBattle()
		{
			//TODO: DNGUYEN - gọi api start battle
			// nếu đủ resource thì trả về true
			return true;
		}
		
		public async UniTask<bool> ClaimRewardBundle()
		{
			//TODO: DNGUYEN - gọi api claim reward
			// claim thành công thì trả về true
			return true;
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