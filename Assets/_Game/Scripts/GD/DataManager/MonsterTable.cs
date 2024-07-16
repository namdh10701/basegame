using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class MonsterTable : DataTable<MonsterTableRecord>
    {
        private List<MonsterTableRecord> _records = new();

        protected override void HandleLoadedRecords(List<MonsterTableRecord> rawRecords)
        {
            _records = rawRecords;
        }

        public List<MonsterTableRecord> GetData()
        {
            return _records;
        }

        public MonsterTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class MonsterTableRecord
    {
        [Index(0)]
        public string Id { get; set; }
        
        [Index(1)]
        public string Name { get; set; }
        
        [Index(2)]
        [Default(0)]
        public float PowerNumber { get; set; }
        
        [Index(3)]
        [Default(0)]
        public float FeverPoint { get; set; }
        
        [Index(4)]
        [Default(0)]
        public float Attack { get; set; }
        
        [Index(5)]
        [Default(0)]
        public float AttackSpeed { get; set; }
        
        [Index(6)]
        [Default(0)]
        public float Hp { get; set; }
        
        [Index(7)]
        [Default(0)]
        public float BlockChance { get; set; }
        
        [Index(8)]
        [Default(0)]
        public float AttackRange { get; set; }
    }
}