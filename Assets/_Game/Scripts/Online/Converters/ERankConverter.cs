using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Online.Enum;

namespace Online.Converters
{
	public class ERankConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ERank);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var token = JToken.Load(reader);
			return System.Enum.Parse(typeof(ERank), token.ToString());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}
	}
}