using System;
using Cysharp.Threading.Tasks;
using Online.Enum;
using Online.Interface;
using Online.Model.ResponseAPI.Common;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Online.Service.Auth
{
	public class EditorAuth : BasePlatformAuth
	{
		public override async UniTask<LoginResponse> LoginAsync()
		{
			string deviceID = SystemInfo.deviceUniqueIdentifier;
			if (deviceID == "295087CA-A8F0-506A-A012-DBCF4DECA026")
				deviceID = "295087CA-A8F0-506A-A012-DBCF4DECA036";

			UniTaskCompletionSource<LoginResponse> signal = new UniTaskCompletionSource<LoginResponse>();
			PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
			{
				TitleId = PlayFabSettings.TitleId,
				CreateAccount = true,
				CustomId = deviceID
			}, result =>
			{
				Debug.Log("PlayfabID: " + result.PlayFabId);
				signal.TrySetResult(new()
				{
					Result = true,
					PlayfabID = result.PlayFabId
				});
			}, error =>
			{
				signal.TrySetResult(new()
				{
					Result = false,
					Error = EErrorCode.PlayfabError
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