using System;
using System.IO;

namespace PracticalWork_7
{
    internal class Program
    {
        static void Main()
        {
            Repository repository = new Repository();
            repository.Path = "Workers.txt";

            MenuSelection(repository);
        }

        /// <summary>
        /// Управление меню выбора
        /// </summary>
        /// <param name="repository"></param>
        static void MenuSelection(Repository repository)
        {
            bool menuActive = true;

            while (menuActive)
            {
                Console.Clear();
                Console.WriteLine("\n1.Посмотреть список всех сотрудников" +
                                  "\n2.Найти Сотрудника по ID" +
                                  "\n3.Удалить сотрудника" +
                                  "\n4.Добавить нового сотрудника" +
                                  "\n5.Найти сотрудников по дате добавления" +
                                  "\n6.Сортировка сотрудников" +
                                  "\n7.Изменение данных сотрудника" +
                                  "\n0.Выход");
                string input = InputString();

                switch (input)
                {
                    case "1":
                        Console.Clear();

                        repository.GetAllWorkers();

                        InputKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Введите ID сотрудника");

                        int inputIndex = InputInteger();
                        Worker worker = repository.GetWorkerById(inputIndex);

                        if (worker.CreateDate == new DateTime(0001, 01, 01))
                        {
                            Console.WriteLine("\nТакого сотрудника нет");
                        }
                        else 
                        { 
                            repository.Print(worker); 
                        }

                        InputKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Введите ID сотрудника");

                        inputIndex = InputInteger();
                        repository.DeleteWorker(inputIndex);

                        InputKey();
                        break;
                    case "4":
                        Console.Clear();

                        NewWorker(repository);

                        InputKey();
                        break;
                    case "5":
                        Console.Clear();

                        Console.WriteLine("Введите дату от которой необходимо искать");
                        DateTime dateFrom = InputDate();

                        Console.WriteLine("Введите дату до которой необходимо искать");
                        DateTime dateTo = InputDate();

                        repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);

                        InputKey();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("\n1.Сортировка по ID" +
                                          "\n2.Сортировка по дате рождения" +
                                          "\n3.Сортировка по дате добавления пользователя");

                        int sortInput = InputInteger();
                        Worker[] sortWorkers = repository.GetArray();

                        Console.Clear();
                        if(sortWorkers.Length > 0)
                        {
                            switch (sortInput)
                                {
                                case 1:
                                    sortWorkers = SortingById(repository, sortWorkers);
                                    break;
                                case 2:
                                    sortWorkers = SortingByDateOfBirth(repository, sortWorkers);
                                    break;
                                case 3:
                                    sortWorkers = SortingByDateOfCreate(repository, sortWorkers);
                                    break;
                                }

                            Console.WriteLine("\nСохранить результат? (д/н)");
                            string saveInput = InputString().ToLower();

                            switch (saveInput)
                            {
                                case "д":
                                    File.Delete(repository.Path);

                                    for (int i = 0; i < sortWorkers.Length; i++)
                                    {
                                        repository.AddWorker(sortWorkers[i]);
                                    }

                                    Console.WriteLine("\nФайл перезаписан");
                                    break;
                                default:
                                    Console.WriteLine("\nФайл остался без изменений");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Сортировать нечего т.к. файл пуст");
                        }

                        InputKey();
                        break;
                    case "7":
                        EditWorker(repository);
                        break;
                    case "0":
                        Console.Clear();
                        menuActive = false;
                        break;
                    default:
                        Console.WriteLine("Некоректный ввод");
                        break;
                }
            }
        }


        /// <summary>
        /// Ввод данных сотрудника
        /// </summary>
        /// <param name="repository"></param>
        static void NewWorker(Repository repository)
        {
            Worker worker = new Worker();

            Console.WriteLine("Введите ID сотрудника:");
            worker.ID = InputInteger();

            worker.CreateDate = DateTime.Now;

            Console.WriteLine("Введите Ф.И.О. сотрудника:");
            worker.FullName = InputString();

            Console.WriteLine("Введите возраст сотрудника:");
            worker.Age = InputInteger();

            Console.WriteLine("Введите рост сотрудника:");
            worker.Height = InputInteger();

            Console.WriteLine("Введите дату рождения сотрудника:");
            worker.DateOfBirth = InputDate();

            Console.WriteLine("Введите место рождения сотрудника:");
            worker.BirthPlace = InputString();

            repository.AddWorker(worker);
        }
        
