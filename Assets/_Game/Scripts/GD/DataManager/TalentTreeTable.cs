using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeTable : DataTable<TalentTreeTableRecord>
    {
        public TalentTreeTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeTableRecord
    {
        [Index(0)]
        public string Id { get; set; }
        
        [Index(1)]
        [Default(0)]
        public int Level { get; set; }
        
        [Index(2)]
        [Default(0)]
        public int Cost { get; set; }
        
        [Index(3)]
        public string ItemId { get; set; }
    }
}