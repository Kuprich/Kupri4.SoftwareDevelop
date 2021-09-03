using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.ReportTemplates
{
    public struct GeneralReportData
    {
        /// <summary>
        /// Перечень данных по каждому сотруднику
        /// </summary>
        public readonly ReportDataItem[] ReportDataItems;
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        /// <summary>
        /// Итого отработано часов
        /// </summary>
        public int TotalHours => ReportDataItems.Sum(i => i.Hours);

        /// <summary>
        /// Итого заработано
        /// </summary>
        public decimal TotalPay => ReportDataItems.Sum(i => i.Pay);

        public GeneralReportData(ReportDataItem[] reportDataItems, DateTime startDate, DateTime endDate)
        {
            ReportDataItems = reportDataItems;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
