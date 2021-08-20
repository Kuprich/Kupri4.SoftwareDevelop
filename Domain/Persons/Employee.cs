using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public class Employee : Staff
    {
        public Employee(string firstName, string lastName) 
            : base(firstName, lastName, Settings.Employee.Status, Settings.Employee.MonthSalary) { }

        public override decimal GetSalaryOnPeriod(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
