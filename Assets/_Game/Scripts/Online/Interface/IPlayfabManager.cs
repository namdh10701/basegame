using System.Collections;

namespace Online.Interface
{
	public interface IPlayfabManager
	{
		void UpgradeItem(string itemInstanceId, System.Action<bool> cb = null);
		
		void RunCoroutine(IEnumerator coroutine);
	}
}