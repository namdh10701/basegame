using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _Base.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.GD
{
    public abstract class DataManager<TRecordType>
    {
        // public static readonly string SHEET_URL =
        //     "https://docs.google.com/spreadsheets/d/1lg1LNncm8kVlASyTctlJqWU7dw5JWz2SKmbD82R8uM0/edit?gid=1364617036#gid=1364617036";
        
        protected abstract string DownloadUrl { get; }
        protected abstract string DataFileName { get; }

        public async Task LoadData()
        {
            Debug.Log("Load" + GetType());
            var filePath = GetFilePath(DataFileName);
            await GSheetDownloader.Download(DownloadUrl, filePath);
            await using var stream = File.OpenRead(filePath);

            var rawRecords = Parser.Parser.GetRecords<TRecordType>(stream);
            HandleLoadedRecords(rawRecords);
        }

        protected abstract void HandleLoadedRecords(List<TRecordType> records);

        private static string GetFilePath(string dataFileName) => Path.Combine(Application.persistentDataPath, "Data", dataFileName);

    }
}