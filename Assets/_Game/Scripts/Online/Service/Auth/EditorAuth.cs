using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Online.Service.Auth
{
	public class EditorAuth : BasePlatformAuth
	{
		public override void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed = null)
		{
			LogInfo("Login with: " + SystemInfo.deviceUniqueIdentifier);
			PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
			{
				TitleId = PlayFabSettings.TitleId,
				CreateAccount = true,
				CustomId = SystemInfo.deviceUniqueIdentifier,
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
			base.LogInfo("[Editor] " + logText);
		}

		public override void LogError(string errorText)
		{
			base.LogError("[Editor] " + errorText);
		}
	}
}