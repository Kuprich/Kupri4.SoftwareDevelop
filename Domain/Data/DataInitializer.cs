using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Data
{
    public static class DataInitializer
    {
        /// <summary>
        /// Создает тестовый набор сотрудников
        /// </summary>
        public static List<Person> CreateSomePersons()
        {
            return new List<Person>
            {
                new Manager("Иван", "Мальков", new List<TimeRecord>
                {
                    new TimeRecord(DateTime.Today.AddDays(-3), 8, "Сидел дома"),
                    new TimeRecord(DateTime.Today.AddDays(-2), 8, "Играл в пасьянс"),
                    new TimeRecord(DateTime.Today.AddDays(-1), 8, "Пил кофе")
                }),
                new Employee("Петр", "Иванов", new List<TimeRecord>
                {
                    new TimeRecord(DateTime.Today.AddDays(-3), 8, "Безельничал"),
                    new TimeRecord(DateTime.Today.AddDays(-2), 8, "Играл в бильярд"),
                    new TimeRecord(DateTime.Today.AddDays(-1), 8, "сидел в телеге")
                }),
                new Employee("Николай", "Николаев", new List<TimeRecord>
                {
                    new TimeRecord(DateTime.Today.AddDays(-3), 8, "Ломал стул"),
                    new TimeRecord(DateTime.Today.AddDays(-2), 8, "Царапал мебель"),
                    new TimeRecord(DateTime.Today.AddDays(-1), 8, "Играл в КС")
                }),
                new Freelancer("Василий", "Иванков", new List<TimeRecord>
                {
                    new TimeRecord(DateTime.Today.AddDays(-3), 8, "Чинил кофемашину"),
                    new TimeRecord(DateTime.Today.AddDays(-2), 8, "Тренировался в зале"),
                    new TimeRecord(DateTime.Today.AddDays(-1), 8, "переводил бабушек через дорогу")
                })
            };
        }
    }
}
