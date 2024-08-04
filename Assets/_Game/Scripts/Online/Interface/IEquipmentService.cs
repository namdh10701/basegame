using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.SaveLoad;
using PlayFab.ClientModels;

namespace Online.Interface
{
	public interface IEquipmentService
	{
		// Update Equipment Ship
		[Obsolete("Use async function instead")]
		public void UpdateEquipShip(ShipSetupSaveData data, System.Action<bool> cb = null);
		
		// Get Equipment Ship
		[Obsolete("Use async function instead")]
		public void RequestEquipShip(System.Action<bool> cb = null);
		
		// Update Equipment Ship
		public Task<bool> UpdateEquipShipAsync(ShipSetupSaveData data);
		
		// Get Equipment Ship
		public Task<bool> RequestEquipShipAsync();
	}
}