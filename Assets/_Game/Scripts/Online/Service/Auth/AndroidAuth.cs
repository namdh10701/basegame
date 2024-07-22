using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Device;

namespace Online.Service.Auth
{
	public class AndroidAuth : BasePlatformAuth
	{
		public override void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed = null)
		{
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