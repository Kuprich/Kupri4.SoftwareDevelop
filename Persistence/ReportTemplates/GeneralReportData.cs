using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence.ReportTemplates
{
    public struct GeneralReportData
    {
        public GeneralReportData(string name, int hours, decimal pay)
        {
            Name = name;
            Hours = hours;
            Pay = pay;
        }

        public string Name { get; set; }
        public int Hours { get; set; }
        public decimal Pay { get; set; }
    }
}
