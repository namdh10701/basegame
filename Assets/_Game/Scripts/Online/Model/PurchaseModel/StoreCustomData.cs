using Newtonsoft.Json;
using Online.Enum;
namespace Online.Model.GooglePurchase
{
	public class StoreCustomData
	{
		[JsonProperty("Limit")]
		public EItemLimit Limit { get; set; }
		
		[JsonProperty("Count")]
		public int Count { get; set; }
		
		[JsonProperty("Countdown")]
		public float Countdown { get; set; }
	}
}