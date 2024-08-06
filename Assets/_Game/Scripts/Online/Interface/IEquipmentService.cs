using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online.Model.ResponseAPI;

namespace Online.Interface
{
	public interface IEquipmentService
	{
		// Update Equipment Ship
		public UniTask<BaseResponse> UpdateEquipmentShipAsync(ShipSetupSaveData data);
		
		// Get Equipment Ship
		public UniTask<ShipSetupSaveData> RequestEquipmentShipAsync();
	}
}