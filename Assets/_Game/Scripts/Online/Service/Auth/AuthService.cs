using Online.Enum;
using Online.Interface;
using PlayFab.ClientModels;

namespace Online.Service.Auth
{
	public class AuthService : IOnlineService
	{
		public IPlayfabManager Manager { get; protected set; }
		
		private BasePlatformAuth _basePlatformAuth = null;

		public void Initialize(IPlayfabManager manager)
		{
			Manager = manager;
#if UNITY_EDITOR
			_basePlatformAuth = new EditorAuth();
#elif UNITY_IOS
			_platformWrapper = new iOSAuthWrapper();
#else
			_platformWrapper = new AndroidAuthWrapper();
#endif
		}

		public void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed)
		{
			_basePlatformAuth.Login(onLoginSucceed);
		}
	}
}