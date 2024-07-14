using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace _Game.Scripts.GD.DataManager
{
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

    public enum ShopType
    {
        Gacha,
        Gem,
        // Pirate,
        Other
    }

    public enum PackSize
    {
        Small,
        Medium,
        Big,
        Other
    }

    public class ShopDataListingRecord
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
}