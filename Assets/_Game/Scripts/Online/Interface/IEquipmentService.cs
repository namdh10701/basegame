using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online.Model.ApiRequest;
using PlayFab.ClientModels;

namespace Online.Interface
{
	public interface IEquipmentService
	{
		// Update Equipment Ship
		public UniTask<BaseResponse> UpdateEquipShip(ShipSetupSaveData data);
		
		// Get Equipment Ship
		public UniTask<ShipSetupSaveData> RequestEquipShip();
	}
}