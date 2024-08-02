using CsvHelper.Configuration.Attributes;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceTable : DataTable<ResourceTableRecord>
    {
        public ResourceTable(string downloadUrl, string dataFileName = null) : base(downloadUrl, dataFileName)
        {
        }
        public DataTableRecord GetDataTableRecord(string id, string name)
        {
            foreach (var record in Records)
            {
                if (record.Id == id && record.Name == name)
                    return record;
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ResourceTableRecord : DataTableRecord
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Name { get; set; }

        public override object GetId()
        {
            return Id;
        }
    }
}