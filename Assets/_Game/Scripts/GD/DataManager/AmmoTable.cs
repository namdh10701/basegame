using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AmmoTable : LocalDataTable<AmmoTableRecord>
    {
        public AmmoTable(string fileName) : base(fileName)
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
                if (record.OperationType == name && record.Rarity.ToString() == rarity)
                    return record.Id;
            }
            return null;

        }
        public AmmoTableRecord GetAmmoTableRecordById(string id)
        {
            foreach (var record in Records)
            {
                if (record.Id == id)
                    return record;

            }
            return null;

        }

        public DataTableRecord GetNextTableRecord(Rarity rarity, string id)
        {
            int index = Records.FindIndex(r => r.Rarity == rarity && r.Id == id);

            if (index == -1 || index + 1 >= Records.Count)
            {
                return null;
            }

            return Records[index + 1];
        }

        public (string, string, string) GetDataSkillDefault(string operationType, Rarity rarity, string rarityLevel)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == operationType && record.Rarity == rarity && record.RarityLevel.ToString() == rarityLevel)
                    return (record.OperationType, record.Skill_Desc, record.Skill_Name);
            }
            return (null, null, null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AmmoTableRecord : DataTableRecord
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
        [Stat("Hp")]
        public float Hp { get; set; }

        [JsonProperty("energy_cost")]
        public float EnergyCost { get; set; }

        [JsonProperty("magazine_size")]
        [Stat("MagazineSize")]
        public float MagazineSize { get; set; }

        [JsonProperty("ammo_attack")]
        [Stat("AmmoAttack")]
        public float AmmoAttack { get; set; }

        [JsonProperty("attack_aoe")]
        [Stat("AttackAoe")]
        public float AttackAoe { get; set; }

        [JsonProperty("armor_pen")]
        [Stat("ArmorPen")]
        public float ArmorPen { get; set; }

        [JsonProperty("project_piercing")]
        public float ProjectPiercing { get; set; }

        [JsonProperty("project_speed")]
        public float ProjectSpeed { get; set; }

        [JsonProperty("ammo_accuracy")]
        [Stat("AmmoAccuracy")]
        public float AmmoAccuracy { get; set; }

        [JsonProperty("ammo_crit_chance")]
        [Stat("AmmoCritChance")]
        public float AmmoCritChance { get; set; }

        [JsonProperty("ammo_crit_damage")]
        [Stat("AmmoCritDamage")]
        public float AmmoCritDamage { get; set; }

        [JsonProperty("trigger_prob")]
        public float TriggerProb { get; set; }

        [JsonProperty("duration")]
        [Stat("Duration")]
        public float Duration { get; set; }

        [JsonProperty("speed_modifier")]
        [Stat("SpeedModifer")]
        public float SpeedModifer { get; set; }

        [JsonProperty("dps")]
        [Stat("Dps")]
        public float Dps { get; set; }

        [JsonProperty("pierc_count")]
        [Stat("PiercCount")]
        public float PiercCount { get; set; }

        [JsonProperty("hp_threshold")]
        [Stat("HpThreshold")]
        public float HpThreshold { get; set; }

        [JsonProperty("skill_name")]
        public string Skill_Name { get; set; }

        [JsonProperty("skill_desc")]
        public string Skill_Desc { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        public override object GetId()
        {
            return Id;
        }

        public string DefaultRarity => Rarity.ToString() + RarityLevel;

        public string Slot
        {
            get
            {
                var parts = Shape.Split("x");
                return (int.Parse(parts[0]) * int.Parse(parts[1])).ToString();
            }
        }
    }
}