using System.Collections.Generic;
using _Game.Scripts.GD.DataManager;
using Online.Interface;
using Online.Model;
using PlayFab.ClientModels;

namespace Online.Service.Profile
{
	public class ProfileService : IOnlineService
	{
		public IPlayfabManager Manager { get; private set; }


		#region Properties

		public string PlayfabID { get; private set; }
		public string DisplayName { get; private set; }
		public LevelModel Level { get; private set; }
		public EnergyModel Energy { get; private set; }
		public Dictionary<string, EquipmentModel> EquipmentProfiles { get; private set; }

		#endregion

		public void Initialize(IPlayfabManager manager)
		{
			Manager = manager;
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
	}
}