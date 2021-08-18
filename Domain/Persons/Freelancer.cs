using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public class Freelancer : Person
    {
        public Freelancer(string firstName, string lastName, List<TimeRecord> timeRecords) 
            : base(firstName, lastName, timeRecords) { }
    }
}
