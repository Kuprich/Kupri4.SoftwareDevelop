using System;
using System.Collections.Generic;

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

        public abstract decimal GetSalaryOnPeriod(DateTime startDate, DateTime endDate);

    }
}
