using Online.Enum;
using PlayFab.ClientModels;
using UnityEngine;

namespace Online.Interface
{
	public abstract class BasePlatformAuth
	{
		public abstract void Login(System.Action<ELoginStatus, GetPlayerCombinedInfoResultPayload> onLoginSucceed = null);

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