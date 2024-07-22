namespace Online.Interface
{
	public interface IOnlineService
	{
		IPlayfabManager Manager { get; }
		
		void Initialize(IPlayfabManager manager);
	}
}