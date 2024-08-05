using System;
using Newtonsoft.Json;

namespace Online.Model
{
	public class LimitPackageModel
	{
		[JsonProperty("Id")]
		public string Id { get; set; }
		
		[JsonProperty("LastTime")]
		public long LastTime { get; set; }

		[JsonProperty("Count")]
		public int Count { get; set; }
	}
}