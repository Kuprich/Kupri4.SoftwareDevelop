using Kupri4.SoftwareDevelop.Domain.Persons;
using Kupri4.SoftwareDevelop.Persistence;
using Kupri4.SoftwareDevelop.Persistence.ReportTemplates;
using System;
using System.Linq;

namespace Kupri4.SoftwareDevelop.SoftwareDevelopConsole
{
    class Program
    {
        HomeController homeController = new ();

        static void Main(string[] args)
        {
            Program app = new();
            app.StartApp();
            
        }

        private void StartApp()
        {
            if (!Greatings()) return;

            ShowActionsAndSelect();

        }

        private void ShowActionsAndSelect()
        {
            int num;
            string firstName;
            string lastName;
            byte hours;
            string message;
            DateTime date;
            DateTime startDate;
            DateTime endDate;
            Console.Write("Выберите желаемое действие ");
            switch (homeController.CurrentPerson)
            {
                case Manager:
                    Console.WriteLine("[1-5]:");
                    Console.WriteLine("[1] - Добавить сотрудника");
                    Console.WriteLine("[2] - Добавить время сотруднику:");
                    Console.WriteLine("[3] - Отчет за период по всем сотрудникам");
                    Console.WriteLine("[4] - Отчет за период по одному сотруднику");
                    Console.WriteLine("[5] - Выход\n");
                    switch (Console.ReadLine().Trim())
                    {
                        #region case "1"
                        case "1":
                            Console.WriteLine("\nВыберите должность добавляемого сотрудника: [1-3]");
                            Console.WriteLine($"[1] - Руководитель");
                            Console.WriteLine($"[2] - Сотрудник на зарплате");
                            Console.WriteLine($"[3] - Фрилансер\n");

                            num = int.Parse(Console.ReadLine().Trim());
                            if (num < 1 || num > 3)
                            {
                                Console.WriteLine("Ошибка ввода!");
                                break;
                            }

                            Console.Write("Введите Имя:");
                            firstName = Console.ReadLine().Trim();
                            Console.Write("Введите Фамилию:");
                            lastName = Console.ReadLine().Trim();

                            switch (num)
                            {
                                case 1:
                                    homeController.AddPerson(typeof(Manager), firstName, lastName);
                                    break;
                                case 2:
                                    homeController.AddPerson(typeof(Employee), firstName, lastName);
                                    break;
                                case 3:
                                    homeController.AddPerson(typeof(Freelancer), firstName, lastName);
                                    break;
                            }
                            break;
                        #endregion
                        #region case "2"
                        case "2":
                            Console.WriteLine($"Выберите сотрудника из списка, которому необходимо добавить время: [1-{homeController.GetPeopleNames().Length}]");

                            if (!SelectPerson()) break;

                            Console.Write("Введите дату в формате dd.mm.yyyy: ");
                            date = DateTime.Parse(Console.ReadLine().Trim());
                            Console.Write("Кол-во часов работы: ");
                            hours = byte.Parse(Console.ReadLine().Trim());
                            Console.Write("Текст сообщения: ");
                            message = Console.ReadLine().Trim();

                            homeController.AddTime(homeController.GetPeopleNames()[num - 1], date, hours, message);
                            break;
                        #endregion
                        #region case "3"
                        case "3":
                            Console.Write("Введите дату начала периода в формате dd.mm.yyyy: ");
                            startDate = DateTime.Parse(Console.ReadLine().Trim());
                            Console.Write("Введите дату окончания периода в формате dd.mm.yyyy: ");
                            endDate = DateTime.Parse(Console.ReadLine().Trim());
                            if (startDate > endDate)
                            {
                                Console.WriteLine("Дата начала не может быть раньше, чем дата окончания");
                                break;
                            }
                            GeneralReportData[] grData = homeController.GetReportForAllPersons(startDate, endDate);
                            Console.WriteLine($"\nОтчет за период с {startDate.Date} по {endDate.Date}");
                            int totalHours = 0;
                            decimal totalPay = 0;
                            foreach (GeneralReportData item in grData)
                            {
                                Console.WriteLine($"{item.Name} отработал {item.Hours} часов и заработал {item.Pay} рублей");
                                totalHours += item.Hours;
                                totalPay += item.Pay;
                            }
                            Console.WriteLine($"Всего часов отработано за период: {totalHours}, Сумма к выплате: {totalPay}");
                            //Console.WriteLine("Желаете просмотреть еще один отчет? Нажмите \"(Д)а\" для продожения, \n \"(Н)ет\"для выхода на главный экран");

                            Console.WriteLine();
                            break; 
                        #endregion
                        case "4":

                            break;
                        case "5":
                            break;
                        default:
                            Console.WriteLine("Ошибка ввода данных");
                            break;
                    }
                    break;

                case Employee:
                case Freelancer:
                    Console.WriteLine("[1-3]:");
                    Console.WriteLine("[1]. Добавить себе время");
                    Console.WriteLine("[2]. Отчет о своём времени работы");
                    Console.WriteLine("[3]. Выход:");
                    break;
            }

            bool SelectPerson()
            {
                for (int i = 0; i < homeController.GetPeopleNames().Length; i++)
                    Console.WriteLine($"[{i + 1}] - {homeController.GetPeopleNames()[i]}");

                num = int.Parse(Console.ReadLine().Trim());

                if (num < 1 || num > homeController.GetPeopleNames().Length)
                {
                    Console.WriteLine("Ошибка ввода!!");
                    return false;
                }
                return true;
            }

        }

        /// <summary>
        ///  Первоначальный экран приветствия.
        /// </summary>
        /// <returns>true - если всё пользователь найден</returns>
        bool Greatings()
        {
            Console.Write("Добро пожаловать в программу по учету зарплаты.\nВведите имя пользователя: ");
            string userName = Console.ReadLine();

            if (homeController.GetPeopleNames() == null)
            {
                Console.WriteLine("Список пользователей пуст");
                return false;
            }

            if (homeController.GetPeopleNames().Contains(userName))
                homeController.SetCurrentPerson(userName);
            Console.WriteLine($"\nЗдравствуйте, {homeController.CurrentPerson.FirstName} {homeController.CurrentPerson.LastName}");
            Console.WriteLine($"Ваша роль: {homeController.CurrentPerson.Status}");
            
            return true;
        }

    }
}
