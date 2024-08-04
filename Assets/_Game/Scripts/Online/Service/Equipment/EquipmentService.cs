using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.SaveLoad;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;

namespace Online.Service
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
		
		public async Task<bool> UpdateEquipShipAsync(ShipSetupSaveData shipSetupData)
		{
			var result = await PlayFabAsync.PlayFabClientAPI.UpdateUserDataAsync(new()
			{
				Data = new Dictionary<string, string>()
				{
					{ "Equipment", Newtonsoft.Json.JsonConvert.SerializeObject(shipSetupData) }
				}
			});

			if (result.IsError)
			{
				LogError(result.Error.ErrorMessage);
				return false;
			}
			
			EquipmentShips = shipSetupData;
			LogSuccess("Equip Ship!");
			return true;
		}
		
		public async Task<bool> RequestEquipmentShipAsync()
		{
			var result = await PlayFabAsync.PlayFabClientAPI.GetUserDataAsync(new()
			{
				Keys = new List<string>
				{
					"Equipment"
				}
			});

			if (result.IsError)
			{
				LogError(result.Error.ErrorMessage);
				return false;
			}
			
			LoadEquipmentShip(result.Result.Data);
			
			LogSuccess("EquipmentShip Loaded!");
			return true;
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