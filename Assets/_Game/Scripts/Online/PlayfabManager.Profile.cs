using Online.Interface;

namespace Online
{
	public partial class PlayfabManager : IProfileService
	{
		#region Profile Service

		public string DisplayName => Profile.DisplayName;
		public int Level => Profile.Level.Level;
		public long Exp => Profile.Level.Exp;
		
		public void LoadProfile()
		{
			
		}
		
		public void UpdateEquipmentProfile(int index, string shipID, object grid, object stack)
		{
			
		}
		
		#endregion
	}
}