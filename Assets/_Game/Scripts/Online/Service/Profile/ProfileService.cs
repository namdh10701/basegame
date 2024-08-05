using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Model.ApiRequest;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Online.Service
{
	public class ProfileService : BaseOnlineService
	{
		#region Properties

		public string PlayfabID { get; private set; }
		public string DisplayName { get; private set; }
		public int Level { get; private set; }
		public long Exp { get; private set; }
		public ERank UserRank { get; private set; } = ERank.Unrank;
		public int UserRankScore { get; private set; }
		public string UserRankID { get; private set; }
		public bool IsGuest { get; private set; }

		#endregion

		public UniTask RequestUserProfileAsync()
		{
			return UniTask.WhenAll(GetProfileAsync(), GetUserReadOnlyDataAsync());
		}
		
		public async UniTask<BaseResponse> RequestDisplayNameAsync()
		{
			var signal = new UniTaskCompletionSource<BaseResponse>();
			PlayFabClientAPI.UpdateUserTitleDisplayName(new()
			{
				DisplayName = "User" + Random.Range(1, 99999).ToString("D5")
			}, (result) =>
			{
				DisplayName = result.DisplayName;
				LogSuccess("Display Name: " + result.DisplayName);
				signal.TrySetResult(new() { Result = true });
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new() { Result = false, Error = error.ErrorMessage});
			});
			return await signal.Task;
		}

		private async UniTask<BaseResponse> GetProfileAsync()
		{
			var signal = new UniTaskCompletionSource<BaseResponse>();
			PlayFabClientAPI.GetPlayerProfile(new()
			{
				PlayFabId = PlayfabID
			}, (result) =>
			{
				LoadProfile(result.PlayerProfile);
				signal.TrySetResult(new() { Result = true });
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new() { Result = false, Error = error.ErrorMessage });
			});
			return await signal.Task;
		}
		
		private async UniTask<BaseResponse> GetUserReadOnlyDataAsync()
		{
			var signal = new UniTaskCompletionSource<BaseResponse>();
			PlayFabClientAPI.GetUserReadOnlyData(new()
			{
				PlayFabId = PlayfabID
			}, (result) =>
			{
				LoadUserReadOnlyData(result.Data);
				signal.TrySetResult(new() { Result = true });
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new() { Result = false, Error = error.ErrorMessage });
			});
			return await signal.Task;
		}

		public void LoadProfile(PlayerProfileModel playerProfile)
		{
			DisplayName = playerProfile.DisplayName;
			PlayfabID = playerProfile.PlayerId;
			IsGuest = playerProfile.Origination is null or LoginIdentityProvider.Custom;
		}
		
		public void LoadUserReadOnlyData(Dictionary<string, UserDataRecord> readOnlyData)
		{
			if (readOnlyData.TryGetValue(C.NameConfigs.Level, out var level))
			{
				Level = System.Convert.ToInt32(level.Value);
			}

			if (readOnlyData.TryGetValue(C.NameConfigs.Exp, out var exp))
			{
				Exp = System.Convert.ToInt32(exp.Value);
			}
			
			if (readOnlyData.TryGetValue(C.NameConfigs.Rank, out var record))
			{
				if (System.Enum.TryParse<ERank>(record.Value, out var rank))
					UserRank = rank;
			}

			if (readOnlyData.TryGetValue(C.NameConfigs.RankScore, out var scoreRecord))
			{
				UserRankScore = System.Convert.ToInt32(scoreRecord.Value);
			}
			
			if (readOnlyData.TryGetValue(C.NameConfigs.CurrentRankID, out var rankID))
			{
				UserRankID = rankID.Value;
			}
		}

		public async UniTask<string> UpdateDisplayName(string displayName)
		{
			var signal = new UniTaskCompletionSource<string>();
			PlayFabClientAPI.UpdateUserTitleDisplayName(new()
			{
				DisplayName = displayName
			}, (result) =>
			{
				LogSuccess("New DisplayName: " + result.DisplayName);
				DisplayName = result.DisplayName;
				signal.TrySetResult(DisplayName);
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(DisplayName);
			});
			return await signal.Task;
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Profile");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Profile");
		}
	}
}