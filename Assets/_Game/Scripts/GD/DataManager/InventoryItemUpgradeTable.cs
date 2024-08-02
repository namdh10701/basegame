
using Newtonsoft.Json;
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
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("gold")]
        public int Gold { get; set; }

        [JsonProperty("blueprint")]
        public int Blueprint { get; set; }

        [JsonProperty("value")]
        public float Effect { get; set; }

        public override object GetId()
        {
            return Level;
        }
    }
}