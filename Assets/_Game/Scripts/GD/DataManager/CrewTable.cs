using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class CrewTable : DataTable<CrewTableRecord>
    {
        public CrewTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
            
        }
        
        public CrewTableRecord FindById(string id)
        {
            return Records.Find(v => v.Id == id);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class CrewTableRecord
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(2)]
        public string OperationType { get; set; }

        [Index(3)]
        public string Shape { get; set; }

        [Index(4)]
        [TypeConverter(typeof(RarityConverter))]
        public Rarity Rarity { get; set; }

        [Index(5)]
        [Default(0)]
        public float MoveSpeed { get; set; }

        [Index(6)]
        [Default(0)]
        public float RepairSpeed { get; set; }
    }
}