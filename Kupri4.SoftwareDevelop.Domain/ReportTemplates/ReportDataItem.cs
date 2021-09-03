namespace Kupri4.SoftwareDevelop.Domain.ReportTemplates
{
    public struct ReportDataItem
    {
        public ReportDataItem(string name, int hours, decimal pay)
        {
            Name = name;
            Hours = hours;
            Pay = pay;
        }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Кол-во отработанных часов
        /// </summary>
        public int Hours { get; }

        /// <summary>
        /// Кол-во заработанных денег
        /// </summary>
        public decimal Pay { get; }

    }
}
