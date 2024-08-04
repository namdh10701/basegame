using System;
using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ApiRequest;
using PlayFab;
using PlayFab.ClientModels;

namespace Online.Service
{
	public class EquipmentService : BaseOnlineService
	{
		public ShipSetupSaveData EquipmentShips { get; private set; }

		public async UniTask<BaseResponse> UpdateEquipShip(ShipSetupSaveData shipSetupData)
		{
			var signal = new UniTaskCompletionSource<BaseResponse>();
			PlayFabClientAPI.UpdateUserData(new()
			{
				Data = new Dictionary<string, string>()
				{
					{
						C.NameConfigs.Equipment, Newtonsoft.Json.JsonConvert.SerializeObject(shipSetupData)
					}
				}
			}, result =>
			{
				EquipmentShips = shipSetupData;
				LogSuccess("Equip Ship!");
				signal.TrySetResult(new()
				{
					Result = true
				});
			}, error =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(new()
				{
					Result = false,
					Error = error.ErrorMessage
				});
			});
			return await signal.Task;
		}

		public async UniTask<ShipSetupSaveData> RequestEquipmentShipAsync()
		{
			var signal = new UniTaskCompletionSource<ShipSetupSaveData>();
			PlayFabClientAPI.GetUserData(new()
			{
				Keys = new List<string>()
				{
					C.NameConfigs.Equipment
				}
			}, result =>
			{
				LoadEquipmentShip(result.Data);
				signal.TrySetResult(EquipmentShips);
			}, error =>
			{
				LogError(error.ErrorMessage);
				signal.TrySetResult(null);
			});
			return await signal.Task;
		}

		public void LoadEquipmentShip(Dictionary<string, UserDataRecord> userData)
		{
			if (userData.TryGetValue(C.NameConfigs.Equipment, out var record))
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