using System;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ApiRequest;
using Online.Service.Auth;
using PlayFab.ClientModels;

namespace Online.Service
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

		public UniTask<LoginResponse> LoginAsync()
		{
			return _basePlatformAuth.LoginAsync();
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

		public class LoginResult
		{
			public ELoginStatus Status;
			public GetPlayerCombinedInfoResultPayload Payload;
		}
	}
}