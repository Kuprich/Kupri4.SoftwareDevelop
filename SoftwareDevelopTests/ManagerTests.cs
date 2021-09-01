using Kupri4.SoftwareDevelop.Domain;
using Kupri4.SoftwareDevelop.Domain.Persons;
using NUnit.Framework;
using System;

namespace Kupri4.SoftwareDevelop.SoftwareDevelopTests
{
    public class ManagerTests
    {

        [Test]
        public void GetPayOnPeriodTest()
        {
            Person m = new Manager("", "");
            m.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-3), 8, ""));
            m.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-2), 9, ""));
            m.TimeRecords.Add(new TimeRecord(DateTime.Now.AddDays(-1), 7, ""));

            Assert.AreEqual(m.GetPayOnPeriod(DateTime.Now.AddDays(-4), DateTime.Now), 29_750);
        }
    }
}