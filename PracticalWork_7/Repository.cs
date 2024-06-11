using System;
using System.IO;
using System.Linq;

namespace PracticalWork_7
{
    struct Repository
    {
        private string path;

        /// <summary>
        /// Выбор пути к файлу
        /// </summary>
        public string Path
        {
            set { this.path = value; }
            get { return this.path; }
        }

        /// <summary>
        /// Получить информацию о всех сотрудниках
        /// </summary>
        /// <returns></returns>
        public void GetAllWorkers()
        {
            Worker[] workers = CreateArray();

            if(workers.Length > 0)
            {
                for (int i = 0; i < workers.Length; i++)
                { 
                    Print(workers[i]); 
                }
            }
            else
            {
                Console.WriteLine("Сотрудников нет");
            }
        }

        /// <summary>
        /// Найти сотрудника по ID
        /// </summary>
        /// <param name="id"> ID сотрудника </param>
        /// <returns></returns>
        public Worker GetWorkerById(int id)
        {
            Worker[] workers = CreateArray();
            Worker worker = new Worker();

            for (int i = 0; i < workers.Length; i++)
            {
                if(id == workers[i].ID)
                {
                    worker = workers[i];
                    break;
                }
            }
            return worker;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="Id"> ID сотрудника </param>
        public void DeleteWorker(int Id) 
        {
            Worker[] workers = CreateArray();

            int deleteWorkerIndex = 0;
            bool workerFound = false;

            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].ID == Id)
                {
                    deleteWorkerIndex = i;
                    workerFound = true;
                    break;
                }
            }

            if(workerFound)
            {
                File.Delete(path);

                for(int i = 0; i < workers.Length; i++)
                {
                    if(i != deleteWorkerIndex)
                    {
                        AddWorker(workers[i]);
                    }
                }
            }
            else
            {
                Console.WriteLine("Сотрудник не найден");
            }
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="worker"></param>
        public void AddWorker(Worker worker)
        {
            string line = string.Join("#",
                                      worker.ID, 
                                      worker.CreateDate, 
                                      worker.FullName, 
                                      worker.Age,
                                      worker.Height, 
                                      worker.DateOfBirth.ToShortDateString(),
                                      worker.BirthPlace);
            using(StreamWriter streamWriter = new StreamWriter(path, true))
            {
                if(line != "")
                { 
                    streamWriter.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Найти работников добавленных между двумя датами
        /// </summary>
        /// <returns></returns>
        public void GetWorkersBetweenTwoDates(DateTime dateFrom,DateTime dateTo) 
        {
            Worker[] workers = CreateArray();
            bool workerFound = false;

            for(int i = 0; i < workers.Length;i++)
            {
                if(workers[i].CreateDate >= dateFrom && workers[i].CreateDate <= dateTo)
                {
                    Print(workers[i]);
                    workerFound = true;
                }
            }

            if(workerFound == false)
            {
                Console.WriteLine("Таких сотрудников нет");
            }
        }

        /// <summary>
        /// Применение изменений сотрудника
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="oldId"></param>
        public void ApplyingChange(Worker worker, int oldId)
        {
            Worker[] workers = CreateArray();

            File.Delete(path);
            bool workerChange = false;

            for(int i = 0;i < workers.Length;i++)
            {
                if(workers[i].ID != oldId || workerChange == true)
                { 
                    AddWorker(workers[i]); 
                }
                else
                {
                    AddWorker(worker);
                    workerChange = true;
                }
            }
        }

        /// <summary>
        /// Получение массива сотрудников
        /// </summary>
        /// <returns></returns>
        public Worker[] GetArray()
        {
            Worker[] workers = CreateArray();
            return workers;
        }

        /// <summary>
        /// Вывод сотрудников в консоль
        /// </summary>
        /// <param name="worker"></param>
        public void Print(Worker worker)
        {
            string pattern = $"\nID - {worker.ID}" +
                             $"\nДата создания записи - {worker.CreateDate.ToShortDateString()}" +
                             $"\nФ.И.О. - {worker.FullName}" +
                             $"\nВозраст - {worker.Age}" +
                             $"\nРост - {worker.Height}" +
                             $"\nДата рождения - {worker.DateOfBirth.ToShortDateString()}" +
                             $"\nМесто рождения - {worker.BirthPlace}";
            Console.WriteLine(pattern);
        }


        /// <summary>
        /// Создание массива сотрудников
        /// </summary>
        /// <returns></returns>
        private Worker[] CreateArray()
        {
            int length = ArrayLength();
            Worker[] workers = new Worker[length];

            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                int currentIndex = 0;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] dataArray = line.Split('#');

                    workers[currentIndex].ID = int.Parse(dataArray[0]);
                    workers[currentIndex].CreateDate = DateTime.Parse(dataArray[1]);
                    workers[currentIndex].FullName = dataArray[2];
                    workers[currentIndex].Age = int.Parse(dataArray[3]);
                    workers[currentIndex].Height = int.Parse(dataArray[4]);
                    workers[currentIndex].DateOfBirth = DateTime.Parse(dataArray[5]);
                    workers[currentIndex].BirthPlace = dataArray[6];

                    currentIndex++;
                }
            }
            return workers;
        }

        /// <summary>
        /// Проверка количества строк в файле и установка длины массива
        /// </summary>
        /// <returns></returns>
        private int ArrayLength()
        {
            FileChecking();
            int length = 0;

            using (StreamReader sw = new StreamReader(path))
            {
                string line;
                while ((line = sw.ReadLine()) != null)
                {
                    length++;
                }

            }
            return length;
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        private void FileChecking()
        {
            if(!File.Exists(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                fileStream.Close();
            }
        }

        
    }
}
