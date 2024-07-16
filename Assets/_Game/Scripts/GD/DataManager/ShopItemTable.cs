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
        private List<ShopItemTableRecord> _records = new();

        protected override void HandleLoadedRecords(List<ShopItemTableRecord> rawRecords)
        {
            _records = rawRecords;
        }

        public List<ShopItemTableRecord> GetData()
        {
            return _records;
        }

        public List<string> GetRarityById(string id)
        {
            return _records.Where(item => item.ItemId == id).Select(item => item.Type).ToList();
        }

        public List<int> GetWeightRarityById(string id)
        {
            return _records.Where(item => item.ItemId == id).Select(item => item.Weight).ToList();
        }

        public int GetAmountById(string id)
        {
            return _records.Where(item => item.ItemId == id)
                .Select(item => item.ItemAmount)
                .FirstOrDefault();

        }

        public ShopItemTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ShopItemTableRecord
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
    }
}