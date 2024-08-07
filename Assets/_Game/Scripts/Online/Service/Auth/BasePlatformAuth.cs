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

		public void LoginFacebook(System.Action<bool, string> cb)
		{
#if ENABLE_FACEBOOK
			if (FB.IsLoggedIn)
			{
				cb?.Invoke(true, FB.ClientToken);
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
						cb?.Invoke(true, aToken.TokenString);
					}
					else
					{
						cb?.Invoke(false, "");
						Debug.Log("User cancelled login");
					}
				});
			}
#else
			cb?.Invoke(false, "Not init!");
#endif
		}

		public UniTask<bool> LinkFacebook()
		{
			var signal = new UniTaskCompletionSource<bool>();
			LoginFacebook((succeed, token) =>
			{
				if (!succeed)
				{
					signal.TrySetResult(false);
					return;
				}

				PlayFabClientAPI.LinkFacebookAccount(new LinkFacebookAccountRequest()
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
			});
			return signal.Task;
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