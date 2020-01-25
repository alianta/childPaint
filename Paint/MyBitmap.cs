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
        private WriteableBitmap btm;

        /// <summary>
        /// Данные, используемые в классе.
        /// </summary>
        private WriteableBitmap Btm => btm;

        /// <summary>
        /// Защищенный конструктор для инициализации единственного экземпляра класса.
        /// </summary>
        /// <param name="wb">Данные, используемые в классе.</param>
        private MyBitmap(WriteableBitmap wb)
        {
            btm = wb;
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="wb">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static MyBitmap GetBitmap(WriteableBitmap wb)
        {
            if (_instance == null)
            {
                _instance = new MyBitmap(wb);
            }
            return _instance;
        }

        /// <summary>
        /// Перезаписать экземпляр класса.
        /// </summary>
        /// <param name="data">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static MyBitmap SetBitmap(WriteableBitmap wb)
        {
            _instance.btm = wb;
            return _instance;
        }

        /// <summary>
        /// Приведение объекта к bitmap.
        /// </summary>
        /// <returns>Данные класса в строковом формате.</returns>
        public WriteableBitmap GetWriteableBitmap()
        {
            return Btm;
        }

        internal static ImageSource GetBitmapCopy()
        {
            throw new NotImplementedException();
        }
    }
}