using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public class Staff : Person
    {
        decimal MonthSalary { get; }
        public Staff(string name, decimal monthSalary) : base(name)
        {
            MonthSalary = monthSalary;
        }
    }
}
