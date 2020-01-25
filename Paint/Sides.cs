using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public class Sides
    {
        /// <summary>
        /// Основной объект, в котором будет храниться уникальный экземпляр класса. 
        /// </summary>
        private static Sides _instance;

        /// <summary>
        /// Данные, хранимые в классе.
        /// </summary>
        private int n;

        /// <summary>
        /// Данные, используемые в классе.
        /// </summary>
        private int N => n;

        /// <summary>
        /// Защищенный конструктор для инициализации единственного экземпляра класса.
        /// </summary>
        /// <param name="wb">Данные, используемые в классе.</param>
        private Sides(int sides)
        {
            n = sides;
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="wb">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static Sides GetSides(int sides)
        {
            if (_instance == null)
            {
                _instance = new Sides(sides);
            }
            return _instance;
        }

        /// <summary>
        /// Перезаписать экземпляр класса.
        /// </summary>
        /// <param name="data">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static Sides SetSides(int sides)
        {
            _instance.n = sides;
            return _instance;
        }

        /// <summary>
        /// Приведение объекта к bitmap.
        /// </summary>
        /// <returns>Данные класса в строковом формате.</returns>
        public int GetCountSides()
        {
            return N;
        }
    }
}
