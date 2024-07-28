namespace Online.Interface
{
	public interface IPlayfabManager
	{
		void UpgradeItem(string itemInstanceId, System.Action<bool> cb = null);
	}
}