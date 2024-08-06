using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Online.Enum;
using Online.Model;
using Online.Model.ResponseAPI;
using Online.Model.ResponseAPI.Ranking;
using PlayFab;
using UnityEngine;

namespace Online.Service
{
	public class RankingService : BaseOnlineService
	{
		public SeasonInfo SeasonInfo { get; private set; }
		public RankInfo RankInfo { get; private set; }

		public async UniTask RequestUserRankAsync()
		{
			await RequestSeasonInfoAsync();

			var leaderboard = await GetLeaderboardAsync(0, 50);

			RankInfo = new() { Count = leaderboard.Count, Players = leaderboard.Players};
			
			LogSuccess("RequestUserRankAsync!");
		}

		public async UniTask<LeaderboardResponse> GetLeaderboardAsync(int begin, int count)
		{
			var signal = new UniTaskCompletionSource<LeaderboardResponse>();
			PlayFabClientAPI.GetLeaderboard(new()
			{
				StatisticName = C.RankConfigs.GetLeaderboard(Manager.Profile.UserRank),
				StartPosition = begin,
				MaxResultsCount = count
			}, result =>
			{
				var leaderboardRes = new LeaderboardResponse();
				leaderboardRes.Count = result.Leaderboard.Count;
				foreach (var item in result.Leaderboard)
				{
					leaderboardRes.Players.Add(new PlayerRankInfo()
					{
						Id = item.PlayFabId,
						DisplayName = item.DisplayName,
						Score = item.StatValue
					});
				}
				LogSuccess("GetLeaderboardAsync, Count: " + leaderboardRes.Count);
				signal.TrySetResult(leaderboardRes);
			}, error =>
			{
				LogError("GetLeaderboardAsync, Error: " + error.ErrorMessage);
				signal.TrySetResult(new LeaderboardResponse());
			});
			return await signal.Task;
		}

		public async UniTask<SeasonInfoResponse> RequestSeasonInfoAsync()
		{
			var signal = new UniTaskCompletionSource<SeasonInfoResponse>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.RequestSeasonInfo
			}, result =>
			{
				var resp = JsonConvert.DeserializeObject<SeasonInfoResponse>(result.FunctionResult.ToString());
				SeasonInfo = resp.SeasonInfo;
				signal.TrySetResult(resp);
			}, error =>
			{
				SeasonInfo = null;
				signal.TrySetResult(null);
			});
			return await signal.Task;
		}

		public async UniTask<BaseResponse> CreateRankTicketAsync()
		{
			var signal = new UniTaskCompletionSource<BaseResponse>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.CreateRankTicket
			}, result =>
			{
				var resp = JsonConvert.DeserializeObject<BaseResponse>(result.FunctionResult.ToString());
				signal.TrySetResult(resp);
			}, error =>
			{
				LogError("Create Rank Ticket Error: " + error.ErrorMessage);
				// signal.TrySetResult(false);
				signal.TrySetException(new Exception("Create Rank Ticket Error: " + error.ErrorMessage));
			});
			return await signal.Task;
		}

		public async UniTask<SubmitRankingResponse> SubmitRankingMatchAsync(int totalDamage)
		{
			var signal = new UniTaskCompletionSource<SubmitRankingResponse>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.SubmitRankingMatchAsync,
				FunctionParameter = new
				{
					Score = totalDamage
				}
			}, result =>
			{
				var rankResponse = JsonConvert.DeserializeObject<SubmitRankingResponse>(result.FunctionResult.ToString());
				RankInfo = rankResponse.RankInfo;
				LogSuccess("Submit Ranking!");
				signal.TrySetResult(rankResponse);
			}, error =>
			{
				LogError("Submit Ranking Match Error: " + error.ErrorMessage);
				signal.TrySetResult(null);
			});
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
				// bundle.Records.Add(new RankReward()
				// {
				//     ItemId = "Cannon_0001",
				//     Amount = 10
				// });
			}

			bundles.Bundles.Add(bundle);
			// <<<< dummy data

			signal.TrySetResult(bundles);
			return await signal.Task;
		}

		public async UniTask<RankTicketResponse> CreatRankTicketAsync()
		{
			var signal = new UniTaskCompletionSource<RankTicketResponse>();
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.CreateRankTicket
			}, result =>
			{
				LogSuccess("CreatRankTicket Completed!");
				signal.TrySetResult(JsonConvert.DeserializeObject<RankTicketResponse>(result.FunctionResult.ToString()));
			}, error =>
			{
				LogError("CreatRankTicket, Error: " + error.ErrorMessage);
				signal.TrySetResult(new RankTicketResponse()
				{
					Result = false,
					Error = EErrorCode.PlayfabError
				});
			});
			return await signal.Task;
		}

		public async UniTask<bool> EndBattle(int score)
		{
			//TODO: DNGUYEN - gọi api end battle tại Tier hiện tại
			/* logic backend:
			 * Từ Score truyền lên tính lại rank cho user
			 * Nếu user lên rank (ví dụ từ 35 -> 30) thì sẽ lấy reward của rank 35 trả cho user
			 * Tra cứu reward tại đây: https://docs.google.com/spreadsheets/d/1NaQMjBxUDNAr4nmQC4NYn8mjq1sdAM8lswMUQ6w7zAE/edit?gid=1102932041#gid=1102932041
			 */
			// request thành công thì trả về true
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