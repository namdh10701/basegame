using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class CrewTable : LocalDataTable<CrewTableRecord>
    {
        public CrewTable(string fileName) : base(fileName)
        {

        }

        public (string, string, string) GetDataSkillDefault(string operationType, string rarity)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == operationType && record.Rarity.ToString() == rarity)
                    return (record.OperationType, record.SkillDesc1, record.Skill_Name_1);
            }
            return (null, null, null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CrewTableRecord : DataTableRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }

        [JsonProperty("shape")]
        public string Shape { get; set; }

        [JsonProperty("rarity")]
        public Rarity Rarity { get; set; }

        [JsonProperty("move_speed")]
        [Stat("MoveSpeed")]
        public float MoveSpeed { get; set; }

        [JsonProperty("repair_speed")]
        [Stat("RepairSpeed")]
        public float RepairSpeed { get; set; }

        [JsonProperty("fever_time_prob")]
        [Stat("FeverTimeProb")]
        public float FeverTimeProb { get; set; }

        [JsonProperty("gold_income")]
        [Stat("GoldIncome")]
        public float GoldIncome { get; set; }

        [JsonProperty("status_reduce")]
        public float StatusReduce { get; set; }

        [JsonProperty("zero_mana_cost")]
        public float ZeroManaCost { get; set; }

        [JsonProperty("luck")]
        [Stat("Luck")]
        public float Luck { get; set; }

        [JsonProperty("bonus_ammo")]
        [Stat("BonusAmmo")]
        public float BonusAmmo { get; set; }

        [JsonProperty("skill_name_1")]
        public string Skill_Name_1 { get; set; }

        [JsonProperty("skill_desc_1")]
        public string SkillDesc1 { get; set; }

        [JsonProperty("skill_name_2")]
        public string Skill_Name_2 { get; set; }

        [JsonProperty("skill_desc_2")]
        public string SkillDesc2 { get; set; }

        [JsonProperty("skill_name_3")]
        public string Skill_Name_3 { get; set; }

        [JsonProperty("skill_desc_3")]
        public string SkillDesc3 { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        public string Slot
        {
            get
            {
                var parts = Shape.Split("x");
                return (int.Parse(parts[0]) * int.Parse(parts[1])).ToString();
            }
        }

        public override object GetId()
        {
            return Id;
        }
    }
}