using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Persistence
{
    class FileService
    {
        /// <summary>
        /// Получить список пользователей из файла "Список сотрудников"
        /// </summary>
        /// <param name="people"></param>
        /// <returns>true - если получится загрузить данные из файла</returns>
        public bool GetPeopleList(List<Person> people)
        {
            // пока это просто получение тестового набора данных;
            // необходимо заменить это на чтение данных из файла

            // получение списка сотрудников;
            people.Add(new Manager("Иван", "Михайлович"));
            people.Add(new Employee("Василий", "Иванович"));
            people.Add(new Employee("Геннадий", "Петрович"));
            people.Add(new Freelancer("Василий", "Иванков"));

            // получение их времени работы
            people[0].TimeRecords.AddRange(new TimeRecord[]
            {
                new TimeRecord(DateTime.Today.AddDays(-3), 8, "Сидел дома"),
                new TimeRecord(DateTime.Today.AddDays(-2), 9, "Играл в пасьянс"),
                new TimeRecord(DateTime.Today.AddDays(-1), 7, "Пил кофе")
            });

            people[1].TimeRecords.AddRange(new TimeRecord[]
            {
                new TimeRecord(DateTime.Today.AddDays(-3), 8, "Безельничал"),
                new TimeRecord(DateTime.Today.AddDays(-2), 9, "Играл в бильярд"),
                new TimeRecord(DateTime.Today.AddDays(-1), 7, "сидел в телеге")
            });

            people[2].TimeRecords.AddRange(new TimeRecord[]
            {
                new TimeRecord(DateTime.Today.AddDays(-3), 8, "Ломал стул"),
                new TimeRecord(DateTime.Today.AddDays(-2), 9, "Царапал мебель"),
                new TimeRecord(DateTime.Today.AddDays(-1), 7, "Играл в КС")
            }); 
            
            people[3].TimeRecords.AddRange(new TimeRecord[]
            {
                new TimeRecord(DateTime.Today.AddDays(-3), 8, "Чинил кофемашину"),
                new TimeRecord(DateTime.Today.AddDays(-2), 9, "Тренировался в зале"),
                new TimeRecord(DateTime.Today.AddDays(-1), 7, "переводил бабушек через дорогу")
            });

            return true;
        }
    }
}
