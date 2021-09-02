﻿using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Persistence
{
    class FileService
    {
        public FileService() { FileInitialize(); }

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

        /// <summary>
        /// Сохранение временной записи в файл
        /// </summary>
        /// <param name="personName">Имя сотрудника</param>
        public void SaveTimeRecordToFile(string personName)
        {
            Person person = HomeController.People.First(p => p.FirstName == personName);
            TimeRecord timeRecord = person.TimeRecords.Last();
            string filePath = null;
            switch (person)
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
            using StreamWriter writer = File.AppendText(filePath);
            writer.WriteLine($"{timeRecord.Date.ToShortDateString()},{person.FirstName},{timeRecord.Hours},{timeRecord.Mesasge}");
        }

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
                    string[] LineData = line.Split(',').Select(s => s.Trim()).ToArray();

                    if (LineData[1] == person.FirstName)
                        person.TimeRecords.Add(new TimeRecord(DateTime.ParseExact(LineData[0], "dd.MM.yyyy", CultureInfo.InvariantCulture), byte.Parse(LineData[2]), LineData[3]));
                }
            }

            string[] peopleData = File.ReadAllLines(Settings.PeopleListPath);

            foreach (string line in peopleData)
            {
                string[] lineData = line.Split(',').Select(s => s.Trim()).ToArray();

                switch (lineData.Last())
                {
                    case Settings.Manager.Status:
                        HomeController.People.Add(new Manager(lineData[0], lineData[1]));
                        LoadTimeRecords(Settings.Manager.TimeRecordsFilePath);
                        break;

                    case Settings.Employee.Status:
                        HomeController.People.Add(new Employee(lineData[0], lineData[1]));
                        LoadTimeRecords(Settings.Employee.TimeRecordsFilePath);
                        break;

                    case Settings.Freelancer.Status:
                        HomeController.People.Add(new Freelancer(lineData[0], lineData[1]));
                        LoadTimeRecords(Settings.Freelancer.TimeRecordsFilePath);
                        break;
                }
            }
        }

        /// <summary>
        /// Сохранение данных о сотруднике в файл
        /// </summary>
        /// <param name="person"></param>
        public void SavePersonToFile(Person person)
        {
            using StreamWriter writer = File.AppendText(Settings.PeopleListPath);
            writer.WriteLine($"{person.FirstName},{person.LastName},{person.Status}");
        }
    }
}