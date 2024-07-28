using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Online.Enum;
using Online.Interface;
using PlayFab.ClientModels;

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