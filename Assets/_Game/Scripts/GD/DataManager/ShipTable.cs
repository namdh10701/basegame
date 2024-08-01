using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipTable : LocalDataTable<ShipTableRecord>
    {
        public ShipTable(string fileName) : base(fileName)
        {
        }
        
        public DataTableRecord GetDataTableRecord(string id, string name)
        {
            foreach (var record in Records)
            {
                if (record.Id == id && record.Name == name)
                    return record;
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShipTableRecord : DataTableRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hp")]
        public float Hp { get; set; }

        [JsonProperty("block_chance")]
        public float BlockChance { get; set; }

        [JsonProperty("max_mana")]
        public float MaxMana { get; set; }

        [JsonProperty("mana_regen_rate")]
        public float ManaRegenRate { get; set; }

        [JsonProperty("cannon_limit")]
        public float CannonLimit { get; set; }

        [JsonProperty("ammo_limit")]
        public float AmmoLimit { get; set; }

        public override object GetId()
        {
            return Id;
        }
    }
}