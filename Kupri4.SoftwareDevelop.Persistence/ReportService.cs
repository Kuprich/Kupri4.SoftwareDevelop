using Kupri4.SoftwareDevelop.Domain.Persons;
using Kupri4.SoftwareDevelop.Domain.ReportTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence
{
    class ReportService
    {
        public GeneralReportData GetGeneralReportData(DateTime startDate, DateTime endDate)
        {

            ReportDataItem[] reportDataItems = HomeController.People
                .Select(person => new ReportDataItem
                (
                    person.FirstName,
                    person.GetHoursOnPeriod(startDate, endDate).Sum(),
                    person.GetPayOnPeriod(startDate, endDate))
                )
                .ToArray();

            return new GeneralReportData(reportDataItems, startDate, endDate);
        }

        public PersonalReportData GetPersonalReportData(string personName, DateTime startDate, DateTime endDate)
        {
            Person person = HomeController.People.First(p => p.FirstName.Equals(personName));
            
            return new(person.FirstName, 
                person.TimeRecords, 
                person.GetPayOnPeriod(startDate, endDate), 
                startDate, endDate);
        }
    }
}
