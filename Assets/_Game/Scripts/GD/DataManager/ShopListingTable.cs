using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

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
            { ShopType.Other, new () },
        };
        
        protected override void HandleLoadedRecords(List<ShopListingTableRecord> rawRecords)
        {
            foreach (var record in rawRecords)
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
    public enum ShopType
    {
        Gacha,
        Gem,
        Other
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PackSize
    {
        Small,
        Medium,
        Big,
        Other
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShopListingTableRecord
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
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ShopTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (Enum.TryParse(typeof(ShopType), text, true, out var result))
            {
                return result;
            }
            return ShopType.Other;
            throw new ArgumentException($"Cannot convert '{text}' to {typeof(ShopType)}");
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value?.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PackSizeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (Enum.TryParse(typeof(PackSize), text, true, out var result))
            {
                return result;
            }

            return PackSize.Other;
            throw new ArgumentException($"Cannot convert '{text}' to {typeof(ShopType)}");
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value?.ToString();
        }
    }
}