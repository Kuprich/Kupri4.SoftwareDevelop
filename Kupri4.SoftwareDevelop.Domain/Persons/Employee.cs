using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public class Employee : Staff
    {
        public Employee(string firstName, string lastName) 
            : base(firstName, lastName, Settings.Employee.Status, Settings.Employee.MonthSalary) { }

        public override decimal GetPayOnPeriod(DateTime startDate, DateTime endDate)
        {
            decimal payPerHour = Settings.Employee.MonthSalary / Settings.WorkingHoursPerMonth;

            decimal totalPay = 0;

            foreach (int hours in GetHoursOnPeriod(startDate, endDate))
            {
                if (hours > Settings.WorkingHoursPerDay)
                    totalPay += payPerHour * Settings.WorkingHoursPerDay + (hours - Settings.WorkingHoursPerDay) * 2 * payPerHour;
                else
                    totalPay += hours * payPerHour;
            }

            return totalPay;
        }
    }
}
