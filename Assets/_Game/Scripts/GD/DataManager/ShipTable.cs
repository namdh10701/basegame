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

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("hp")]
        [Stat("Hp")]
        public float Hp { get; set; }

        [JsonProperty("block_chance")]
        [Stat("BlockChance")]
        public float BlockChance { get; set; }

        [JsonProperty("max_mana")]
        [Stat("MaxMana")]
        public float MaxMana { get; set; }

        [JsonProperty("mana_regen_rate")]
        [Stat("ManaRegenRate")]
        public float ManaRegenRate { get; set; }

        [JsonProperty("cannon_limit")]
        [Stat("CannonLimit")]
        public float CannonLimit { get; set; }

        [JsonProperty("ammo_limit")]
        [Stat("AmmoLimit")]
        public float AmmoLimit { get; set; }

        public override object GetId()
        {
            return Id;
        }
    }
}