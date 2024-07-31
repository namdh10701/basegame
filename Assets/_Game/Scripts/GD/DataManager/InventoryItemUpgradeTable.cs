using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryItemUpgradeTable : LocalDataTable<InventoryItemUpgradeTableRecord>
    {
        public InventoryItemUpgradeTable(string fileName = null) : base(fileName)
        {
        }

        public InventoryItemUpgradeTableRecord GetGoldAndBlueprintByLevel(int level)
        {
            foreach (var record in Records)
            {
                if (record.Level == level)
                    return record;
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InventoryItemUpgradeTableRecord : DataTableRecord
    {
        [Index(0)]
        [Default(0)]
        public int Level { get; set; }

        [Index(1)]
        [Default(0)]
        public int Gold { get; set; }

        [Index(2)]
        [Default(0)]
        public int Blueprint { get; set; }

        [Index(3)]
        [Default(0)]
        public float Effect { get; set; }

        public override object GetId()
        {
            return Level;
        }
    }
}