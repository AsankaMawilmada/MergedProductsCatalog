using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MergedProductsCatalog.Services
{
    public static class CSVExportHelper
    {
        public static void ExportItems<T>(string fileName, List<T> items)
        {
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(items);
            }
        }
    }

}
