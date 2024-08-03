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
            var lv = FindById(currentLevel).Level;
            var nextRec = FindById(lv + 1);

            if (nextRec == null)
            {
                return -1;
            }

            return nextRec.Level;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlayerLevelTableRecord : DataTableRecord
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("exp_require")]
        public int Name { get; set; }

        public override object GetId()
        {
            return Level;
        }
    }
}