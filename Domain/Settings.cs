using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain
{
    public static class Settings
    {
        public static byte WorkingHoursPerDay = 8;
        public static int WorkingHoursPerMonth = 160;
        public static class Manager
        {
            public const string Status = "Руководитель";
            public const decimal MonthSalary = 200_000M;
            public const decimal MonthBonus = 20_000M;
        }
        public static class Employee
        {
            public const string Status = "Сотрудник";
            public const decimal MonthSalary = 120_000M;
        }
        public static class Freelancer
        {
            public const string Status = "Фрилансер";
        }
    }
}
