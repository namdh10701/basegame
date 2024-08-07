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

		public async UniTask LinkFacebook()
		{
			await _basePlatformAuth.LinkFacebook();
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