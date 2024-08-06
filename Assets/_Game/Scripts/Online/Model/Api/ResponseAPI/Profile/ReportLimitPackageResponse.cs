using System.Collections.Generic;

namespace Online.Model.ResponseAPI.Profile
{
	public class ReportLimitPackageResponse : BaseResponse
	{
		public List<LimitPackageModel> LimitPackages { get; set; }
	}
}