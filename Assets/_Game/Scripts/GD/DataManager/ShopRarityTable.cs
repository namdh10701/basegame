using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopRarityTable : DataTable<ShopRarityTableRecord>
    {
        private List<ShopRarityTableRecord> _records = new();

        protected override void HandleLoadedRecords(List<ShopRarityTableRecord> rawRecords)
        {
            _records = rawRecords;
        }

        public List<ShopRarityTableRecord> GetData()
        {
            return _records;
        }

        public List<string> GetDataNames(string gachaType)
        {
            return _records.Where(item => item.Type == gachaType).Select(item => item.Name).ToList();
        }

        public List<int> GetWeights(string gachaType, string rarity)
        {
            return _records.Where(item => item.Type == gachaType && item.DefaultRarity == rarity).Select(item => item.Weight).ToList();
        }

        public string GetIdByNameAndRarity(string itemName, string rarity)
        {
            return _records.Where(item => item.Name == itemName && item.DefaultRarity == rarity).Select(item => item.Id).FirstOrDefault();
        }

        public ShopRarityTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ShopRarityTableRecord
    {
        [Index(0)]
        public string Id { get; set; }
        
        [Index(1)]
        public string Name { get; set; }
        
        [Index(2)]
        public string Type { get; set; }
        
        [Index(3)]
        public string DefaultRarity { get; set; }
        
        [Index(4)]
        [Default(0)]
        public int Weight { get; set; }
    }
}