using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model.ResponseAPI;

namespace Online
{
	public partial class PlayfabManager : IEquipmentService
	{
		public UniTask<BaseResponse> UpdateEquipmentShipAsync(ShipSetupSaveData data)
		{
			return Equipment.UpdateEquipmentShipAsync(data);
		}
		
		public UniTask<ShipSetupSaveData> RequestEquipmentShipAsync()
		{
			return Equipment.RequestEquipmentShipAsync();
		}
	}
}