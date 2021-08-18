using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain
{
    public class TimeRecord
    {
        public TimeRecord(DateTime date, byte hours, string mesasge)
        {
            Date = date;
            Hours = hours;
            Mesasge = mesasge;
        }
        public TimeRecord() { }
        public DateTime Date { get; }
        public byte Hours { get; set; }
        public string Mesasge { get; set; }

    }
}
