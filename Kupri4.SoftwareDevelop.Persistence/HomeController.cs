using Kupri4.SoftwareDevelop.DataAccess.Csv;
using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using Kupri4.SoftwareDevelop.Domain.ReportTemplates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Persistence
{
    public class HomeController
    {
        /// <summary>
        /// весь список пользователей с записями работы
        /// </summary>
        private readonly ReportService _reportService = new ReportService();

        /// <summary>
        /// Текущий авторизованный пользователь
        /// </summary>
        public Person CurrentPerson { get; private set; }

        /// <summary>
        /// Получение массива имен сотрудников
        /// </summary>
        /// <returns>массив имен сотрудников</returns>
        public string[] GetPeopleNames()
            => PersonRepository.People.Select(p => p.FirstName).ToArray();


        /// <summary>
        /// Установка значения для текущего выбранного сотрудника
        /// </summary>
        /// <param name="personFirstName">Имя сотрудника</param>
        public void SetCurrentPerson(string personFirstName) =>
            CurrentPerson = PersonRepository.GetPersonByFirstName(personFirstName);


        /// <summary>
        /// Добавление времени сотруднику
        /// </summary>
        /// <param name="personName">Имя сотрудника</param>
        /// <param name="date">Дата действия</param>
        /// <param name="hours">Время выполнения задания</param>
        /// <param name="message">Инфо о выполненной работе</param>
        public void AddTime(string personName, DateTime date, byte hours, string message) =>
            PersonRepository.AddTime(personName, date, hours, message);


        /// <summary>
        /// Общий отчет по сотруднику
        /// </summary>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns>Данные, необходимые для печати отчета</returns>
        public GeneralReportData GetReportForAllPersons(DateTime startDate, DateTime endDate) => 
            _reportService.GetGeneralReportData(startDate, endDate);
            

        /// <summary>
        /// Отчет по одному сотруднику
        /// </summary>
        /// <param name="personName">Имя сотрдуника</param>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns>Данные, необходимые для печати отчета</returns>
        public PersonalReportData GetReportForAnyPerson(string personName, DateTime startDate, DateTime endDate) =>
            _reportService.GetPersonalReportData(personName, startDate, endDate);

        /// <summary>
        /// Добавление сотрудника с должностью "Руководитель"
        /// </summary>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="lastName">Фамилия сотрудника</param>
        /// <param name="status">Должность сотрудника</param>
        public void AddPerson(string firstName, string lastName, string status) =>
            PersonRepository.AddPerson(firstName, lastName, status);

    }
}