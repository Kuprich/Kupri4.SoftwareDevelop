using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence
{
    public class HomeController
    {

        List<Person> people = new();
        public Person CurrentPerson { get; private set; }

        FileService fileService = new();

        public HomeController()
        {
            fileService.GetPeopleList(people);
        }

        public string[] GetPeopleNames()
        {  
            return people.Select(p => p.FirstName).ToArray();
        }

        public void SetCurrentPerson(string userName)
        {
            CurrentPerson = people.First(p => p.FirstName == userName);
        }

        public void AddPerson<T>( T item, string firstName, string lastName)
        {
            switch (item)
            {
                case Manager:
                    people.Add(new Manager(firstName, lastName));
                    break;
                case Employee:
                    people.Add(new Employee(firstName, lastName));
                    break;
                case Freelancer:
                    people.Add(new Freelancer(firstName, lastName));
                    break;
            }
        }

        public void AddTime(string personName, DateTime date, byte hours, string mesage)
        {
            people.First(p => p.FirstName.Equals(personName))
                .TimeRecords.Add(new TimeRecord(date, hours, mesage));
        }
    }
}
