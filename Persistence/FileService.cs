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
            //пока это просто получение тестового набора данных;
            people.Add(new Manager("Иван", "Михайлович"));
            people.Add(new Employee("Василий", "Иванович"));
            people.Add(new Employee("Геннадий", "Петрович"));

            return true;
        }
    }
}
