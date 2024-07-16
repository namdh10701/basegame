using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _Base.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.GD.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DataTable<TRecordType>
    {
        // public static readonly string SHEET_URL =
        //     "https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=1364617036#gid=1364617036";
        
        protected string DownloadUrl { get; private set; }
        protected string DataFileName { get; private set; }

        public DataTable(string downloadUrl, string dataFileName = null)
        {
            if (string.IsNullOrEmpty(dataFileName))
            {
                dataFileName = GetType().Name;
            }
            DownloadUrl = downloadUrl;
            DataFileName = dataFileName;
        }

        public virtual async Task LoadData()
        {
            Debug.Log("Load: " + GetType());
            var filePath = GetFilePath(DataFileName);
            await GSheetDownloader.Download(DownloadUrl, filePath);
            await using var stream = File.OpenRead(filePath);

            _records = Parser.Parser.GetRecords<TRecordType>(stream);
        }
        
        private List<TRecordType> _records = new();

        public List<TRecordType> Records => _records;
        
        public virtual List<TRecordType> GetRecords()
        {
            return _records;
        }

        // protected abstract void HandleLoadedRecords(List<TRecordType> records);

        private string GetFilePath(string dataFileName) => Path.Combine(Application.persistentDataPath, "Data", dataFileName);

        // public static DataTable<TRecordType> CreateTable(string downloadUrl, string dataFileName = null)
        // {
        //     return (DataTable<TRecordType>)Activator.CreateInstance(typeof(DataTable<TRecordType>), downloadUrl, dataFileName);
        // }
    }
}