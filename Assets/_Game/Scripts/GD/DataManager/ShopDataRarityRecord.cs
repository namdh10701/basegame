using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    public class ShopDataRarityRecord
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