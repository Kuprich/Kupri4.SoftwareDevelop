using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain
{
    class TimeRecord
    {
        public TimeRecord(DateTime date, string name, byte hours, string mesasge)
        {
            Date = date;
            Name = name;
            Hours = hours;
            Mesasge = mesasge;
        }

        public DateTime Date { get; }
        public string Name { get; }
        public byte Hours { get; set; }
        public string Mesasge { get; set; }
    }
}
