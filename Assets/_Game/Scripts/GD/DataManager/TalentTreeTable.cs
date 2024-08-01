using System.Linq;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeTable : LocalDataTable<TalentTreeTableRecord>
    {
        public TalentTreeTable(string fileName) : base(fileName)
        {
        }

        public int GetLevelRecordCount(int level)
        {
            return Records.Count(v => v.Level == level);
        }
        
        public TalentTreeTableRecord FindByLevel(int level)
        {
            return Records.FirstOrDefault(v => v.Level == level);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeTableRecord: DataTableRecord
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("cost")]
        public int Cost { get; set; }
        
        [JsonProperty("stat_id")]
        public string ItemId { get; set; }
        
        public override object GetId()
        {
            return Id;
        }
    }
}