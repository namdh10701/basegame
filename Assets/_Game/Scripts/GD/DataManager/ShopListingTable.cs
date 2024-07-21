using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopListingTable : DataTable<ShopListingTableRecord>
    {
        private Dictionary<ShopType, List<ShopListingTableRecord>> _records = new()
        {
            { ShopType.Gacha, new () },
            { ShopType.Gem, new () },
            { ShopType.Pirate, new () },
            { ShopType.Gold, new () },
            { ShopType.Other, new () },
        };

        public override async Task LoadData()
        {
            await base.LoadData();
            foreach (var record in Records)
            {
                _records[record.ShopType].Add(record);
            }
        }

        public List<ShopListingTableRecord> GetData(ShopType shopType)
        {
            return _records[shopType];
        }

        public (string, PackSize) GetPriceById(ShopType shopType, string id)
        {
            var item = GetData(shopType).FirstOrDefault(x => x.ItemId == id);
            return (item?.PriceAmount.ToString(), item.PackSize);
        }

        public ShopListingTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShopListingTableRecord : DataTableRecord
    {
        [Index(0)]
        public string ItemId { get; set; }

        [Index(1)]
        [TypeConverter(typeof(ShopTypeConverter))]
        public ShopType ShopType { get; set; }

        [Index(2)]
        [TypeConverter(typeof(PackSizeConverter))]
        public PackSize PackSize { get; set; }

        [Index(3)]
        public string GachaType { get; set; }

        [Index(4)]
        public string PriceType { get; set; }

        [Index(5)]
        [Default(0)]
        public float PriceAmount { get; set; }

        [Index(6)]
        public string BuyLimitType { get; set; }

        [Index(7)]
        [Default(0)]
        public int BuyLimitAmount { get; set; }

        [Index(8)]
        public string StartDate { get; set; }

        [Index(9)]
        public string EndDate { get; set; }

        public override object GetId()
        {
            return ItemId;
        }
    }
}