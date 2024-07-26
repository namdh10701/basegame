using Online.Enum;
using Online.Interface;
using Online.Service.Auth;
using Online.Service.Leaderboard;
using Online.Service.Profile;
using PlayFab;
using UnityEngine;

namespace Online
{
	public partial class PlayfabManager : UnityEngine.MonoBehaviour, IPlayfabManager
	{
		public static PlayfabManager Instance;

		private IOnlineService _authService = null;
		private IOnlineService _profileService = null;
		private IOnlineService _inventoryService = null;

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
							RequestNewProfile();
						}
						else
						{
							Profile.LoadProfile(infoPayload.PlayerProfile, infoPayload.UserData, infoPayload.UserReadOnlyData);
							LoadProfile();
						}
						break;
				}
			});
		}

		public void LinkFacebook()
		{
			Auth.LinkFacebook();
		}

		public void RequestNewProfile()
		{
			PlayFabCloudScriptAPI.ExecuteFunction(new ()
			{
				FunctionName = C.CloudFunction.RequestNewProfile
			}, (result) =>
			{
				Debug.LogError(result.FunctionResult.ToString());
			}, (error) =>
			{
				Debug.LogError(error.GenerateErrorReport());
			});
		}
	}
}