        /// <summary>
        /// Изменение данных сотрудника
        /// </summary>
        /// <param name="repository"></param>
        static void EditWorker(Repository repository)
        {
            Console.Clear();
            Console.WriteLine("Введите ID сотрудника");

            int inputId = InputInteger();
            Worker worker = repository.GetWorkerById(inputId);
            int oldId = worker.ID;

            if (worker.CreateDate != new DateTime(0001, 01, 01))
            {
                Console.WriteLine("\nЧто необходимо изменить:" +
                                  "\n1.ID" +
                                  "\n2.Ф.И.О." +
                                  "\n3.Возраст" +
                                  "\n4.Рост" +
                                  "\n5.Дата рождения" +
                                  "\n6.Место рождения" +
                                  "\n0.Выход");
                int inputField = InputInteger();

                switch(inputField)
                {
                    case 1:
                        Console.WriteLine("Введите новый ID");
                        int newId = InputInteger();
                        worker.ID = newId;
                        break;
                    case 2:
                        Console.WriteLine("Введите новое Ф.И.О.");
                        string newName = InputString();
                        worker.FullName = newName;
                        break;
                    case 3:
                        Console.WriteLine("Введите новый возраст");
                        int newAge = InputInteger();
                        worker.Age = newAge;
                        break;
                    case 4:
                        Console.WriteLine("Введите новый рост");
                        int newHeight = InputInteger();
                        worker.Height = newHeight;
                        break;
                    case 5:
                        Console.WriteLine("Введите новую дату рождения");
                        DateTime newBirthDate = InputDate();
                        worker.DateOfBirth = newBirthDate;
                        break;
                    case 6:
                        Console.WriteLine("Введите новое место рождения");
                        string newBirthPlace = InputString();
                        worker.BirthPlace = newBirthPlace;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nТакого сотрудника нет");
            }

            repository.ApplyingChange(worker, oldId);

            InputKey();
        }


        /// <summary>
        /// Сортировка сотрудников по ID
        /// </summary>
        /// <param name="repository"></param>
        static Worker[] SortingById(Repository repository, Worker[] workers)
        {
            Worker[] sortWorkers = workers;

            for (int i = 0; i < sortWorkers.Length; i++)
            {
                Worker tempWorker = new Worker();

                for (int j = i + 1; j < sortWorkers.Length; j++)
                {
                    if (sortWorkers[i].ID > sortWorkers[j].ID)
                    {
                        tempWorker = sortWorkers[j];
                        sortWorkers[j] = sortWorkers[i];
                        sortWorkers[i] = tempWorker;
                    }
                }
                repository.Print(sortWorkers[i]);
            }
            return sortWorkers;
        }

        /// <summary>
        /// Сортировка по дню рождения
        /// </summary>
        /// <param name="repository"></param>
        static Worker[] SortingByDateOfBirth(Repository repository, Worker[] workers)
        {
            Worker[] sortWorkers = workers;

            for (int i = 0; i < sortWorkers.Length; i++)
            {
                Worker tempWorker = new Worker();

                for (int j = i + 1; j < sortWorkers.Length; j++)
                {
                    if (sortWorkers[i].DateOfBirth > sortWorkers[j].DateOfBirth)
                    {
                        tempWorker = sortWorkers[j];
                        sortWorkers[j] = sortWorkers[i];
                        sortWorkers[i] = tempWorker;
                    }
                }
                repository.Print(workers[i]);
            }

            return workers;
        }

        /// <summary>
        /// Сортировка по дате создания учетной записи
        /// </summary>
        /// <param name="repository"></param>
        static Worker[] SortingByDateOfCreate(Repository repository, Worker[] workers)
        {
            Worker[] sortWorkers = workers;

            for (int i = 0; i < sortWorkers.Length; i++)
            {
                Worker tempWorker = new Worker();

                for (int j = i + 1; j < sortWorkers.Length; j++)
                {
                    if (sortWorkers[i].CreateDate > sortWorkers[j].CreateDate)
                    {
                        tempWorker = sortWorkers[j];
                        sortWorkers[j] = sortWorkers[i];
                        sortWorkers[i] = tempWorker;
                    }
                }
                repository.Print(sortWorkers[i]);
            }

            return sortWorkers;
        }


        /// <summary>
        /// Проверка ввода даты
        /// </summary>
        /// <returns></returns>
        static DateTime InputDate()
        {
            DateTime dateTime;
            
            while(true)
            {
                string date = Console.ReadLine();

                if (DateTime.TryParse(date, out dateTime))
                { 
                    return dateTime; 
                }
                else 
                {
                    Console.WriteLine("Введена некоректная дата, попробуйте еще раз");
                }
            }

        }

        /// <summary>
        /// Проверка ввода числа
        /// </summary>
        /// <returns></returns>
        static int InputInteger()
        {
            while(true)
            {
                int number;
                string input = Console.ReadLine().Trim();
                if (Int32.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Введите число");
                }
            }
        }

        /// <summary>
        /// Проверка ввода строки
        /// </summary>
        /// <returns></returns>
        static string InputString()
        {
            string input = Console.ReadLine().Trim();

            if(input == "")
            {
                input = "Неизвестно";
            }

            return input;
        }

        /// <summary>
        /// Задержка показа информации
        /// </summary>
        static void InputKey()
        {
            Console.WriteLine("\nНажмите любую кнопку для продолжения");
            Console.ReadKey();
        }
    }
}
