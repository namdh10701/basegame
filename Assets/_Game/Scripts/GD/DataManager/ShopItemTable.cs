using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopItemTable : DataTable<ShopItemTableRecord>
    {
        public List<string> GetRarityById(string id)
        {
            var types = new List<string>();
            foreach (var record in Records)
            {
                if (record.ItemId == id)
                {
                    types.Add(record.Type);
                }
            }
            return types;
        }

        public List<int> GetWeightRarityById(string id)
        {
            var weights = new List<int>();
            foreach (var record in Records)
            {
                if (record.ItemId == id)
                {
                    weights.Add(record.Weight);
                }
            }
            return weights;
        }

        public int GetAmountById(string id)
        {
            foreach (var record in Records)
            {
                if (record.ItemId == id)
                {
                    return record.ItemAmount;
                }

            }
            return -1;
        }

        public ShopItemTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShopItemTableRecord : DataTableRecord
    {
        [Index(0)]
        public string ItemId { get; set; }

        [Index(1)]
        public string Type { get; set; }

        [Index(2)]
        [Default(0)]
        public int ItemAmount { get; set; }

        [Index(3)]
        [Default(0)]
        public int Weight { get; set; }

        public override object GetId()
        {
            return ItemId;
        }
    }
}