using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Model;
using PlayFab;
using PlayFab.ClientModels;

namespace Online.Service
{
	public class ProfileService : BaseOnlineService
	{
		#region Properties

		public string PlayfabID { get; private set; }
		public string DisplayName { get; private set; }
		public int Level { get; private set; }
		public long Exp { get; private set; }
		
		#endregion

		public void LoadProfile(PlayerProfileModel playerProfile, Dictionary<string, UserDataRecord> readOnlyData)
		{
			PlayfabID = playerProfile.PlayerId;
			DisplayName = playerProfile.DisplayName;

			if (readOnlyData.TryGetValue(C.NameConfigs.Level, out var level))
			{
				Level = Convert.ToInt32(level.Value);
			}
			
			if (readOnlyData.TryGetValue(C.NameConfigs.Exp, out var exp))
			{
				Exp = Convert.ToInt32(exp.Value);
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