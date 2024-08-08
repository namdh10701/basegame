using System.Collections.Generic;
using Online.Enum;
using PlayFab.ClientModels;

namespace Online.Model.ResponseAPI.Ranking
{
	public class ClaimSeasonRewardResponse : BaseResponse
	{
		public ERank NewRank { get; set; }
		public Dictionary<string, int> VirtualCurrency { get; set; }
		public List<ItemInstance> Items { get; set; }
	}
}