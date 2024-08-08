using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Online.Model.ResponseAPI.Common;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

#if ENABLE_FACEBOOK
using Facebook.Unity;
#endif

namespace Online.Interface
{
	public abstract class BasePlatformAuth
	{
		public abstract UniTask<LoginResponse> LoginAsync();
		public abstract void LinkPlatform(string token, System.Action<bool> cb = null);

		protected BasePlatformAuth()
		{
			InitFacebook();
		}

		private void InitFacebook(System.Action<bool> cb = null)
		{
#if ENABLE_FACEBOOK
			if (!FB.IsInitialized)
			{
				FB.Init(() =>
				{
					if (FB.IsInitialized)
					{
						FB.ActivateApp();
						cb?.Invoke(true);
					}
					else
					{
						cb?.Invoke(false);
					}
				});
			}
			else
			{
				FB.ActivateApp();
				cb?.Invoke(true);
			}
#else
			cb?.Invoke(false);
#endif
		}

		public async UniTask<(bool, string)> LoginFacebook()
		{
			var signal = new UniTaskCompletionSource<(bool, string)>();
#if ENABLE_FACEBOOK
			if (FB.IsLoggedIn)
			{
				signal.TrySetResult((true, FB.ClientToken));
			}
			else
			{
				var perms = new List<string>()
				{
					"public_profile",
					"email"
				};
				FB.LogInWithReadPermissions(perms, (result) =>
				{
					if (FB.IsLoggedIn)
					{
						var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
						signal.TrySetResult((true, aToken.TokenString));
					}
					else
					{
						signal.TrySetResult((false, ""));
					}
				});
			}
#else
			signal.TrySetResult((false, ""));
#endif
			return await signal.Task;
		}

		public async UniTask<bool> LinkFacebook()
		{
			var signal = new UniTaskCompletionSource<bool>();
			var (result, token) = await LoginFacebook();
			if (result)
			{
				LogInfo("[Auth] LinkFB, Token: " + token);
				PlayFabClientAPI.LinkFacebookAccount(new()
				{
					AccessToken = token,
				}, result =>
				{
					LogInfo("[Auth] Link Facebook Succeed");
					signal.TrySetResult(true);
				}, error =>
				{
					LogError("[Auth] Link Facebook Failed: " + error.ErrorMessage);
					signal.TrySetResult(false);
				});
			}
			else
			{
				signal.TrySetResult(false);
			}
			return await signal.Task;
		}

		public UniTask<bool> UnlinkFacebook()
		{
			var signal = new UniTaskCompletionSource<bool>();
			PlayFabClientAPI.UnlinkFacebookAccount(new UnlinkFacebookAccountRequest(), result =>
			{
				LogInfo("[Auth] Unlink Facebook Succeed");
				signal.TrySetResult(true);
			}, error =>
			{
				LogError("[Auth] Unlink Facebook Failed: " + error.ErrorMessage);
				signal.TrySetResult(false);
			});
			return signal.Task;
		}

		public virtual void LogInfo(string logText)
		{
			Debug.Log(logText);
		}

		public virtual void LogError(string errorText)
		{
			Debug.LogError(errorText);
		}
	}
}