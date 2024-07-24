using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeItemTable : DataTable<TalentTreeItemTableRecord>
    {
        public TalentTreeItemTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeItemTableRecord: DataTableRecord
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        [Default(0)]
        [Stat("Attack")]
        public float Atk { get; set; }

        [Index(2)]
        [Default(0)]
        [Stat("HP")]
        public float Hp { get; set; }

        [Index(3)]
        [Default(0)]
        [Stat("Max MP")]
        public float MaxMana { get; set; }

        [Index(4)]
        [Default(0)]
        [Stat("MP Regen")]
        public float ManaRegen { get; set; }

        [Index(5)]
        [Default(0)]
        [Stat("Ship slot")]
        public float ShipSlot { get; set; }

        [Index(6)]
        [Default(0)]
        [Stat("Gold earning", true)]
        public float GoldEarning { get; set; }

        [Index(7)]
        [Default(0)]
        [Stat("Crit change", true)]
        public float CritChance { get; set; }

        [Index(8)]
        [Default(0)]
        [Stat("Crit damage", true)]
        public float CritDmg { get; set; }
        
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