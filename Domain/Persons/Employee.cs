using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public class Employee : Staff
    {
        public Employee(string name, decimal monthSalary) : base(name, monthSalary)
        {
        }
    }
}
