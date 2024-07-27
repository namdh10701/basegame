using System.Collections.Generic;
using Online.Enum;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IEquipmentService
	{
		public void UpdateEquipment(string data, System.Action<bool> cb = null)
		{
			Equipment.UpdateEquipment(data, cb);
		}
	}
}