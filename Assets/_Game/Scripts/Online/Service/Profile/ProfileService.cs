using System.Collections.Generic;
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
		public EnergyModel Energy { get; private set; }
		public Dictionary<string, EquipmentModel> EquipmentProfiles { get; private set; }

		#endregion

		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);
		}

		public void LoadProfile(PlayerProfileModel playerProfile, Dictionary<string, UserDataRecord> userData, Dictionary<string, UserDataRecord> readOnlyData)
		{
			PlayfabID = playerProfile.PlayerId;
			DisplayName = playerProfile.DisplayName;
			EquipmentProfiles = new();

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

			if (readOnlyData.TryGetValue(C.NameConfigs.Energy, out var energyRecord))
			{
				Energy = Newtonsoft.Json.JsonConvert.DeserializeObject<EnergyModel>(energyRecord.Value);
			}
			else
			{
				Energy = new EnergyModel()
				{
					Energy = 100
				};
			}

			for (int i = 0; i < C.ProfileConfigs.TotalProfile; i++)
			{
				var keyProfile = string.Format(C.NameConfigs.EquipmentProfile, i);
				if (userData.TryGetValue(keyProfile, out var equipmentRecord))
				{
					EquipmentProfiles.Add(keyProfile, Newtonsoft.Json.JsonConvert.DeserializeObject<EquipmentModel>(equipmentRecord.Value));
				}
				else
				{
					EquipmentProfiles.Add(keyProfile, new EquipmentModel());
				}
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