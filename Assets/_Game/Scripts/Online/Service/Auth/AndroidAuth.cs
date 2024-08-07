using Cysharp.Threading.Tasks;
using Online.Interface;
using Online.Model.ResponseAPI.Common;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

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
				AndroidDeviceId = SystemInfo.deviceUniqueIdentifier
			}, result =>
			{
				Debug.Log("PlayfabID: " + result.PlayFabId);
				signal.TrySetResult(new()
				{
					Result = true,
					PlayfabID = result.PlayFabId,
				});
			}, error =>
			{
				LogError($"Login Failed: {error.ErrorMessage}");
				signal.TrySetResult(new() { Result = false });
			});
			return await signal.Task;
		}

		public override void LinkPlatform(string token, System.Action<bool> cb = null)
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