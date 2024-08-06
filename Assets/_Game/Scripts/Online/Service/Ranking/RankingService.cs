using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Generators;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Online.Enum;
using Online.Interface;
using Online.Model;
using Online.Model.ApiRequest;
using PlayFab;
using PlayFab.ClientModels;
using Random = UnityEngine.Random;

namespace Online.Service
{
    public class RankingService : BaseOnlineService
    {
        public RankInfo RankInfo { get; private set; }
        public UserRankInfo UserRankInfo { get; private set; }

        public async UniTask<bool> RequestUserRankAsync()
        {
            var signal = new UniTaskCompletionSource<bool>();
            PlayFabClientAPI.ExecuteCloudScript(new()
            {
                FunctionName = C.CloudFunction.GetRankInfo
            }, result =>
            {
                // // UnRank case
                // if (result.FunctionResult == null)
                // {
                //     RankInfo = new RankInfo();
                //
                //     RankInfo.EndTimestamp = (ulong)DateTime.Now.AddHours(10).ToFileTimeUtc();
                //     RankInfo.SeasonNo = 1;
                //     RankInfo.SeasonName = "Octopus XXX";
                //     
                //     UserRankInfo = new UserRankInfo();
                //     UserRankInfo.Players = new PlayerRankInfo [50];
                //
                //     var playerName = PlayerNameGenerator.GeneratePlayerNames(50);
                //     for (int i = 0; i < 50; i++)
                //     {
                //         UserRankInfo.Players[i] = new PlayerRankInfo
                //         {
                //             DisplayName = playerName[i],
                //             Id = "fake_user",
                //         };
                //     }
                //
                //     var userRank = Random.Range(0, 50);
                //     UserRankInfo.Players[userRank] = new PlayerRankInfo
                //     {
                //         DisplayName = PlayfabManager.Instance.DisplayName,
                //         Id = PlayfabManager.Instance.Profile.PlayfabID
                //     };
                //     
                //     signal.TrySetResult(true);
                //     return;
                // }
                var rankResponse = JsonConvert.DeserializeObject<RankInfoResponse>(result.FunctionResult.ToString());
                RankInfo = rankResponse.RankInfo;
                UserRankInfo = rankResponse.UserRankInfo;

                if (UserRankInfo == null)
                {
                    UserRankInfo = new UserRankInfo();
                    UserRankInfo.Players = new PlayerRankInfo [50];

                    var playerName = PlayerNameGenerator.GeneratePlayerNames(50);
                    for (int i = 0; i < 50; i++)
                    {
                        UserRankInfo.Players[i] = new PlayerRankInfo
                        {
                            DisplayName = playerName[i],
                            Id = "fake_user",
                        };
                    }

                    var userRank = Random.Range(0, 50);
                    UserRankInfo.Players[userRank] = new PlayerRankInfo
                    {
                        DisplayName = PlayfabManager.Instance.DisplayName,
                        Id = PlayfabManager.Instance.Profile.PlayfabID
                    };
                }
                
                signal.TrySetResult(true);
            }, error =>
            {
                LogError("Get User Rank Error: " + error.ErrorMessage);
                signal.TrySetResult(false);
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
                UserRankInfo = rankResponse.UserRankInfo;
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

		public async UniTask<bool> StartBattle()
		{
			//TODO: DNGUYEN - gọi api start battle tại Tier hiện tại
			// nếu đủ resource thì trả về true
			return true;
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