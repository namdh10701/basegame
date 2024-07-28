using System;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Online.Service.Auth
{
	public class IOSAuth : BasePlatformAuth
	{
		public IOSAuth() : base()
		{
			
		}
		
		public override void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed = null)
		{
			PlayFabClientAPI.LoginWithIOSDeviceID(new LoginWithIOSDeviceIDRequest()
			{
				TitleId = PlayFabSettings.TitleId,
				CreateAccount = true,
				DeviceModel = SystemInfo.deviceModel,
				DeviceId = SystemInfo.deviceUniqueIdentifier,
				InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
				{
					GetPlayerProfile = true,
					GetUserVirtualCurrency = true,
				}
			}, result =>
			{
				LogInfo($"Login result [PlayfabID: {result.PlayFabId}]");
				onLoginSucceed?.Invoke(result.NewlyCreated ? ELoginStatus.Newly : ELoginStatus.Succeed, result.InfoResultPayload);
			}, error =>
			{
				LogError($"Login Failed: {error.ErrorMessage}");
				onLoginSucceed?.Invoke(ELoginStatus.Failed, null);
			});
		}

		public override void LinkPlatform(string token, Action<bool> cb = null)
		{
			PlayFabClientAPI.LinkApple(new LinkAppleRequest()
			{
				IdentityToken = token
			}, result =>
			{
				LogInfo("Link Apple Succeed");
				cb?.Invoke(true);
			}, error =>
			{
				LogError("Link Apple Failed: " + error.ErrorMessage);
				cb?.Invoke(false);
			});
		}

		public override void LogInfo(string logText)
		{
			base.LogInfo("[iOS] " + logText);
		}

		public override void LogError(string errorText)
		{
			base.LogError("[iOS] " + errorText);
		}
	}
}