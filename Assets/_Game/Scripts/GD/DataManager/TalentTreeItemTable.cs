using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeItemTable : LocalDataTable<TalentTreeItemTableRecord>
    {
        public TalentTreeItemTable(string fileName = null) : base(fileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeItemTableRecord: DataTableRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("atk")]
        public float Atk { get; set; }

        [JsonProperty("hp")]
        public float Hp { get; set; }

        [JsonProperty("max_mana")]
        public float MaxMana { get; set; }

        [JsonProperty("mana_regen")]
        public float ManaRegen { get; set; }

        [JsonProperty("ship_slot")]
        public float ShipSlot { get; set; }

        [JsonProperty("gold_earning")]
        public float GoldEarning { get; set; }

        [JsonProperty("crit_chance")]
        public float CritChance { get; set; }

        [JsonProperty("crit_dmg")]
        public float CritDmg { get; set; }

        [JsonProperty("luck")]
        public float Luck { get; set; }
        
        public override object GetId()
        {
            return Id;
        }

        public ItemInfo GetInfo()
        {
            var props = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(v =>
                v.GetCustomAttributes(typeof(StatAttribute), false).Count() == 1);

            var prop = props.FirstOrDefault(v => ((float)v.GetValue(this)) > 0f);

            if (prop == null)
            {
                return null;
            }

            var stat = prop.GetCustomAttributes<StatAttribute>().First();
            var name = stat.Name;

            
            var statValue = prop.GetValue(this);
            
            var value = statValue.ToString();

            if (statValue is float && stat.IsPercentage)
            {
                value = (((float)statValue) * 100).ToString(CultureInfo.InvariantCulture) + "%";
            }
            
            return new ItemInfo()
            {
                Name = name,
                Value = value,
            };
        }

        public class ItemInfo
        {
            public string Name;
            public string Value;
        }
    }
}