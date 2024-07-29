namespace Online.Model.GooglePurchase
{
	public class JsonData {
		// JSON Fields, ! Case-sensitive

		public string orderId;
		public string packageName;
		public string productId;
		public long purchaseTime;
		public int purchaseState;
		public string purchaseToken;
	}

	public class PayloadData {
		public JsonData JsonData;

		// JSON Fields, ! Case-sensitive
		public string signature;
		public string json;

		public static PayloadData FromJson(string json) {
			try
			{
				var payload = Newtonsoft.Json.JsonConvert.DeserializeObject<PayloadData>(json);
				payload.JsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonData>(payload.json);
				return payload;
			}
			catch (System.Exception e)
			{
				return new PayloadData()
				{
					signature = "signature",
					JsonData = new JsonData()
					{
						orderId = "orderId",
						packageName = "package_name",
						productId = "product_id",
						purchaseTime = 1,
						purchaseToken = "token",
						purchaseState = 0
					}
				};
			}
		}
	}
	
	public class GooglePurchase
	{
		public PayloadData PayloadData;
		
		public string Store;
		public string TransactionID;
		public string Payload;

		public static GooglePurchase FromJson(string json)
		{
			var purchase = Newtonsoft.Json.JsonConvert.DeserializeObject<GooglePurchase>(json);
			purchase.PayloadData = PayloadData.FromJson(purchase.Payload);
			return purchase;
		}
	}
}