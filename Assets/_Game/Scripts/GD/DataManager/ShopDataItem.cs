using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;

namespace _Game.Scripts.GD
{
    public class ShopDataItem : DataManager<ShopDataItemRecord>
    {
        public static ShopDataItem Instance = new();

        private List<ShopDataItemRecord> _shopData = new();

        protected override string DownloadUrl
            => "https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=0#gid=0";
        protected override string DataFileName => "ShopData_Item";
        protected override void HandleLoadedRecords(List<ShopDataItemRecord> rawRecords)
        {
            _shopData = rawRecords;
        }

        public List<ShopDataItemRecord> GetData()
        {
            return _shopData;
        }

        public List<string> GetRarityById(string id)
        {
            return _shopData.Where(item => item.ItemId == id).Select(item => item.Type).ToList();
        }

        public List<int> GetWeightRarityById(string id)
        {
            return _shopData.Where(item => item.ItemId == id).Select(item => item.Weight).ToList();
        }

        public int GetAmountById(string id)
        {
            return _shopData.Where(item => item.ItemId == id)
                    .Select(item => item.ItemAmount)
                    .FirstOrDefault();

        }
    }
}