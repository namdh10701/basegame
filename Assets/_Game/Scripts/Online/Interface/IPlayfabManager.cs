using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Online.Interface
{
	public interface IPlayfabManager
	{
		UniTask UpgradeItem(string itemInstanceId);
		UniTask CombineItems(List<string> itemInstanceIds);
		
		void RunCoroutine(IEnumerator coroutine);
	}
}