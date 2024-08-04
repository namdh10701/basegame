using _Game.Scripts.SaveLoad;
using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model.ApiRequest;

namespace Online
{
	public partial class PlayfabManager : IEquipmentService
	{
		public UniTask<BaseResponse> UpdateEquipShip(ShipSetupSaveData data)
		{
			return Equipment.UpdateEquipShip(data);
		}
		
		public UniTask<ShipSetupSaveData> RequestEquipShip()
		{
			return Equipment.RequestEquipmentShipAsync();
		}
	}
}