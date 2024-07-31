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

        public virtual async UniTask LoadData()
        {
            Debug.Log("Load Local: " + DataFileName);
            var filePath = GetFilePath(DataFileName);

            await using var stream = File.OpenRead(filePath);
            _records = Parser.Parser.GetRecords<TRecordType>(stream);
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