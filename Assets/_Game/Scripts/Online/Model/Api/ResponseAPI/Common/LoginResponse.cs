using Online.Enum;
using PlayFab.ClientModels;

namespace Online.Model.ResponseAPI.Common
{
	public class LoginResponse : BaseResponse
	{
		public string PlayfabID { get; set; } = new("");
	}
}