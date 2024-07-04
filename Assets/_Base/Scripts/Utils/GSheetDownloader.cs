using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace _Base.Scripts.Utils
{
    public class GSheetDownloader
    {
        private static string GetDownloadUrl(string url)
        {
            var matches = Regex.Matches(url, @"\/d\/(.+)\/edit\?gid=(\d+)");
            var matched = matches.FirstOrDefault();

            if (matched == null || matched.Groups.Count != 2)
            {
                return null;
            }
            
            var docId = matched.Groups[0];
            var gId = matched.Groups[1];
            // string docId, string gId
            return
                $"https://docs.google.com/spreadsheets/d/{docId}/export?gid={gId}&format=csv";
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="saveTargetFile"></param>
        public static async Task Download(string url, string saveTargetFile)
        {
            var contents = await GetHttpResponse(GetDownloadUrl(url));

            var dir = Path.GetDirectoryName(saveTargetFile);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            
            await File.WriteAllTextAsync(saveTargetFile, contents);
        }
        
        static async Task<string> GetHttpResponse(string url)
        {
            return (await UnityWebRequest.Get(url).SendWebRequest()).downloadHandler.text;
        }
    }
}