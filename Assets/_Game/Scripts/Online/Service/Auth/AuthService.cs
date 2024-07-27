using Facebook.Unity;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;

namespace Online.Service.Auth
{
	public class AuthService : BaseOnlineService
	{
		private BasePlatformAuth _basePlatformAuth = null;

		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);
#if UNITY_EDITOR
			_basePlatformAuth = new EditorAuth();
#elif UNITY_IOS
			_basePlatformAuth = new IOSAuth();
#else
			_basePlatformAuth = new AndroidAuth();
#endif
		}

		public void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed)
		{
			_basePlatformAuth.Login(onLoginSucceed);
		}

		public void LinkFacebook()
		{
			_basePlatformAuth.LinkFacebook();
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Auth");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Auth");
		}
	}
}