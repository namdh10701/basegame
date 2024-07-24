using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _Base.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.GD.DataManager
{
    public abstract class DataTableRecord
    {
        public abstract object GetId();
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class DataTable<TRecordType> where TRecordType: DataTableRecord
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
            Debug.Log("Load: " + DataFileName);
            var filePath = GetFilePath(DataFileName);

            var shouldDownloadData = !File.Exists(filePath);
            shouldDownloadData = true;
            
            if (shouldDownloadData) {
                try
                {
                    await GSheetDownloader.Download(DownloadUrl, filePath);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error loading: " + DownloadUrl);
                    Debug.LogError(e);
                    throw;
                }
            }
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

        private string GetFilePath(string dataFileName) => Path.Combine(RootFolderPath, dataFileName);

        // public static DataTable<TRecordType> CreateTable(string downloadUrl, string dataFileName = null)
        // {
        //     return (DataTable<TRecordType>)Activator.CreateInstance(typeof(DataTable<TRecordType>), downloadUrl, dataFileName);
        // }
        
        public TRecordType FindById(string id)
        {
            return Records.Find(v => Equals(v.GetId(), id));
        }

        private static string RootFolderPath = Path.Combine(Application.persistentDataPath, "Data");
    }
}