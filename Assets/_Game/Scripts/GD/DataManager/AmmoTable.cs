using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AmmoTable : DataTable<AmmoTableRecord>
    {
        private List<AmmoTableRecord> _records = new();

        protected override void HandleLoadedRecords(List<AmmoTableRecord> rawRecords)
        {
            _records = rawRecords;
        }

        public List<AmmoTableRecord> GetData()
        {
            return _records;
        }

        public AmmoTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class AmmoTableRecord
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string OperationType { get; set; }

        [Index(2)]
        public string Shape { get; set; }

        [Index(3)]
        public string Rarity { get; set; }

        [Index(4)]
        [Default(0)]
        public float RarityLevel { get; set; }

        [Index(5)]
        public string Name { get; set; }

        [Index(6)]
        public string DefaultRarity { get; set; }

        [Index(7)]
        [Default(0)]
        public float Hp { get; set; }

        [Index(8)]
        [Default(0)]
        public float EnergyCost { get; set; }

        [Index(9)]
        [Default(0)]
        public float MagazineSize { get; set; }

        [Index(10)]
        [Default(0)]
        public float AmmoAttack { get; set; }

        [Index(11)]
        [Default(0)]
        public float AttackAoe { get; set; }

        [Index(12)]
        [Default(0)]
        public float ArmorPen { get; set; }

        [Index(13)]
        [Default(0)]
        public float ProjectPiercing { get; set; }

        [Index(14)]
        [Default(0)]
        public float ProjectSpeed { get; set; }

        [Index(15)]
        [Default(0)]
        public float AmmoAccuracy { get; set; }

        [Index(16)]
        [Default(0)]
        public float AmmoCritChance { get; set; }

        [Index(17)]
        [Default(0)]
        public float AmmoCritDamage { get; set; }

        [Index(18)]
        [Default(0)]
        public float TriggerProb { get; set; }

        [Index(19)]
        [Default(0)]
        public float Duration { get; set; }

        [Index(20)]
        [Default(0)]
        public float SpeedModifer { get; set; }

        [Index(21)]
        [Default(0)]
        public float Dps { get; set; }

        [Index(22)]
        [Default(0)]
        public float PiercCount { get; set; }

        [Index(23)]
        [Default(0)]
        public float HpThreshold { get; set; }

        [Index(24)]
        [Default(0)]
        public float Type { get; set; }
    }
}