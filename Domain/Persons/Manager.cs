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

        public override decimal GetSalaryOnPeriod(DateTime startDate, DateTime endDate)
        {
            byte[] hoursArray = TimeRecords
            .Where(r => r.Date >= startDate && r.Date <= endDate)
            .Select(r => r.Hours)
            .ToArray();

            decimal salaryPerHour = Settings.WorkingHoursPerDay / Settings.WorkingHoursPerMonth * Settings.Manager.MonthSalary;
            decimal bonusPerHour = Settings.WorkingHoursPerDay / Settings.WorkingHoursPerMonth * Settings.Manager.MonthBonus;

            decimal totalSalary = 0;
            foreach (byte hours in hoursArray)
            {
                if (hours > Settings.WorkingHoursPerDay)
                    totalSalary += Settings.WorkingHoursPerDay * salaryPerHour + (hours - Settings.WorkingHoursPerDay) * bonusPerHour;
            }
            return totalSalary;
        }
    }
}
