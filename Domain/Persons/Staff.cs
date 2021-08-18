using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public abstract class Staff : Person
    {
        decimal MonthSalary { get; }
        public Staff(string firstName, string lastName, decimal monthSalary, List<TimeRecord> timeRecords) 
            : base(firstName, lastName, timeRecords)
        {
            MonthSalary = monthSalary;
        }
    }
}
