using Kupri4.SoftwareDevelop.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Persistence.ReportTemplates
{
    public struct PersonalReportData
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя Сотрдуника</param>
        /// <param name="timeRecords">Списокв временных записей, принадлежащих текущему сотруднику</param>
        /// <param name="totalPay">Кол-во заработанных денег за весть период</param>
        public PersonalReportData(string name, List<TimeRecord> timeRecords, decimal totalPay)
        {
            TimeRecords = timeRecords;
            TotalPay = totalPay;
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
        /// Списокв временных записей, принадлежащих текущему сотруднику
        /// </summary>
        public List<TimeRecord> TimeRecords { get; }
    }
}
