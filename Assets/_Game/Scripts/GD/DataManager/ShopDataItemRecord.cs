using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    public class ShopDataItemRecord
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