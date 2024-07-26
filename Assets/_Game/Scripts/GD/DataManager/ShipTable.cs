using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipTable : DataTable<ShipTableRecord>
    {
        public ShipTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
        public DataTableRecord GetDataTableRecord(string id, string name)
        {
            foreach (var record in Records)
            {
                if (record.Id == id && record.Name == name)
                    return record;
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShipTableRecord : DataTableRecord
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(2)]
        [Default(0)]
        public float Hp { get; set; }

        [Index(3)]
        [Default(0)]
        public float BlockChance { get; set; }

        [Index(4)]
        [Default(0)]
        public float MaxMana { get; set; }

        [Index(5)]
        [Default(0)]
        public float ManaRegenRate { get; set; }

        [Index(6)]
        [Default(0)]
        public float CannonLimit { get; set; }

        [Index(7)]
        [Default(0)]
        public float AmmoLimit { get; set; }

        public override object GetId()
        {
            return Id;
        }
    }
}