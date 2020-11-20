using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Helper
{
    public static class FileIO
    {
        public const string pointInfos_FileName = "pointInfos.csv";
        public const string lineInfos_FileName = "lineInfos.csv";


        /// <summary>
        /// Export data to csv
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="info"></param>
        public static void ExportToCSV(string filePath, List<string> info)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllLines(filePath, info);
        }

        public static string CreateFilePath(string folderPath, string fileNameWithExtension)
        {
            if (Directory.Exists(folderPath))
            {
               return Path.Combine(folderPath, fileNameWithExtension);
            }

            return null;
        }

        public static List<string> ReadFromCSV(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<string>();

            return File.ReadAllLines(filePath).ToList();
        }
    }
}
