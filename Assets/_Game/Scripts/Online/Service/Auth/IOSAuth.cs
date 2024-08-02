using System;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ApiRequest;
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
		
		public override async UniTask<LoginResponse> LoginAsync()
		{
			UniTaskCompletionSource<LoginResponse> signal = new UniTaskCompletionSource<LoginResponse>();
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
					GetUserData = true,
					GetUserReadOnlyData = true,
					GetUserInventory = true
				}
			}, result =>
			{
				LogInfo($"Login result [PlayfabID: {Newtonsoft.Json.JsonConvert.SerializeObject(result.InfoResultPayload)}]");
				signal.TrySetResult(new()
				{
					Result = true,
					Status = result.NewlyCreated ? ELoginStatus.Newly : ELoginStatus.Succeed,
					PlayfabID = result.PlayFabId,
					ResultPayload = result.InfoResultPayload
				});
			}, error =>
			{
				LogError($"Login Failed: {error.ErrorMessage}");
				signal.TrySetResult(new()
				{
					Result = false,
					Status = ELoginStatus.Failed,
					PlayfabID = null,
					ResultPayload = null
				});
			});
			return await signal.Task;
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