using System.Collections.Generic;
namespace Online.Model.ApiRequest
{
	public class ReportLimitPackageResponse : BaseResponse
	{
		public List<LimitPackageModel> LimitPackages { get; set; }
	}
}