using Online.Enum;
namespace Online.Model.ApiRequest
{
	public class BaseResponse
	{
		public bool Result { get; set; }
		public EErrorCode Error { get; set; }
	}
}