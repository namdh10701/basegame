using System.Linq;
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

        public int GetLevelRecordCount(int level)
        {
            return Records.Count(v => v.Level == level);
        }
        
        public TalentTreeTableRecord FindByLevel(int level)
        {
            return Records.FirstOrDefault(v => v.Level == level);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class TalentTreeTableRecord: DataTableRecord
    {
        [Index(0)]
        [Default(0)]
        public int Id { get; set; }
        
        [Index(1)]
        [Default(0)]
        public int Level { get; set; }
        
        [Index(2)]
        [Default(0)]
        public int Cost { get; set; }
        
        [Index(3)]
        public string ItemId { get; set; }
        
        public override object GetId()
        {
            return Id;
        }
    }
}