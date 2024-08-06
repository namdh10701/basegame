using Online.Enum;

namespace Online.Model.ResponseAPI
{
	public class BaseResponse
	{
		public bool Result { get; set; }
		public EErrorCode Error { get; set; }
	}
}