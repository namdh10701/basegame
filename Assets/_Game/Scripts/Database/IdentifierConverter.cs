using _Base.Scripts.Database;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class IdentifierConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Identifier);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            if (jsonObject["Value"] != null)
            {
                int value = jsonObject["Value"].Value<int>();
                if (jsonObject["GearType"] != null)
                {
                    string gearTypeString = jsonObject["GearType"].Value<string>();
                    if (Enum.TryParse(typeof(GearType), gearTypeString, out object gearTypeObj))
                    {
                        GearType gearType = (GearType)gearTypeObj;
                        return new GearKey(value, gearType);
                    }
                    else
                    {
                        Debug.LogError($"Invalid GearType value: {gearTypeString}");
                        return null;
                    }
                }
                else
                {
                    return new IntKey(value);
                }
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IntKey intKey)
            {
                var jsonObject = new JObject();
                jsonObject.Add("Value", intKey.Value);
                jsonObject.WriteTo(writer);
            }
        }
    }
}