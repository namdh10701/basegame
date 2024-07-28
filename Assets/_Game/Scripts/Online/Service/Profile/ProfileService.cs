using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Online.Interface;
using Online.Model;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Online.Service.Profile
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

		public void RequestNewProfile(System.Action<bool> cb = null)
		{
			PlayFabClientAPI.UpdateUserTitleDisplayName(new()
			{
				DisplayName = "User" + Random.Range(0, 10000).ToString("D5")
			}, (result) =>
			{
				LogSuccess("New name: " + result.DisplayName);
			}, (error) =>
			{
				LogError(error.ErrorMessage);
			});
			
			PlayFabClientAPI.ExecuteCloudScript(new()
			{
				FunctionName = C.CloudFunction.RequestNewProfile
			}, (result) =>
			{
				LogSuccess("Create new profile!");
				cb?.Invoke(true);
			}, (error) =>
			{
				LogError(error.ErrorMessage);
				cb?.Invoke(false);
			});
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