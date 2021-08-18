using System;
using System.Collections.Generic;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public abstract class Person
    {
        public List<TimeRecord> TimeRecords { get; } = new();
        public string FirstName { get; }
        public string LastName { get; set; }
        protected Person(string firstName, string lastName, List<TimeRecord> timeRecords)
        {
            TimeRecords = timeRecords;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
