namespace _Base.Scripts.JsonAdapter
{
    using System;
    using Newtonsoft.Json;
    using UnityEngine;

    public class Vector2IntConverter : JsonConverter<Vector2Int>
    {
        public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            int x = 0;
            int y = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = (string)reader.Value;
                    if (!reader.Read())
                        continue;

                    switch (propertyName)
                    {
                        case "x":
                            x = reader.Value != null ? Convert.ToInt32(reader.Value) : 0;
                            break;
                        case "y":
                            y = reader.Value != null ? Convert.ToInt32(reader.Value) : 0;
                            break;
                    }
                }

                if (reader.TokenType == JsonToken.EndObject)
                    break;
            }

            return new Vector2Int(x, y);
        }
    }
}