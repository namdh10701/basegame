using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class CannonTable : DataTable<CannonTableRecord>
    {
        public CannonTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class CannonTableRecord
    {
        
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string OperationType { get; set; }

        [Index(2)]
        public string Shape { get; set; }

        [Index(3)]
        [TypeConverter(typeof(RarityConverter))]
        public Rarity Rarity { get; set; }

        [Index(4)]
        [Default(0)]
        public int RarityLevel { get; set; }

        [Index(5)]
        public string Name { get; set; }

        [Index(6)]
        public string DefaultRarity { get; set; }

        [Index(7)]
        [Default(0)]
        public float Hp { get; set; }

        [Index(8)]
        [Default(0)]
        public float Attack { get; set; }

        [Index(9)]
        [Default(0)]
        public float AttackSpeed { get; set; }

        [Index(10)]
        [Default(0)]
        public float Accuracy { get; set; }

        [Index(11)]
        [Default(0)]
        public float CritChance { get; set; }

        [Index(12)]
        [Default(0)]
        public float CritDamage { get; set; }

        [Index(13)]
        [Default(0)]
        public float Range { get; set; }

        [Index(14)]
        [Default(0)]
        public float Skill { get; set; }

        [Index(15)]
        [Default(0)]
        public float PrimaryProjectDmg { get; set; }

        [Index(16)]
        [Default(0)]
        public float SecondaryProjectDmg { get; set; }

        [Index(17)]
        [Default(0)]
        public float ProjectCount { get; set; }

        [Index(18)]
        [Default(0)]
        public float Angle { get; set; }
    }
}