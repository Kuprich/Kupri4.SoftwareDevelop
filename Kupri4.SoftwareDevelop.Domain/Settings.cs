using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain
{
    public static class Settings
    {
        /// <summary>
        /// Кол-во рабочих часов в день
        /// </summary>
        public const int WorkingHoursPerDay = 8;
        /// <summary>
        /// Кол-во рабочих часов в месяц
        /// </summary>
        public const int WorkingHoursPerMonth = 160;
        public static class Manager
        {
            /// <summary>
            /// Занимаемая должность
            /// </summary>
            public const string Status = "Руководитель";
            /// <summary>
            /// Зарплата за месяц
            /// </summary>
            public const decimal MonthSalary = 200_000.0M;
            /// <summary>
            /// премия за месяц
            /// </summary>
            public const decimal MonthBonus = 20_000.0M;
            /// <summary>
            /// Путь к файлу с временными записями
            /// </summary>
            public const string TimeRecordsFilePath = BaseDir + "\\TimeListForManagers.csv";
        }
        public static class Employee
        {
            /// <summary>
            /// Занимаемая должность
            /// </summary>
            public const string Status = "Сотрудник";
            /// <summary>
            /// Зарплата за месяц
            /// </summary>
            public const decimal MonthSalary = 120_000M;
            /// <summary>
            /// Путь к файлу с временными записями
            /// </summary>
            public const string TimeRecordsFilePath = BaseDir + "\\TimeListForEmployes.csv";
        }
        public static class Freelancer
        {
            /// <summary>
            /// Занимаемая должность
            /// </summary>
            public const string Status = "Фрилансер";
            /// <summary>
            /// Зарплата за час
            /// </summary>
            public const decimal PayPerHour = 1_000M;
            /// <summary>
            /// Путь к файлу с временными записями
            /// </summary>
            public const string TimeRecordsFilePath = BaseDir + "\\TimeListForFreelancers.csv";
        }
        /// <summary>
        /// базовая директория для хранения *.csv файлов проекта
        /// </summary>
        public const string BaseDir = "Db";
        /// <summary>
        /// Путь к файлу с данными об основной информации сотрудников
        /// </summary>
        public const string PeopleListPath  = BaseDir + "\\PeopleList.csv";

    }
}
                                           