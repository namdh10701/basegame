using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using PlayFab.ClientModels;

namespace Online.Interface
{
	public interface IEquipmentService
	{
		// Update Equipment Ship
		public void UpdateEquipShip(ShipSetupSaveData data, System.Action<bool> cb = null);
		
		// Get Equipment Ship
		public void RequestEquipShip(System.Action<bool> cb = null);
	}
}