using Kupri4.SoftwareDevelop.Domain.Persons;
using Kupri4.SoftwareDevelop.Persistence;
using Kupri4.SoftwareDevelop.Persistence.ReportTemplates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Kupri4.SoftwareDevelop.SoftwareDevelopConsole
{
    class Program
    {
        #region Variables
        int num;
        string firstName;
        string lastName;
        byte hours;
        string message;
        DateTime date;
        DateTime startDate;
        DateTime endDate;
        int totalHours;
        decimal totalPay;
        HomeController homeController = new();
        #endregion

        #region Methods
        bool IsSelectPerson()
        {
            Console.WriteLine($"\nВыберите сотрудника из списка: [1-{homeController.GetPeopleNames().Length}]");

            for (int i = 0; i < homeController.GetPeopleNames().Length; i++)
                Console.WriteLine($"[{i + 1}] - {homeController.GetPeopleNames()[i]}");
            Console.WriteLine();
            num = int.Parse(Console.ReadLine().Trim());

            if (num < 1 || num > homeController.GetPeopleNames().Length)
            {
                PrintInputNumError();
                return false;
            }
            return true;
        }
        bool IsSelectPeriod()
        {
            Console.Write("\nВведите дату начала периода в формате dd.mm.yyyy: ");
            startDate = DateTime.Parse(Console.ReadLine().Trim());
            Console.Write("Введите дату окончания периода в формате dd.mm.yyyy: ");
            endDate = DateTime.Parse(Console.ReadLine().Trim());
            if (startDate > endDate)
            {
                Console.WriteLine("Дата начала не может быть раньше, чем дата окончания");
                return false;
            }
            return true;
        }
        bool IsAddPerson()
        {
            Console.WriteLine("\nВыберите должность добавляемого сотрудника: [1-3]");
            Console.WriteLine($"[1] - Руководитель");
            Console.WriteLine($"[2] - Сотрудник на зарплате");
            Console.WriteLine($"[3] - Фрилансер\n");

            num = int.Parse(Console.ReadLine().Trim());
            if (num < 1 || num > 3)
            {
                PrintInputNumError();
                return false;
            }

            Console.Write("Введите Имя: ");
            firstName = Console.ReadLine().Trim();
            Console.Write("Введите Фамилию: ");
            lastName = Console.ReadLine().Trim();

            switch (num)
            {
                case 1:
                    homeController.AddManager(firstName, lastName);
                    break;
                case 2:
                    homeController.AddEmployee(firstName, lastName);
                    break;
                case 3:
                    homeController.AddFreelancer(firstName, lastName);
                    break;
            }
            return true;
        }
        bool IsExit()
        {
            Console.WriteLine("Вы уверены, что хотите выйти?\nНажмите (Д)а для подтверждения, (Н)ет - для отмены");
            return Console.ReadLine().Trim().Equals("Д", StringComparison.OrdinalIgnoreCase);
        }
        void AddTime()
        {
            Console.Write("Введите дату в формате dd.mm.yyyy: ");
            date = DateTime.Parse(Console.ReadLine().Trim());
            Console.Write("Кол-во часов работы: ");
            hours = byte.Parse(Console.ReadLine().Trim());
            Console.Write("Текст сообщения: ");
            message = Console.ReadLine().Trim();
        }
        void PrintPersonalReport(PersonalReportData prData)
        {
            Console.WriteLine($"\nОтчет по сотруднику {prData.Name} \nза период с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}:");
            foreach (var item in prData.TimeRecords)
                Console.WriteLine($"{item.Date.ToShortDateString()}, {item.Hours} часов, {item.Mesasge}");
            Console.WriteLine($"Итого: {prData.TotalHours} часов, заработано: {prData.TotalPay} рублей");
        }
        void PrintInputNumError()
        {
            ConsoleColor tmpColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nОшибка ввода!");
            Console.ForegroundColor = tmpColor;
        }
        #endregion

        static void Main(string[] args)
        {
            Program app = new();
            app.StartApp();
        }

        private void StartApp()
        {
            if (!Greatings()) 
                return;
            while (ShowActionsAndSelect()) ;
        }

        private bool ShowActionsAndSelect()
        {
            Console.Write("\nВыберите желаемое действие ");
            switch (homeController.CurrentPerson)
            {
                #region Manager Actions         
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
                            IsAddPerson();
                            break;
                        #endregion
                        #region case "2"
                        case "2":
                            if (!IsSelectPerson()) break;
                            AddTime();
                            homeController.AddTime(homeController.GetPeopleNames()[num - 1], date, hours, message);
                            break;
                        #endregion
                        #region case "3"
                        case "3":
                            if (!IsSelectPeriod()) break;
                            GeneralReportData[] grData = homeController.GetReportForAllPersons(startDate, endDate);
                            Console.WriteLine($"\nОтчет за период с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}");
                            totalHours = 0;
                            totalPay = 0;
                            foreach (GeneralReportData item in grData)
                            {
                                Console.WriteLine($"{item.Name} отработал {item.Hours} часов и заработал {item.Pay,-2} рублей");
                                totalHours += item.Hours;
                                totalPay += item.Pay;
                            }
                            Console.WriteLine($"Всего часов отработано за период: {totalHours}, Сумма к выплате: {totalPay}");
                            //Console.WriteLine("Желаете просмотреть еще один отчет? Нажмите \"(Д)а\" для продожения, \n \"(Н)ет\"для выхода на главный экран");

                            Console.WriteLine();
                            break;
                        #endregion
                        #region case "4"
                        case "4":
                            if (!IsSelectPerson()) break;
                            if (!IsSelectPeriod()) break;
                            PrintPersonalReport(homeController.GetReportForAnyPerson(homeController.GetPeopleNames()[num - 1], startDate, endDate));
                            break;
                        #endregion
                        #region case "5"
                        case "5":
                            if (IsExit()) return false;
                            break;
                        #endregion
                        #region default:
                        default:
                            PrintInputNumError();
                            break; 
                            #endregion
                    }
                    break;
                #endregion

                #region Employee & Freelancer Actions
                case Employee:
                case Freelancer:
                    Console.WriteLine("[1-3]:");
                    Console.WriteLine("[1]. Добавить себе время");
                    Console.WriteLine("[2]. Отчет о своём времени работы");
                    Console.WriteLine("[3]. Выход:");
                    switch (Console.ReadLine().Trim())
                    {
                        #region case "1"
                        case "1":
                            AddTime();
                            homeController.AddTime(homeController.CurrentPerson.FirstName, date, hours, message);
                            break;
                        #endregion
                        #region case "2"
                        case "2":
                            if (!IsSelectPeriod()) break;
                            PrintPersonalReport(homeController.GetReportForAnyPerson(homeController.CurrentPerson.FirstName, startDate, endDate));
                            break;
                        #endregion
                        #region case "3"
                        case "3":
                            if (IsExit()) return false;
                            break; 
                        #endregion
                        #region default:
                        default:
                            PrintInputNumError();
                            break; 
                            #endregion
                    }
                    break; 
                    #endregion
            }
            return true;
        }

        /// <summary>
        ///  Первоначальный экран приветствия.
        /// </summary>
        /// <returns>true - если пользователь найден</returns>
        bool Greatings()
        {
            void Greet()
            {
                Console.Write($"\nЗдравствуйте, {homeController.CurrentPerson.FirstName} {homeController.CurrentPerson.LastName}\nВаша роль: ");
                ConsoleColor tmpColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(homeController.CurrentPerson.Status);
                Console.ForegroundColor = tmpColor;
            }
            bool Login()
            {
                Console.WriteLine("Введите имя пользователя: ");
                string userName = Console.ReadLine();

                if (!homeController.GetPeopleNames().Contains(userName))
                {
                    Console.WriteLine("Пользователя с таким именем не существует\n Программа будет завершена");
                    return false;
                }
                return true;
            }

            Console.WriteLine("Добро пожаловать в программу по учету зарплаты");

            if (homeController.GetPeopleNames().Length == 0)
            {
                Console.WriteLine("Не найденно ни одного пользователя!");
                Console.WriteLine("Создать нового пользователя?\nВведите (Д)а для подсверждения операции, (Н)ет - Выход из программы");
                if (!Console.ReadLine().Trim().Equals("Д", StringComparison.OrdinalIgnoreCase)) 
                    return false;

                if (!IsAddPerson())
                    return false;
                else
                    homeController.SetCurrentPerson(firstName);
                Greet();
                return true;
            }

            if (Login())
                Greet();

            return true;
        }

    }
}
