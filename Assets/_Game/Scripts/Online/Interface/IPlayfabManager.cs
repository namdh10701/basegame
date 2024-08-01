using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Online.Interface
{
	public interface IPlayfabManager
	{
		UniTask UpgradeItem(string itemInstanceId);
		
		void MergeItem(List<string> itemInstanceIds, System.Action<bool> cb = null);
		
		void RunCoroutine(IEnumerator coroutine);
	}
}