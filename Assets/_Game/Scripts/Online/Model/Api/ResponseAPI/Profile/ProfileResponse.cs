using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Model.ResponseAPI.Profile
{
	public class ProfileResponse : BaseResponse
	{
		public PlayerProfileModel PlayerProfileModel { get; set; }
		public Dictionary<string, int> UserVirtualCurrency { get; set; }
		public Dictionary<string, UserDataRecord> UserReadOnlyData
		{
			get;
			set;
		}
		public Dictionary<string, UserDataRecord> UserData
		{
			get;
			set;
		}
		public List<ItemInstance> UserInventory
		{
			get;
			set;
		}
		public UserAccountInfo UserAccountInfo
		{
			get;
			set;
		}
	}
}