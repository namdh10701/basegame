using Online.Enum;
using Online.Interface;
using Online.Service.Auth;
using Online.Service.Leaderboard;
using Online.Service.Profile;

namespace Online
{
	public partial class PlayfabManager : UnityEngine.MonoBehaviour, IPlayfabManager
	{
		public static PlayfabManager Instance;

		private BaseOnlineService _authService = null;
		private BaseOnlineService _profileService = null;
		private BaseOnlineService _inventoryService = null;

		#region Services

		public AuthService Auth => _authService as AuthService;
		public ProfileService Profile => _profileService as ProfileService;
		public InventoryService Inventory => _inventoryService as InventoryService;

		#endregion

		private void Awake()
		{
			Instance = this;
			Initialize();
		}

		private void Initialize()
		{
			_authService = new AuthService();
			_profileService = new ProfileService();
			_inventoryService = new InventoryService();

			_authService.Initialize(this);
			_profileService.Initialize(this);
			_inventoryService.Initialize(this);
		}

		public void Login()
		{
			Auth.Login((result, infoPayload) =>
			{
				switch (result)
				{
					case ELoginStatus.Failed:
						break;

					default:
						if (result == ELoginStatus.Newly)
						{
							Profile.RequestNewProfile(result =>
							{
								if (result)
								{
									RequestInventory();
								}
							});
						}
						else
						{
							Profile.LoadProfile(infoPayload.PlayerProfile, infoPayload.UserData, infoPayload.UserReadOnlyData);
							Inventory.LoadVirtualCurrency(infoPayload.UserVirtualCurrency);
							Inventory.LoadItems(infoPayload.UserInventory);
						}
						break;
				}
			});
		}

		public void LinkFacebook()
		{
			Auth.LinkFacebook();
		}
	}
}