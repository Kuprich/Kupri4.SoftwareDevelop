using Kupri4.SoftwareDevelop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence.ReportTemplates
{
    public struct PersonalReportData
    {
        public PersonalReportData(string name, List<TimeRecord> timeRecords, decimal totalPay)
        {
            TimeRecords = timeRecords;
            TotalPay = totalPay;
            Name = name;
        }
        public string Name { get; }
        public int TotalHours => TimeRecords.Sum(r => r.Hours);
        public decimal TotalPay { get; }
        public List<TimeRecord> TimeRecords { get; }
    }
}
