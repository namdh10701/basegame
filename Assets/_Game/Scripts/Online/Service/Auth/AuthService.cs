using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ResponseAPI.Common;
using Online.Service.Auth;
using PlayFab.ClientModels;

namespace Online.Service
{
	public class AuthService : BaseOnlineService
	{
		public UserFacebookInfo FacebookInfo { get; private set; }
#if UNITY_ANDROID
		public UserGooglePlayGamesInfo GooglePlayGamesInfo { get; private set; }
#else
		public UserAppleIdInfo AppleIdInfo { get; private set; }
#endif

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

		public void LoadAccountInfo(UserAccountInfo accountInfo)
		{
			FacebookInfo = accountInfo.FacebookInfo;
#if UNITY_ANDROID
			GooglePlayGamesInfo = accountInfo.GooglePlayGamesInfo;
#else
			UserAppleIdInfo = accountInfo.UserAppleIdInfo;
#endif
		}

		public UniTask<bool> LinkFacebook()
		{
			return _basePlatformAuth.LinkFacebook();
		}

		public async UniTask UnlinkFacebook()
		{
			await _basePlatformAuth.UnlinkFacebook();
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