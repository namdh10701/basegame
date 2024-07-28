namespace Online.Interface
{
    public interface IProfileService
	{
		public string DisplayName { get; }
		public int Level { get; }
		public long EXP { get; }
		public int Energy { get; }
		
		// Profile
		public void UpdateEquipmentProfile(int index, string shipID, object grid, object stack);
	}
}