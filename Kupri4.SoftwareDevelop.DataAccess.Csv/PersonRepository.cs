using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Kupri4.SoftwareDevelop.DataAccess.Csv
{

    public static class PersonRepository
    {
        public static List<Person> People { get; set; } = new List<Person>();

        static PersonRepository()
        {
            FileInitializer.FileInitialize();
            LoadPeopleDataFromFiles();
        }
        public static Person GetPersonByFirstName(string firstName)
        {
            return People.First(person => person.FirstName.Equals(firstName));
        }
        public static void AddTime(string firstName, DateTime date, byte hours, string mesage)
        {
            GetPersonByFirstName(firstName)
               .TimeRecords.Add(new TimeRecord(date, hours, mesage));
            SaveTimeRecordToFile(firstName);
        }

        static void LoadPeopleDataFromFiles()
        {
            void LoadTimeRecords(string filePath)
            {
                Person person = People.Last();
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
                        People.Add(new Manager(lineData[0], lineData[1]));
                        LoadTimeRecords(Settings.Manager.TimeRecordsFilePath);
                        break;

                    case Settings.Employee.Status:
                        People.Add(new Employee(lineData[0], lineData[1]));
                        LoadTimeRecords(Settings.Employee.TimeRecordsFilePath);
                        break;

                    case Settings.Freelancer.Status:
                        People.Add(new Freelancer(lineData[0], lineData[1]));
                        LoadTimeRecords(Settings.Freelancer.TimeRecordsFilePath);
                        break;
                }
            }
        }

        static void SaveTimeRecordToFile(string firstName)
        {
            Person person = People.First(p => p.FirstName == firstName);
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

        public static void AddPerson(string firstName, string lastName, string status)
        {
            Person person; 

            switch (status)
            {
                case Settings.Manager.Status:
                    person = new Manager(firstName, lastName);
                    break;
                case Settings.Employee.Status:
                    person = new Employee(firstName, lastName);
                    break;
                case Settings.Freelancer.Status:
                    person = new Freelancer(firstName, lastName);
                    break;
                default:
                    person = null;
                    break;
            }

            if (person is null)
            {
                return;
            }

            People.Add(person);
            SavePersonToFile(person);

        }

        static void SavePersonToFile(Person person)
        {
            using StreamWriter writer = File.AppendText(Settings.PeopleListPath);
            writer.WriteLine($"{person.FirstName},{person.LastName},{person.Status}");
        }
    }
}
