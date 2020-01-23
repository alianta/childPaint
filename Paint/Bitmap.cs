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
    public class Bitmap
    {
        /// <summary>
        /// Основной объект, в котором будет храниться уникальный экземпляр класса. 
        /// </summary>
        private static Bitmap _instance;

        /// <summary>
        /// Данные, используемые в классе.
        /// </summary>
        private WriteableBitmap wb = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgra32, null);

        /// <summary>
        /// Защищенный конструктор для инициализации единственного экземпляра класса.
        /// </summary>
        /// <param name="bmt">Данные, используемые в классе.</param>
        private Bitmap(WriteableBitmap bmt)
        {
            wb = bmt;
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="data">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static Bitmap GetBitmap(WriteableBitmap wb)
        {
            _instance = new Bitmap(wb);
            return _instance;
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="data">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static Bitmap GetInstance(WriteableBitmap wb)
        {
            // Если экземпляр еще не инициализирован - выполняем инициализацию. 
            // Иначе возвращаем имеющийся экземпляр.
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new Bitmap(wb);
                }
              
            }

            return _instance;
        }

    }
}
