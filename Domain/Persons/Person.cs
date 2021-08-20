using System;
using System.Collections.Generic;
using System.Linq;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public abstract class Person
    {
        public List<TimeRecord> TimeRecords { get; } = new();
        public string FirstName { get; }
        public string LastName { get; }
        public string Status { get; }

        protected Person(string firstName, string lastName, string status)
        {
            FirstName = firstName;
            LastName = lastName;
            Status = status;
        }
        protected Person() { }

        public abstract decimal GetPayOnPeriod(DateTime startDate, DateTime endDate);

        public int[] GetHoursOnPeriod(DateTime startDate, DateTime endDate)
        {
            return TimeRecords
            .Where(r => r.Date >= startDate && r.Date <= endDate)
            .Select(r => r.Hours)
            .ToArray();
        }

    }
}
