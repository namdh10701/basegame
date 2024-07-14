using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;

namespace _Game.Scripts.GD
{
    public class ShopDataRarity : DataManager<ShopDataRarityRecord>
    {
        public static ShopDataRarity Instance = new();

        private List<ShopDataRarityRecord> _shopData = new();

        protected override string DownloadUrl
            => "https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=809979341#gid=809979341";
        protected override string DataFileName => "ShopData_Rarity";
        protected override void HandleLoadedRecords(List<ShopDataRarityRecord> rawRecords)
        {
            _shopData = rawRecords;
        }

        public List<ShopDataRarityRecord> GetData()
        {
            return _shopData;
        }

        public List<string> GetDataNames(string gachaType)
        {
            return _shopData.Where(item => item.Type == gachaType).Select(item => item.Name).ToList();
        }

        public List<int> GetWeights(string gachaType, string rarity)
        {
            return _shopData.Where(item => item.Type == gachaType && item.DefaultRarity == rarity).Select(item => item.Weight).ToList();
        }

        public string GetIdByNameAndRarity(string itemName, string rarity)
        {
            return _shopData.Where(item => item.Name == itemName && item.DefaultRarity == rarity).Select(item => item.Id).FirstOrDefault();
        }
    }
}