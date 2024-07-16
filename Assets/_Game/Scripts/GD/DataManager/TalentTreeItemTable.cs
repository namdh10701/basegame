using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeItemTable : DataTable<TalentTreeItemTableRecord>
    {
        private List<TalentTreeItemTableRecord> _records = new();

        protected override void HandleLoadedRecords(List<TalentTreeItemTableRecord> rawRecords)
        {
            _records = rawRecords;
        }

        public List<TalentTreeItemTableRecord> GetData()
        {
            return _records;
        }

        public TalentTreeItemTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeItemTableRecord
    {
        [Index(0)]
        [Default(0)]
        public string Id { get; set; }

        [Index(1)]
        [Default(0)]
        public float Atk { get; set; }

        [Index(2)]
        [Default(0)]
        public float Hp { get; set; }

        [Index(3)]
        [Default(0)]
        public float MaxMana { get; set; }

        [Index(4)]
        [Default(0)]
        public float ManaRegen { get; set; }

        [Index(5)]
        [Default(0)]
        public float ShipSlot { get; set; }

        [Index(6)]
        [Default(0)]
        public float GoldEarning { get; set; }

        [Index(7)]
        [Default(0)]
        public float CritChance { get; set; }

        [Index(8)]
        [Default(0)]
        public float CritDmg { get; set; }
    }
}