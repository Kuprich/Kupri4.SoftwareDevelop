using System;
using System.Collections.Generic;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public abstract class Person
    {
        /// <summary>
        /// Лист временных записей по текущему сотрдунику
        /// </summary>
        public List<TimeRecord> TimeRecords { get; } = new();
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName { get; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName { get; }
        /// <summary>
        /// Занимаемая должность сотрудника
        /// </summary>
        public string Status { get; }


        #region protected Person(string firstName, string lastName, string status)
        /// <summary>
        /// Конструтор класса
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="status">Должность</param>
        protected Person(string firstName, string lastName, string status)
        {
            FirstName = firstName;
            LastName = lastName;
            Status = status;
        }
        #endregion
        #region protected Person() { }
        /// <summary> Конструктор класса без параметров </summary>
        protected Person() { } 
        #endregion

        #region public abstract decimal GetPayOnPeriod(DateTime startDate, DateTime endDate); 
        /// <summary>
        /// Подстчет заработанных за период денег
        /// </summary>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns>Кол-во денег</returns>
        public abstract decimal GetPayOnPeriod(DateTime startDate, DateTime endDate); 
        #endregion
        #region public int[] GetHoursOnPeriod(DateTime startDate, DateTime endDate)
        /// <summary>
        /// Выборка кол-ва часов за период
        /// </summary>
        /// <param name="startDate">Начало периода</param>
        /// <param name="endDate">Конец периода</param>
        /// <returns></returns>
        public int[] GetHoursOnPeriod(DateTime startDate, DateTime endDate)
        {
            return TimeRecords
            .Where(r => r.Date >= startDate && r.Date <= endDate)
            .Select(r => r.Hours)
            .ToArray();
        } 
        #endregion

    }
}
