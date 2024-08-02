using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class MonsterTable : LocalDataTable<MonsterTableRecord>
    {
        public MonsterTable(string fileName) : base(fileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class MonsterTableRecord: DataTableRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("power_number")]
        public float PowerNumber { get; set; }
        
        [JsonProperty("fever_point")]
        public float FeverPoint { get; set; }
        
        [JsonProperty("attack")]
        public float Attack { get; set; }
        
        [JsonProperty("attack_speed")]
        public float AttackSpeed { get; set; }
        
        [JsonProperty("hp")]
        public float Hp { get; set; }
        
        [JsonProperty("block_chance")]
        public float BlockChance { get; set; }
        
        [JsonProperty("attack_range")]
        public float AttackRange { get; set; }
        
        public override object GetId()
        {
            return Id;
        }
    }
}