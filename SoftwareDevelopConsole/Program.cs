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
        #region Поля и свойства
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

        static void Main()
        {
            Program app = new();
            app.StartApp();
        }

        #region private void StartApp()
        /// <summary>
        /// старт программы
        /// </summary>
        private void StartApp()
        {
            if (!Greatings())
                return;
            while (ShowActionsAndSelect()) ;
        }
        #endregion

        #region bool Greatings()
        /// <summary>
        ///  Первоначальный экран приветствия.
        /// </summary>
        /// <returns>True - если пользователь найден</returns>
        bool Greatings()
        {
            void Greet()
            {
                Console.Write($"Здравствуйте, {homeController.CurrentPerson.FirstName} {homeController.CurrentPerson.LastName}\nВаша роль: ");
                ConsoleColor tmpColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(homeController.CurrentPerson.Status);
                Console.ForegroundColor = tmpColor;
            }
            bool Login()
            {
                Console.Write("Введите имя пользователя: ");
                string userName = Console.ReadLine().Trim();

                if (!homeController.GetPeopleNames().Contains(userName))
                {
                    Console.WriteLine("Пользователя с таким именем не существует\nПрограмма будет завершена");
                    return false;
                }
                homeController.SetCurrentPerson(userName);
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

            if (!Login()) return false;
            Greet();

            return true;
        }
        #endregion
        #region bool ShowActionsAndSelect()
        /// <summary>
        /// Вывод в выбор действий
        /// </summary>
        /// <returns>True - действие выполнено</returns>
        private bool ShowActionsAndSelect()
        {
            Console.Write("Выберите желаемое действие ");
            switch (homeController.CurrentPerson)
            {
                #region Manager Actions         
                case Manager:
                    Console.WriteLine("[1-5]:");
                    Console.WriteLine("[1] - Добавить сотрудника");
                    Console.WriteLine("[2] - Добавить время сотруднику:");
                    Console.WriteLine("[3] - Отчет за период по всем сотрудникам");
                    Console.WriteLine("[4] - Отчет за период по одному сотруднику");
                    Console.WriteLine("[5] - Выход");
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
                            if (!IsAddTime()) break;
                            homeController.AddTime(homeController.GetPeopleNames()[num - 1], date, hours, message);
                            break;
                        #endregion
                        #region case "3"
                        case "3":
                            if (!IsSelectPeriod()) break;
                            GeneralReportData[] grData = homeController.GetReportForAllPersons(startDate, endDate);
                            Console.WriteLine($"Отчет за период с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}");
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
                            if (!IsAddTime()) break;
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
        #endregion

        #region bool IsSelectPerson()
        /// <summary>
        /// Предлагает выбор сотрудника из списка
        /// </summary>
        /// <returns>True - если сотрудник выбран</returns>
        bool IsSelectPerson()
        {
            Console.WriteLine($"Выберите сотрудника из списка: [1-{homeController.GetPeopleNames().Length}]");

            for (int i = 0; i < homeController.GetPeopleNames().Length; i++)
                Console.WriteLine($"[{i + 1}] - {homeController.GetPeopleNames()[i]}");

            num = int.Parse(Console.ReadLine().Trim());

            if (num < 1 || num > homeController.GetPeopleNames().Length)
            {
                PrintInputNumError();
                return false;
            }
            return true;
        }
        #endregion
        #region bool IsSelectPeriod()
        /// <summary>
        /// Предлагает выбор временного отрезка в формате [dd:mm:yyyy - dd:mm:yyyy]
        /// </summary>
        /// <returns>True - если период введен корректно</returns>
        bool IsSelectPeriod()
        {
            Console.Write("Введите дату начала периода в формате dd.mm.yyyy: ");
            if (!CheckDateFormat(Console.ReadLine().Trim(), out startDate))
                return false;
            Console.Write("Введите дату окончания периода в формате dd.mm.yyyy: ");
            if (!CheckDateFormat(Console.ReadLine().Trim(), out endDate))
                return false;
            if (startDate > endDate)
            {
                Console.WriteLine("Дата начала не может быть раньше, чем дата окончания");
                return false;
            }
            return true;
        }
        #endregion
        #region bool IsAddPerson()
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <returns>True - сотрудник добавлен</returns>
        bool IsAddPerson()
        {
            Console.WriteLine("Выберите должность добавляемого сотрудника: [1-3]");
            Console.WriteLine($"[1] - Руководитель");
            Console.WriteLine($"[2] - Сотрудник на зарплате");
            Console.WriteLine($"[3] - Фрилансер");

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
        #endregion
        #region bool IsExit()
        /// <summary>
        /// Выход из программы
        /// </summary>
        /// <returns>True - если пользователь подтвердил выход</returns>
        bool IsExit()
        {
            Console.WriteLine("Вы уверены, что хотите выйти?\nНажмите (Д)а для подтверждения, (Н)ет - для отмены");
            return Console.ReadLine().Trim().Equals("Д", StringComparison.OrdinalIgnoreCase);
        }
        #endregion
        #region bool IsAddTime()
        /// <summary>
        /// Добавление времени сотруднику
        /// </summary>
        /// <returns>True - время добавлено</returns> 
        bool IsAddTime()
        {
            Console.Write("Введите дату в формате dd.mm.yyyy: ");
            if (!CheckDateFormat(Console.ReadLine().Trim(), out date))
                return false;
            Console.Write("Кол-во часов работы: ");
            hours = byte.Parse(Console.ReadLine().Trim());
            if (hours > 24)
            {
                Console.WriteLine("Кол-во часов не может быть больше, чем 24");
                return false;
            }

            Console.Write("Текст сообщения: ");
            message = Console.ReadLine().Trim();

            return true;
        }
        #endregion
        #region bool CheckDateFormat(string value, out DateTime date)
        /// <summary>
        /// Проверка конвертирования даты в число
        /// </summary>
        /// <param name="value">строковое значение даты</param>
        /// <param name="date">полученное значение после преобразования</param>
        /// <returns>True - если преобразование прошо успешно</returns>
        bool CheckDateFormat(string value, out DateTime date)
        {
            try
            {
                date = DateTime.Parse(value);
                return true;
            }
            catch
            {
                ConsoleColor tmpColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверный формат вводимой даты");
                Console.ForegroundColor = tmpColor;
                date = DateTime.MinValue;
                return false;
            }
        }
        #endregion
        #region void PrintPersonalReport(PersonalReportData prData)
        /// <summary>
        /// Вывод отчета по сотруднику
        /// </summary>
        /// <param name="prData">Данные для отчета</param>
        void PrintPersonalReport(PersonalReportData prData)
        {
            Console.WriteLine($"Отчет по сотруднику {prData.Name} \nза период с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}:");
            foreach (var item in prData.TimeRecords)
                Console.WriteLine($"{item.Date.ToShortDateString()}, {item.Hours} часов, {item.Mesasge}");
            Console.WriteLine($"Итого: {prData.TotalHours} часов, заработано: {prData.TotalPay} рублей");
        }
        #endregion
        #region void PrintInputNumError()
        /// <summary>
        /// Вывод ошибки неправильно выбранного действия
        /// </summary>
        void PrintInputNumError()
        {
            ConsoleColor tmpColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка ввода!");
            Console.ForegroundColor = tmpColor;
        }
        #endregion
    }
}
