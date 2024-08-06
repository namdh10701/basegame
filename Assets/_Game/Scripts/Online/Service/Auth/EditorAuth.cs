using System;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ResponseAPI.Common;
using PlayFab;
using PlayFab.ClientModels;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Online.Service.Auth
{
	public class EditorAuth : BasePlatformAuth
	{
		public override async UniTask<LoginResponse> LoginAsync()
		{
			UniTaskCompletionSource<LoginResponse> signal = new UniTaskCompletionSource<LoginResponse>();
			PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
			{
				TitleId = PlayFabSettings.TitleId,
				CreateAccount = true,
				CustomId = SystemInfo.deviceUniqueIdentifier,
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
				signal.TrySetResult(new()
				{
					Result = true,
					Status = result.NewlyCreated ? ELoginStatus.Newly : ELoginStatus.Succeed,
					PlayfabID = result.PlayFabId,
					ResultPayload = result.InfoResultPayload
				});
			}, error =>
			{
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