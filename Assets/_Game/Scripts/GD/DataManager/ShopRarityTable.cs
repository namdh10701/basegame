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
        public List<string> GetDataNames(string gachaType, string rarity)
        {
            var names = new List<string>();
            foreach (var record in Records)
            {
                if (record.Type == gachaType && record.Rarity.ToString() == rarity)
                {
                    names.Add(record.Name);
                }
            }
            return names;
            // return Records.Where(item => item.Type == gachaType).Select(item => item.Name).ToList();
        }

        public List<int> GetWeights(string gachaType, string rarity)
        {
            var weigts = new List<int>();
            foreach (var record in Records)
            {
                if (record.Type == gachaType && record.Rarity.ToString() == rarity)
                {
                    weigts.Add(record.Weight);
                }
            }
            return weigts;
        }

        public string GetIdByNameAndRarity(string itemName, string rarity)
        {
            return Records.Where(item => item.Name == itemName && item.Rarity.ToString() == rarity).Select(item => item.Id).FirstOrDefault();
        }

        public ShopRarityTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShopRarityTableRecord : DataTableRecord
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(2)]
        public string Type { get; set; }

        [Index(3)]
        // public string DefaultRarity { get; set; }
        [TypeConverter(typeof(RarityConverter))]
        public Rarity Rarity { get; set; }

        [Index(4)]
        [Default(0)]
        public int Weight { get; set; }

        public override object GetId()
        {
            return Id;
        }
    }
}