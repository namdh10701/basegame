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
		public LevelModel Level { get; private set; }

		#endregion

		public void LoadProfile(PlayerProfileModel playerProfile, Dictionary<string, UserDataRecord> readOnlyData)
		{
			PlayfabID = playerProfile.PlayerId;
			DisplayName = playerProfile.DisplayName;

			if (readOnlyData.TryGetValue(C.NameConfigs.Level, out var levelRecord))
			{
				Level = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelModel>(levelRecord.Value);
			}
			else
			{
				Level = new LevelModel()
				{
					Level = 1,
					Exp = 0
				};
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