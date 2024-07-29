using System;
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

        public string GetSlotByName(string name)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name)
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

        public DataTableRecord GetDataTableRecord(string name, string defaultRarity)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name && record.DefaultRarity == defaultRarity)
                    return record;
            }
            return null;
        }

        public (string, string) GetDataSkillDefault(string name, string defaultRarity)
        {
            foreach (var record in Records)
            {
                if (record.OperationType == name && record.DefaultRarity == defaultRarity)
                    return (record.OperationType, record.Skill_Desc);
            }
            return (null, null);
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
        [Stat("HP")]
        public float Hp { get; set; }

        [Index(8)]
        [Default(0)]
        [Stat("Attack")]
        public float Attack { get; set; }

        [Index(9)]
        [Default(0)]
        [Stat("AttackSpeed")]
        public float AttackSpeed { get; set; }

        [Index(10)]
        [Default(0)]
        [Stat("Accuracy")]
        public float Accuracy { get; set; }

        [Index(11)]
        [Default(0)]
        [Stat("CritChance")]
        public float CritChance { get; set; }

        [Index(12)]
        [Default(0)]
        [Stat("CritDamage")]
        public float CritDamage { get; set; }

        [Index(13)]
        [Default(0)]
        [Stat("Range")]
        public float Range { get; set; }

        [Index(14)]
        [Default(0)]
        [Stat("Skill")]
        public float Skill { get; set; }

        [Index(15)]
        [Default(0)]
        [Stat("PrimaryProjectDmg")]
        public float PrimaryProjectDmg { get; set; }

        [Index(16)]
        [Default(0)]
        [Stat("SecondaryProjectDmg")]
        public float SecondaryProjectDmg { get; set; }

        [Index(17)]
        [Default(0)]
        [Stat("ProjectCount")]
        public float ProjectCount { get; set; }

        [Index(18)]
        [Default(0)]
        [Stat("Angle")]
        public float Angle { get; set; }

        [Index(19)]
        [Default(false)]
        [BooleanFalseValues("0", "false")]
        [BooleanTrueValues("1", "true")]
        public bool Enable { get; set; }

        [Index(21)]
        [Default(0)]
        [Stat("Skill_Name")]
        public string Skill_Name { get; set; }

        [Index(22)]
        [Default(0)]
        [Stat("Skill_Desc")]
        public string Skill_Desc { get; set; }




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