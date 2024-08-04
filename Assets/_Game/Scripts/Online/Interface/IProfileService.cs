using Online.Enum;
namespace Online.Interface
{
    public interface IProfileService
	{
		public string DisplayName { get; }
		public int Level { get; }
		public long Exp { get; }
		public ERank Rank { get; }
		
		// Profile
		public void UpdateEquipmentProfile(int index, string shipID, object grid, object stack);
	}
}