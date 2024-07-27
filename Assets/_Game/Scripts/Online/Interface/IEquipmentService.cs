using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Interface
{
	public interface IEquipmentService
	{
		public void UpdateEquipment(string data, System.Action<bool> cb = null);
	}
}