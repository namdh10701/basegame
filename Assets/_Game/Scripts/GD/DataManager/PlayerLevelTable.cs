using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerLevelTable : DataTable<PlayerLevelTableRecord>
    {
        public PlayerLevelTable(string fileName) : base(fileName)
        {
        }

        public int FindNextLevelExp(int currentLevel)
        {
            var nextRec = FindById(currentLevel + 1);

            if (nextRec == null)
            {
                return -1;
            }

            return nextRec.RequiredExp;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayerLevelTableRecord : DataTableRecord
    {
        // [JsonProperty("level")]
        [Index(0)]
        public int Level { get; set; }

        // [JsonProperty("exp_require")]
        [Index(1)]
        public int RequiredExp { get; set; }

        public override object GetId()
        {
            return Level;
        }
    }
}