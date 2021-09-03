using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Kupri4.SoftwareDevelop.DataAccess.Csv
{
    static class FileInitializer
    {

        /// <summary>
        /// Инициализация каталогов и файлов для хранения записей
        /// </summary>
        public static void FileInitialize()
        {
            if (!Directory.Exists(Settings.BaseDir))
                Directory.CreateDirectory(Settings.BaseDir);

            foreach (string filePath in new string[]{
                Settings.PeopleListPath,
                Settings.Manager.TimeRecordsFilePath,
                Settings.Employee.TimeRecordsFilePath,
                Settings.Freelancer.TimeRecordsFilePath})
                if (!File.Exists(filePath))
                    File.CreateText(filePath).Close();
        }

    }
}
