using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using Kupri4.SoftwareDevelop.Persistence.ReportTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence
{
    public class HomeController
    {
        
        internal static List<Person> People { get; set; } =  new();
        FileService fileService = new();
        public Person CurrentPerson { get; private set; }

        public HomeController() { fileService.LoadPeopleDataFromFiles(); }

        #region public string[] GetPeopleNames()
        /// <summary>
        /// Получение массива имен сотрудников
        /// </summary>
        /// <returns>массив имен сотрудников</returns>
        public string[] GetPeopleNames()
            => People.Select(p => p.FirstName).ToArray();
        #endregion
        #region public void SetCurrentPerson(string userName)
        /// <summary>
        /// Установка значения для текущего выбранного сотрудника
        /// </summary>
        /// <param name="personName">Имя сотрудника</param>
        public void SetCurrentPerson(string personName) =>
            CurrentPerson = People.First(p => p.FirstName == personName);
        #endregion
        #region  public void AddTime(string personName, DateTime date, byte hours, string mesage)
        /// <summary>
        /// Добавление времени сотруднику
        /// </summary>
        /// <param name="personName">Имя сотрудника</param>
        /// <param name="date">Дата действия</param>
        /// <param name="hours">Время выполнения задания</param>
        /// <param name="mesage">Инфо о выполненной работе</param>
        public void AddTime(string personName, DateTime date, byte hours, string mesage)
        {
            People.First(p => p.FirstName.Equals(personName))
                .TimeRecords.Add(new TimeRecord(date, hours, mesage));
            fileService.SaveTimeRecordToFile(personName);
        }
        #endregion
        #region public GeneralReportData[] GetReportForAllPersons(DateTime startDate, DateTime endDate)
        /// <summary>
        /// Общий отчет по сотруднику
        /// </summary>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns>Данные, необходимые для печати отчета</returns>
        public GeneralReportData[] GetReportForAllPersons(DateTime startDate, DateTime endDate) => People
            .Select(p => new GeneralReportData(p.FirstName, p.GetHoursOnPeriod(startDate, endDate).Sum(), p.GetPayOnPeriod(startDate, endDate)))
            .ToArray();
        #endregion
        #region public PersonalReportData GetReportForAnyPerson(string personName, DateTime startDate, DateTime endDate)
        /// <summary>
        /// Отчет по одному сотруднику
        /// </summary>
        /// <param name="personName">Имя сотрдуника</param>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns>Данные, необходимые для печати отчета</returns>
        public PersonalReportData GetReportForAnyPerson(string personName, DateTime startDate, DateTime endDate)
        {
            Person p = People.First(p => p.FirstName.Equals(personName));
            return new(p.FirstName, p.TimeRecords, p.GetPayOnPeriod(startDate, endDate));
        }
        #endregion
        #region  public void AddManager(string firstName, string lastName)
        /// <summary>
        /// Добавление сотрудника с должностью "Руководитель"
        /// </summary>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="lastName">Фамилия сотрудника</param>
        public void AddManager(string firstName, string lastName)
        {
            Manager manager = new(firstName, lastName);
            People.Add(manager);
            fileService.SavePersonToFile(manager);
        }
        #endregion
        #region public void AddEmployee(string firstName, string lastName)
        /// <summary>
        /// Добавление сотрудника с должностью "Сотрудник на зарплате"
        /// </summary>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="lastName">Фамилия сотрудника</param>
        public void AddEmployee(string firstName, string lastName)
        {
            Employee employee = new(firstName, lastName);
            People.Add(employee);
            fileService.SavePersonToFile(employee);
        }
        #endregion
        #region public void AddFreelancer(string firstName, string lastName)
        /// <summary>
        /// Добавление сотрудника с должностью "Фрилансер"
        /// </summary>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="lastName">Фамилия сотрудника</param>
        public void AddFreelancer(string firstName, string lastName)
        {
            Freelancer freelancer = new(firstName, lastName);
            People.Add(freelancer);
            fileService.SavePersonToFile(freelancer);
        } 
        #endregion
    }
}
