using Online.Enum;
using Online.Interface;
using Online.Service.Auth;
using Online.Service.Profile;

namespace Online
{
	public class PlayfabManager : UnityEngine.MonoBehaviour, IPlayfabManager
	{
		public static PlayfabManager Instance;

		private IOnlineService _authService = null;
		private IOnlineService _profileService = null;

		#region Services

		public AuthService Auth => _authService as AuthService;
		public ProfileService Profile => _profileService as ProfileService;

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

			_authService.Initialize(this);
			_profileService.Initialize(this);
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
						Profile.LoadProfile(infoPayload);
						break;
				}
			});
		}
	}
}