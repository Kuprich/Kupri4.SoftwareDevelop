using System;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public abstract class Person
    {
        public string Name { get; }
        public Person(string name)
        {
            Name = name;
        }
    }
}
