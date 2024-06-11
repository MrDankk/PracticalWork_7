using System;

namespace PracticalWork_7
{
    struct Worker
    {
        /// <summary>
        /// Индекс
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Время создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Ф.И.О.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        public string BirthPlace { get; set; }
    }
}
