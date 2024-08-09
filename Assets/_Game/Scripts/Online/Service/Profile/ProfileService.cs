using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Online.Enum;
using Online.Model;
using Online.Model.ResponseAPI;
using Online.Model.ResponseAPI.Profile;
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
		public ERank UserRank { get; private set; } = ERank.Rookie;
		public int UserRankScore { get; private set; }
		public string UserRankID { get; private set; }
		public bool IsGuest { get; private set; }
		public List<LimitPackageModel> LimitPackages { get; private set; } = new();
		public CompleteSeasonInfo CompleteSeasonInfo { get; private set; }
		public List<GachaPackageModel> GachaPackages { get; private set; }

		#endregion

		public async UniTask<ProfileResponse> RequestProfileAsync(string playfabId)
		{
			var signal = new UniTaskCompletionSource<ProfileResponse>();
			PlayFabClientAPI.GetPlayerCombinedInfo(new()
			{
				PlayFabId = playfabId,
				InfoRequestParameters = new()
				{
					GetPlayerProfile = true,
					GetUserVirtualCurrency = true,
					GetUserData = true,
					GetUserReadOnlyData = true,
					GetUserInventory = true,
					GetUserAccountInfo = true,
				}
			}, (result) =>
			{
				signal.TrySetResult(new ProfileResponse()
				{
					Result = true,
					PlayerProfileModel = result.InfoResultPayload.PlayerProfile,
					UserVirtualCurrency = result.InfoResultPayload.UserVirtualCurrency,
					UserData = result.InfoResultPayload.UserData,
					UserReadOnlyData = result.InfoResultPayload.UserReadOnlyData,
					UserInventory = result.InfoResultPayload.UserInventory,
					UserAccountInfo = result.InfoResultPayload.AccountInfo
				});
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new()
				{
					Result = false,
					Error = EErrorCode.PlayfabError
				});
			});
			return await signal.Task;
		}

		public async UniTask<string> RequestDisplayNameAsync(string newDisplayName)
		{
			var signal = new UniTaskCompletionSource<string>();
			PlayFabClientAPI.UpdateUserTitleDisplayName(new()
			{
				DisplayName = newDisplayName
			}, (result) =>
			{
				LogSuccess("Display Name: " + result.DisplayName);
				signal.TrySetResult(result.DisplayName);
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult("");
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
				signal.TrySetResult(new()
				{
					Result = true
				});
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new()
				{
					Result = false,
					Error = EErrorCode.PlayfabError
				});
			});
			return await signal.Task;
		}

		public void LoadProfile(PlayerProfileModel playerProfile)
		{
			PlayfabID = playerProfile.PlayerId;
			DisplayName = playerProfile.DisplayName;
			IsGuest = playerProfile.Origination == LoginIdentityProvider.Custom;
		}

		public void SetLimitPackage(List<LimitPackageModel> limitPackages)
		{
			LimitPackages = limitPackages;
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

			if (readOnlyData.TryGetValue(C.NameConfigs.CurrentRankID, out var rankID))
			{
				UserRankID = rankID.Value;
			}

			if (readOnlyData.TryGetValue(C.NameConfigs.LimitPackages, out var records))
			{
				LimitPackages.Clear();
				LimitPackages = JsonConvert.DeserializeObject<List<LimitPackageModel>>(records.Value);
			}
			
			if (readOnlyData.TryGetValue(C.NameConfigs.GachasPackages, out var gachasPackages))
			{
				GachaPackages = JsonConvert.DeserializeObject<List<GachaPackageModel>>(gachasPackages.Value);
			}
			
			if(readOnlyData.TryGetValue(C.NameConfigs.CompleteSeasonInfo, out var completeSeasonInfo))
			{
				CompleteSeasonInfo = JsonConvert.DeserializeObject<CompleteSeasonInfo>(completeSeasonInfo.Value);
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