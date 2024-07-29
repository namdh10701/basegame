using System;
using System.Threading.Tasks;
using Facebook.Unity;
using Online.Enum;
using Online.Interface;
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

		[Obsolete("Use LoginAsync instead")]
		public void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed)
		{
			_basePlatformAuth.Login(onLoginSucceed);
		}
		
		public async Task<LoginResult> LoginAsync()
		{
			TaskCompletionSource<LoginResult> signal = new TaskCompletionSource<LoginResult>();
			_basePlatformAuth.Login((result, infoPayload) =>
			{
				signal.TrySetResult(new()
				{
					Status = result,
					Payload = infoPayload
				});
			});
			return await signal.Task;
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