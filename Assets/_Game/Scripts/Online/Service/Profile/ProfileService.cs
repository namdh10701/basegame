using System.Collections.Generic;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;

namespace Online.Service.Profile
{
	public class ProfileService : IOnlineService, IProfile
	{
		public IPlayfabManager Manager { get; protected set; }

		public string PlayfabID => _playfabID;

		#region IProfile

		public string DisplayName { get; set; }
		public Dictionary<EVirtualCurrency, int> Currencies { get; set; } = new();

		#endregion

		#region Variables

		private string _playfabID = null;

		#endregion

		public void Initialize(IPlayfabManager manager)
		{
			Manager = manager;
			Currencies = new Dictionary<EVirtualCurrency, int>()
			{
				{
					EVirtualCurrency.Coin, 0
				},
				{
					EVirtualCurrency.Gem, 0
				}
			};
		}
		
		public void LoadProfile(GetPlayerCombinedInfoResultPayload infoPayload)
		{
			DisplayName = infoPayload.PlayerProfile.DisplayName;
			foreach (EVirtualCurrency currency in System.Enum.GetValues(typeof(EVirtualCurrency)))
			{
				if (infoPayload.UserVirtualCurrency.TryGetValue(C.NameConfigs.Currencies[currency], out int value))
				{
					Currencies[currency] = value;
				}
			}
		}
		
		public void SetPlayfabID(string playfabId)
		{
			_playfabID = playfabId;
		}
	}
}