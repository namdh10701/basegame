using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipTable : DataTable<ShipTableRecord>
    {
        private List<ShipTableRecord> _records = new();

        protected override void HandleLoadedRecords(List<ShipTableRecord> rawRecords)
        {
            _records = rawRecords;
        }

        public List<ShipTableRecord> GetData()
        {
            return _records;
        }

        public ShipTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ShipTableRecord
    {
        [Index(0)]
        public string Id { get; set; }
        
        [Index(1)]
        public string Name { get; set; }
        
        [Index(2)]
        [Default(0)]
        public float Hp { get; set; }
        
        [Index(3)]
        [Default(0)]
        public float BlockChance { get; set; }
        
        [Index(4)]
        [Default(0)]
        public float MaxMana { get; set; }
        
        [Index(5)]
        [Default(0)]
        public float ManaRegenRate { get; set; }
        
        [Index(6)]
        [Default(0)]
        public float CannonLimit { get; set; }
        
        [Index(7)]
        [Default(0)]
        public float AmmoLimit { get; set; }
    }
}