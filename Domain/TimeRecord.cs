using Kupri4.SoftwareDevelop.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupri4.SoftwareDevelop.Domain
{
    public class TimeRecord
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="date">Дата события</param>
        /// <param name="hours">Кол-во отработанных часов</param>
        /// <param name="mesasge">Описание выполненной работы</param>
        public TimeRecord(DateTime date, byte hours, string mesasge)
        {
            Date = date;
            Hours = hours;
            Mesasge = mesasge;
        }
        /// <summary>
        /// Конструктор класса без параметров
        /// </summary>
        public TimeRecord() { }
        /// <summary>
        /// Дата события
        /// </summary>
        public DateTime Date { get; }
        /// <summary>
        /// Кол-во отработанных часов
        /// </summary>
        public int Hours { get; set; }
        /// <summary>
        /// Описание выполненной работы
        /// </summary>
        public string Mesasge { get; set; }

    }
}
