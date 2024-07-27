using System.Collections.Generic;
using Online.Enum;
using Online.Interface;
using PlayFab;
using PlayFab.ClientModels;
namespace Online.Service.Leaderboard
{
	public class EquipmentService : BaseOnlineService
	{
		public override void Initialize(IPlayfabManager manager)
		{
			base.Initialize(manager);

		}

		public void UpdateEquipment(string data, System.Action<bool> cb = null)
		{
			PlayFabClientAPI.UpdateUserData(new()
			{
				Data = new Dictionary<string, string>()
				{
					{
						"Equipment", data
					}
				}
			}, result =>
			{
				LogSuccess("Update Equipment Success");
			}, error =>
			{
				LogError(error.ErrorMessage);
			});
		}

		public override void LogSuccess(string message)
		{
			LogEvent(false, message, "Equipment");
		}

		public override void LogError(string error)
		{
			LogEvent(true, error, "Equipment");
		}
	}
}