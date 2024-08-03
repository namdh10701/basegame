using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class CannonTable : LocalDataTable<CannonTableRecord>
    {
        public CannonTable(string name) : base(name)
        {
        }

        public string GetSlotByName(string id)
        {
            foreach (var record in Records)
            {
                if (record.Id == id)
                    return record.Slot;
            }
            return null;
        }

        public string GetIdByNameAndRarityDefault(string name, string defaultRarity)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name && record.DefaultRarity == defaultRarity)
                    return record.Id;
            }
            return null;
        }

        public float GetHPById(string id)
        {
            foreach (var record in Records)
            {
                if (record.Id == id)
                    return record.Hp;
            }
            return -1;
        }

        // public (string, string, string) GetDataSkillDefault(string operationType, Rarity rarity, string rarityLevel)
        // {
        //     foreach (var record in Records)
        //     {
        //         if (record.OperationType == operationType && record.Rarity == rarity && record.RarityLevel.ToString() == rarityLevel)
        //             return (record.OperationType, record.Skill_Desc, record.Skill_Name);
        //     }
        //     return (null, null, null);
        // }

        public (string, string, string) GetDataSkillDefault(string id)
        {
            foreach (var record in Records)
            {
                if (record.Id == id)
                    return (record.OperationType, record.Skill_Desc, record.Skill_Name);
            }
            return (null, null, null);
        }

        public DataTableRecord GetNextTableRecord(Rarity rarity, string operationType, string rarityLevel)
        {
            int index = Records.FindIndex(r => r.Rarity == rarity && r.OperationType == operationType && r.RarityLevel.ToString() == rarityLevel);

            if (index == -1 || index + 1 >= Records.Count)
            {
                return null;
            }

            return Records[index + 1];
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class CannonTableRecord : DataTableRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }

        [JsonProperty("shape")]
        public string Shape { get; set; }

        [JsonProperty("rarity")]
        public Rarity Rarity { get; set; }

        [JsonProperty("rarity_level")]
        public int RarityLevel { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hp")]
        [Stat("HP")]
        public float Hp { get; set; }

        [JsonProperty("attack")]
        [Stat("Attack")]
        public float Attack { get; set; }

        [JsonProperty("attack_speed")]
        [Stat("AttackSpeed")]
        public float AttackSpeed { get; set; }

        [JsonProperty("accuracy")]
        [Stat("Accuracy")]
        public float Accuracy { get; set; }

        [JsonProperty("crit_chance")]
        [Stat("CritChance")]
        public float CritChance { get; set; }

        [JsonProperty("crit_damage")]
        [Stat("CritDamage")]
        public float CritDamage { get; set; }

        [JsonProperty("range")]
        [Stat("Range")]
        public float Range { get; set; }

        [JsonProperty("skill")]
        public float Skill { get; set; }

        [JsonProperty("primary_project_dmg")]
        [Stat("PrimaryProjectDmg")]
        public float PrimaryProjectDmg { get; set; }

        [JsonProperty("secondary_project_dmg")]
        [Stat("SecondaryProjectDmg")]
        public float SecondaryProjectDmg { get; set; }

        [JsonProperty("project_count")]
        public float ProjectCount { get; set; }

        [JsonProperty("angle")]
        [Stat("Angle")]
        public float Angle { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("skill_name")]
        public string Skill_Name { get; set; }

        [JsonProperty("skill_desc")]
        public string Skill_Desc { get; set; }

        public string DefaultRarity => Rarity.ToString() + RarityLevel;

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