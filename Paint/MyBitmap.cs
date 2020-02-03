using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paint
{
    /// <summary>
    /// Класс, реализующий паттерн Одиночка.
    /// </summary>
    /// <remarks>
    /// Порождающий паттерн, гарантирующий, что для класса будет создан только один единственный экземпляр.
    /// </remarks>
    public class MyBitmap
    {
        /// <summary>
        /// Основной объект, в котором будет храниться уникальный экземпляр класса. 
        /// </summary>
        private static MyBitmap _instance;


        /// <summary>
        /// Данные, хранимые в классе.
        /// </summary>
        public WriteableBitmap Btm { get; set; }
        public WriteableBitmap BtmCopy { get; set; }

        /// <summary>
        /// Защищенный конструктор для инициализации единственного экземпляра класса.
        /// </summary>
        /// <param name="wb">Данные, используемые в классе.</param>
        private MyBitmap()
        {
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="wb">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static MyBitmap GetBitmap()
        {
            if (_instance == null)
            {
                _instance = new MyBitmap();
            }
            return _instance;
        }

        /// <summary>
        /// Сохранили текщий в копию.
        /// </summary>
        public void CreateCopy()
        {
            BtmCopy = new WriteableBitmap(_instance.Btm);
        }

        /// <summary>
        /// Поменяли местами.
        /// </summary>
        public void SetBitmapToCopy()
        {
            _instance.Btm = new WriteableBitmap(_instance.BtmCopy);
        }
    }
}