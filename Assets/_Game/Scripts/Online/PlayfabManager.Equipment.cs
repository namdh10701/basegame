using _Game.Scripts.SaveLoad;
using Online.Interface;

namespace Online
{
	public partial class PlayfabManager : IEquipmentService
	{
		public void UpdateEquipShip(ShipSetupSaveData data, System.Action<bool> cb = null)
		{
			Equipment.UpdateEquipShip(data, cb);
		}
		
		public void RequestEquipShip(System.Action<bool> cb = null)
		{
			Equipment.RequestEquipmentShip(cb);
		}
	}
}