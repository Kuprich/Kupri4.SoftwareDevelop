using Kupri4.SoftwareDevelop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Domain.ReportTemplates
{
    public struct PersonalReportData
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя Сотрдуника</param>
        /// <param name="timeRecords">Списокв временных записей, принадлежащих текущему сотруднику</param>
        /// <param name="totalPay">Кол-во заработанных денег за весть период</param>
        public PersonalReportData(string name, List<TimeRecord> timeRecords, decimal totalPay, DateTime startDate, DateTime endDate)
        {
            TimeRecords = timeRecords;
            TotalPay = totalPay;
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
        }
        /// <summary>
        /// Имя Сотрдуника
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Итоговое кол-во отработанных часов
        /// </summary>
        public int TotalHours => TimeRecords.Sum(r => r.Hours);

        /// <summary>
        /// Кол-во заработанных денег за весть период
        /// </summary>
        public decimal TotalPay { get; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// Списокв временных записей, принадлежащих текущему сотруднику
        /// </summary>
        public List<TimeRecord> TimeRecords { get; }
    }
}
