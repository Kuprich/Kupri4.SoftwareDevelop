using System;
using System.Collections.Generic;

namespace Kupri4.SoftwareDevelop.DataAccess.Csv
{
    public class EmployeeRepository
    {

        //public void LoadPeopleDataFromFiles()
        //{
        //    void LoadTimeRecords(string filePath)
        //    {
        //        Person person = HomeController.People.Last();
        //        string[] TimeRecordsData = File.ReadAllLines(filePath);
        //        foreach (string line in TimeRecordsData)
        //        {
        //            string[] LineData = line.Split(',').Select(s => s.Trim()).ToArray();

        //            if (LineData[1] == person.FirstName)
        //                person.TimeRecords.Add(new TimeRecord(DateTime.ParseExact(LineData[0], "dd.MM.yyyy", CultureInfo.InvariantCulture), byte.Parse(LineData[2]), LineData[3]));
        //        }
        //    }

        //    string[] peopleData = File.ReadAllLines(Settings.PeopleListPath);

        //    foreach (string line in peopleData)
        //    {
        //        string[] lineData = line.Split(',').Select(s => s.Trim()).ToArray();

        //        switch (lineData.Last())
        //        {
        //            case Settings.Manager.Status:
        //                HomeController.People.Add(new Manager(lineData[0], lineData[1]));
        //                LoadTimeRecords(Settings.Manager.TimeRecordsFilePath);
        //                break;

        //            case Settings.Employee.Status:
        //                HomeController.People.Add(new Employee(lineData[0], lineData[1]));
        //                LoadTimeRecords(Settings.Employee.TimeRecordsFilePath);
        //                break;

        //            case Settings.Freelancer.Status:
        //                HomeController.People.Add(new Freelancer(lineData[0], lineData[1]));
        //                LoadTimeRecords(Settings.Freelancer.TimeRecordsFilePath);
        //                break;
        //        }
        //    }
        //}
    }
}
