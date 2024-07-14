using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GD.DataManager;
using Fusion;

namespace _Game.Scripts.GD
{
    public class ShopDataListing : DataManager<ShopDataListingRecord>
    {
        public static ShopDataListing Instance = new();

        private Dictionary<ShopType, List<ShopDataListingRecord>> _shopData = new()
        {
            { ShopType.Gacha, new () },
            { ShopType.Gem, new () },
            { ShopType.Other, new () },
        };

        protected override string DownloadUrl
            => "https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=1364617036#gid=1364617036";
        protected override string DataFileName => "ShopData_Listing";
        protected override void HandleLoadedRecords(List<ShopDataListingRecord> rawRecords)
        {
            foreach (var record in rawRecords)
            {
                _shopData[record.ShopType].Add(record);
            }
        }

        public List<ShopDataListingRecord> GetData(ShopType shopType)
        {
            return _shopData[shopType];
        }

        public (string, PackSize) GetPriceById(ShopType shopType, string id)
        {
            var item = GetData(shopType).FirstOrDefault(x => x.ItemId == id);
            return (item?.PriceAmount.ToString(), item.PackSize);
        }
    }
}