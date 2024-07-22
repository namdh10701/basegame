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
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class CrewTableRecord: DataTableRecord
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

        [Index(7)]
        [Default(0)]
        public float FeverTimeProb { get; set; }

        [Index(8)]
        [Default(0)]
        public float GoldIncome { get; set; }

        [Index(9)]
        [Default(0)]
        public float StatusReduce { get; set; }

        [Index(10)]
        [Default(0)]
        public float ZeroManaCost { get; set; }

        [Index(11)]
        [Default(0)]
        public float Luck { get; set; }

        [Index(12)]
        [Default(0)]
        public float BonusAmmo { get; set; }

        [Index(13)]
        public string SkillDesc1 { get; set; }

        [Index(14)]
        public string SkillDesc2 { get; set; }

        [Index(15)]
        public string SkillDesc3 { get; set; }
        
        public override object GetId()
        {
            return Id;
        }
    }
}