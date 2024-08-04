using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IAdService
	{
		public async UniTask ShowVideoAd(string adUnitId)
		{
			var canShow = await Ads.CanWatchAd(adUnitId);
			if (canShow && await Ads.ShowVideoAd(adUnitId))
			{
				await Inventory.RequestInventoryAsync();
			}
		}
	}
}