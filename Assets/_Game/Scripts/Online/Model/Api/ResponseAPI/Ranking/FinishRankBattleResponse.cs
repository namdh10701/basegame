using System.Collections.Generic;
using PlayFab.ClientModels;

namespace Online.Model.ResponseAPI.Ranking
{
	public class FinishRankBattleResponse : BaseResponse
	{
		public int Damage { get; set; } = 0;
		public Dictionary<string, object> Data { get; set; } = new();
		public Dictionary<string, int> VirtualCurrency { get; set; } = new();
		public List<ItemInstance> Items { get; set; } = new();
	}
}