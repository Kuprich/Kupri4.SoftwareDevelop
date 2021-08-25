using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence.ReportTemplates
{
    public struct GeneralReportData
    {
        #region public GeneralReportData(string name, int hours, decimal pay)
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="hours">Кол-во отработанных часов </param>
        /// <param name="pay">Кол-во заработанных денег</param>
        public GeneralReportData(string name, int hours, decimal pay)
        {
            Name = name;
            Hours = hours;
            Pay = pay;
        }
        #endregion
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Кол-во отработанных часов
        /// </summary>
        public int Hours { get; set; }
        /// <summary>
        /// Кол-во заработанных денег
        /// </summary>
        public decimal Pay { get; set; }
    }
}
