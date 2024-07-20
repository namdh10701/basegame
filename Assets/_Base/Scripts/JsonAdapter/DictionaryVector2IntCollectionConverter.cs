using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.SaveLoad;
using Newtonsoft.Json;
using UnityEngine;

namespace _Base.Scripts.JsonAdapter
{

    public class DictionaryVector2IntICollectionConverter : JsonConverter<Dictionary<Vector2Int, ItemData>>
    {
        public override void WriteJson(JsonWriter writer, Dictionary<Vector2Int, ItemData> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var kvp in value)
            {
                writer.WritePropertyName($"{kvp.Key.x},{kvp.Key.y}");
                // writer.WritePropertyName(JsonConvert.SerializeObject(kvp.Key, new Vector2IntConverter()));
                serializer.Serialize(writer, kvp.Value);
            }
            writer.WriteEndObject();
        }

        public override Dictionary<Vector2Int, ItemData> ReadJson(JsonReader reader, Type ItemDataType, Dictionary<Vector2Int, ItemData> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var dictionary = new Dictionary<Vector2Int, ItemData>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    // var key = JsonConvert.DeserializeObject<Vector2Int>((string)reader.Value, new Vector2IntConverter());
                    var keyParts = ((string)reader.Value).Split(",");
                    var key = new Vector2Int(int.Parse(keyParts[0]), int.Parse(keyParts[1]));
                    reader.Read();
                    var value = serializer.Deserialize<ItemData>(reader);
                    dictionary[key] = value;
                }

                if (reader.TokenType == JsonToken.EndObject)
                    break;
            }

            return dictionary;
        }
    }
}