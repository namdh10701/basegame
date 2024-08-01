using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.GD.DataManager
{
    public abstract class LocalDataTable<TRecordType> where TRecordType: DataTableRecord
    {
        protected string DataFileName { get; private set; }

        protected LocalDataTable(string dataFileName)
        {
            DataFileName = dataFileName;
        }

        public virtual UniTask LoadData()
        {
            var filePath = GetFilePath(DataFileName);
            var allText = File.ReadAllText(filePath);
            _records = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TRecordType>>(allText);
            return default;
        }
        
        private List<TRecordType> _records = new();

        public List<TRecordType> Records => _records;
        
        public virtual List<TRecordType> GetRecords()
        {
            return _records;
        }

        private string GetFilePath(string dataFileName) => Path.Combine(Application.persistentDataPath, dataFileName);

        public TRecordType FindById(string id)
        {
            return Records.Find(v => Equals(v.GetId(), id));
        }
    }
}