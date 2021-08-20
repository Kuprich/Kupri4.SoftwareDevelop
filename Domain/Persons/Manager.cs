using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain.Persons
{
    public class Manager : Staff
    {
        public Manager(string firstName, string lastName) 
            : base(firstName, lastName, Settings.Manager.Status, Settings.Manager.MonthSalary) { }

        public override decimal GetPayOnPeriod(DateTime startDate, DateTime endDate)
        {

            decimal payPerHour = Settings.Manager.MonthSalary / Settings.WorkingHoursPerMonth;
            decimal bonusPerDay = Settings.WorkingHoursPerDay * Settings.Manager.MonthBonus / Settings.WorkingHoursPerMonth;

            decimal totalPay = 0;

            foreach (int hours in GetHoursOnPeriod(startDate, endDate))
            {
                if (hours > Settings.WorkingHoursPerDay)
                    totalPay += payPerHour * Settings.WorkingHoursPerDay + bonusPerDay;
                else
                    totalPay += hours * payPerHour;
            }

            return totalPay;
        }
    }
}
