using Online.Enum;
using PlayFab.ClientModels;
namespace Online.Model.ApiRequest
{
	public class LoginResponse : BaseResponse
	{
		public ELoginStatus Status { get; set; }
		public string PlayfabID { get; set; }
		public GetPlayerCombinedInfoResultPayload ResultPayload { get; set; }
	}
}