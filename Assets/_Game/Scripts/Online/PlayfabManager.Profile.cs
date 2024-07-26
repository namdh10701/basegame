using Online.Enum;
using Online.Interface;
using Online.Service.Auth;
using Online.Service.Leaderboard;
using Online.Service.Profile;
using PlayFab;
using UnityEngine;

namespace Online
{
	public partial class PlayfabManager : IProfileService
	{
		#region Profile Service

		public string DisplayName => Profile.DisplayName;
		public int Level => Profile.Level.Level;
		public long EXP => Profile.Level.Exp;
		public int Energy => Profile.Energy.Energy;
		
		public void LoadProfile()
		{
			
		}
		
		public void UpdateEquipmentProfile(int index, string shipID, object grid, object stack)
		{
			
		}
		
		#endregion
	}
}