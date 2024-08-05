using System;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ApiRequest;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Device;

namespace Online.Service.Auth
{
	public class AndroidAuth : BasePlatformAuth
	{
		public override async UniTask<LoginResponse> LoginAsync()
		{
			UniTaskCompletionSource<LoginResponse> signal = new UniTaskCompletionSource<LoginResponse>();
			PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest()
			{
				TitleId = PlayFabSettings.TitleId,
				CreateAccount = true,
				AndroidDevice = SystemInfo.deviceModel,
				AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
				InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
				{
					GetPlayerProfile = true,
					GetUserVirtualCurrency = true,
					GetUserData = true,
					GetUserReadOnlyData = true,
					GetUserInventory = true,
					GetUserAccountInfo = true,
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
			cb?.Invoke(false);
		}


		public override void LogInfo(string logText)
		{
			base.LogInfo("[Android] " + logText);
		}

		public override void LogError(string errorText)
		{
			base.LogError("[Android] " + errorText);
		}
	}
}