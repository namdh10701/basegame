using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AmmoTable : DataTable<AmmoTableRecord>
    {
        public AmmoTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }

        public string GetShapeByName(string name)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name)
                    return record.Shape;
            }
            return null;
        }

        public string GetIdByNameAndRarityDefault(string name, string rarity)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name && record.DefaultRarity == rarity)
                    return record.Id;
            }
            return null;

        }
        public DataTableRecord GetDataTableRecord(string name, string defaultRarity)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name && record.DefaultRarity == defaultRarity)
                    return record;
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AmmoTableRecord : DataTableRecord
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
        [Stat("Hp")]
        public float Hp { get; set; }

        [Index(8)]
        [Default(0)]
        [Stat("EnergyCost")]
        public float EnergyCost { get; set; }

        [Index(9)]
        [Default(0)]

        [Stat("MagazineSize")]
        public float MagazineSize { get; set; }

        [Index(10)]
        [Default(0)]
        [Stat("AmmoAttack")]
        public float AmmoAttack { get; set; }

        [Index(11)]
        [Default(0)]
        [Stat("AttackAoe")]
        public float AttackAoe { get; set; }

        [Index(12)]
        [Default(0)]
        [Stat("ArmorPen")]
        public float ArmorPen { get; set; }

        [Index(13)]
        [Default(0)]
        [Stat("ProjectPiercing")]
        public float ProjectPiercing { get; set; }

        [Index(14)]
        [Default(0)]
        [Stat("ProjectSpeed")]
        public float ProjectSpeed { get; set; }

        [Index(15)]
        [Default(0)]
        [Stat("AmmoAccuracy")]
        public float AmmoAccuracy { get; set; }

        [Index(16)]
        [Default(0)]
        [Stat("AmmoCritChance")]
        public float AmmoCritChance { get; set; }

        [Index(17)]
        [Default(0)]
        [Stat("AmmoCritDamage")]
        public float AmmoCritDamage { get; set; }

        [Index(18)]
        [Default(0)]
        [Stat("TriggerProb")]
        public float TriggerProb { get; set; }

        [Index(19)]
        [Default(0)]
        [Stat("Duration")]
        public float Duration { get; set; }

        [Index(20)]
        [Default(0)]
        [Stat("SpeedModifer")]
        public float SpeedModifer { get; set; }

        [Index(21)]
        [Default(0)]
        [Stat("Dps")]
        public float Dps { get; set; }

        [Index(22)]
        [Default(0)]
        [Stat("PiercCount")]
        public float PiercCount { get; set; }

        [Index(23)]
        [Default(0)]
        [Stat("HpThreshold")]
        public float HpThreshold { get; set; }

        [Index(24)]
        public string Type { get; set; }

        public override object GetId()
        {
            return Id;
        }
    }
}