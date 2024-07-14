using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Fusion.LagCompensation;

namespace _Game.Scripts.GD.Parser
{
    public class Parser
    {
        public static List<TRecordClass> GetRecords<TRecordClass>(FileStream filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TRecordClass>().ToList();
        }

        public static List<TRecordClass> GetRecords<TRecordClass>(string csvContent)
        {
            using var reader = new StringReader(csvContent);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TRecordClass>().ToList();
        }
    }
}