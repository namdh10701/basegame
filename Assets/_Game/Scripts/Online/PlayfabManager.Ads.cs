using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online
{
	public partial class PlayfabManager : IAdService
	{
		public bool CanShowAd(string adUnitId)
		{
			return false;
		}
	}
}