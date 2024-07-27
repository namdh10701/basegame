using System.Collections.Generic;
using Facebook.Unity;
using JetBrains.Annotations;
using Online.Enum;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Online.Interface
{
	public abstract class BasePlatformAuth
	{
		public abstract void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed = null);
		public abstract void LinkPlatform(string token, System.Action<bool> cb = null);

		protected BasePlatformAuth()
		{
			InitFacebook();
		}

		private void InitFacebook(System.Action<bool> cb = null)
		{
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
		}

		public void LoginFacebook(System.Action<bool, string> cb)
		{
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
		}

		public void LinkFacebook(System.Action<bool> cb = null)
		{
			LoginFacebook((succeed, token) =>
			{
				if (!succeed) return;

				PlayFabClientAPI.LinkFacebookAccount(new LinkFacebookAccountRequest()
				{
					AccessToken = token,
				}, result =>
				{
					LogInfo("[Auth] Link Facebook Succeed");
					cb?.Invoke(true);
				}, error =>
				{
					LogError("[Auth] Link Facebook Failed: " + error.ErrorMessage);
					cb?.Invoke(false);
				});
			});
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