using System;
using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
namespace Online.Service.Leaderboard
{
	public class EquipmentService : BaseOnlineService
	{
		public ShipSetupSaveData EquipmentShips { get; private set; }
		
		public void UpdateEquipShip(ShipSetupSaveData shipSetupData, Action<bool> cb)
		{
			PlayFabClientAPI.UpdateUserData(new()
			{
				Data = new Dictionary<string, string>()
				{
					{
						"Equipment", Newtonsoft.Json.JsonConvert.SerializeObject(shipSetupData)
					}
				}
			}, result =>
			{
				EquipmentShips = shipSetupData;
				LogSuccess("Equip Ship!");
				cb?.Invoke(true);
			}, error =>
			{
				LogError(error.ErrorMessage);
				cb?.Invoke(false);
			});
		}
		
		public void RequestEquipmentShip(Action<bool> cb = null)
		{
			PlayFabClientAPI.GetUserData(new()
			{
				Keys = new List<string>()
				{
					"Equipment"
				}
			}, result =>
			{
				LoadEquipmentShip(result.Data);
				cb?.Invoke(true);
			}, error =>
			{
				LogError(error.ErrorMessage);
				cb?.Invoke(false);
			});
		}

		public void LoadEquipmentShip(Dictionary<string, UserDataRecord> userData)
		{
			if (userData.TryGetValue(C.NameConfigs.EquipmentShips, out var record))
			{
				EquipmentShips = Newtonsoft.Json.JsonConvert.DeserializeObject<ShipSetupSaveData>(record.Value);
			}
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Equipment");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Equipment");
		}
	}
}