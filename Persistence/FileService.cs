using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.IO;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Persistence
{
    class FileService
    {
        public FileService() { FileInitialize(); }

        #region void FileInitialize()
        /// <summary>
        /// Инициализация каталогов и файлов для хранения записей
        /// </summary>
        void FileInitialize()
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
        #endregion
        #region public void SaveTimeRecordToFile(string personName)
        /// <summary>
        /// Сохранение временной записи в файл
        /// </summary>
        /// <param name="personName">Имя сотрудника</param>
        public void SaveTimeRecordToFile(string personName)
        {
            Person p = HomeController.People.First(p => p.FirstName == personName);
            TimeRecord tr = p.TimeRecords.Last();
            string filePath = null;
            switch (p)
            {
                case Manager:
                    filePath = Settings.Manager.TimeRecordsFilePath;
                    break;
                case Employee:
                    filePath = Settings.Employee.TimeRecordsFilePath;
                    break;
                case Freelancer:
                    filePath = Settings.Employee.TimeRecordsFilePath;
                    break;
            }
            File.AppendAllText(filePath, $"{tr.Date.ToShortDateString()},{p.FirstName},{tr.Hours},{tr.Mesasge}");
        }
        #endregion
        #region public void LoadPeopleDataFromFiles()
        /// <summary>
        /// Получить список пользователей из файла "Список сотрудников"
        /// </summary>
        /// <param name="people"></param>
        /// <returns>true - если получится загрузить данные из файла</returns>
        public void LoadPeopleDataFromFiles()
        {
            void LoadTimeRecords(string filePath)
            {
                Person person = HomeController.People.Last();
                string[] TimeRecordsData = File.ReadAllLines(filePath);
                foreach (string line in TimeRecordsData)
                {
                    string[] items = line.Split(',').Select(s => s.Trim()).ToArray();

                    if (items[1] == person.FirstName)
                        person.TimeRecords.Add(new TimeRecord(DateTime.Parse(items[0]), byte.Parse(items[2]), items[3]));
                }
            }

            string[] peopleData = File.ReadAllLines(Settings.PeopleListPath);

            foreach (string line in peopleData)
            {
                string[] items = line.Split(',').Select(s => s.Trim()).ToArray();

                switch (items.Last())
                {
                    case Settings.Manager.Status:
                        HomeController.People.Add(new Manager(items[0], items[1]));
                        LoadTimeRecords(Settings.Manager.TimeRecordsFilePath);
                        break;

                    case Settings.Employee.Status:
                        HomeController.People.Add(new Employee(items[0], items[1]));
                        LoadTimeRecords(Settings.Employee.TimeRecordsFilePath);
                        break;

                    case Settings.Freelancer.Status:
                        HomeController.People.Add(new Freelancer(items[0], items[1]));
                        LoadTimeRecords(Settings.Freelancer.TimeRecordsFilePath);
                        break;
                }
            }
        }
        #endregion
        #region public void SavePersonToFile(Person p)
        /// <summary>
        /// Сохранение данных о сотруднике в файл
        /// </summary>
        /// <param name="p"></param>
        public void SavePersonToFile(Person p) =>
            File.AppendAllText(Settings.PeopleListPath, $"{p.FirstName},{p.LastName},{p.Status}"); 
        #endregion
    }
}
