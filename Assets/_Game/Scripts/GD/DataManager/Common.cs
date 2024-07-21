using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public enum Rarity
    {
        Common,
        Good,
        Rare,
        Epic,
        Legend,
        Other,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ShopType
    {
        Gacha = 0,
        Gem = 1,
        Pirate = 2,
        Gold = 3,
        Other = 4
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
    public class ShopTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return Enum.TryParse(typeof(ShopType), text, true, out var result) ? result : ShopType.Other;
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
            return Enum.TryParse(typeof(PackSize), text, true, out var result) ? result : PackSize.Other;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value?.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RarityConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return Enum.TryParse(typeof(Rarity), text, true, out var result) ? result : Rarity.Other;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value?.ToString();
        }
    }
}