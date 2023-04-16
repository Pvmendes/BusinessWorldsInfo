using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWorldsInfo.Utils
{
    public static class CSVFileUtils
    {
        public static void CSVWriteLocal(string filePath, IEnumerable<dynamic> obj)
        {
            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, configPersons))
            {
                csv.WriteRecords(obj);
            }
        }
    }
}
