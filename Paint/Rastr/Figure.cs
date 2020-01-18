using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    abstract class Figure
    {
        public virtual void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {

        }

        /// <summary>
        /// Метод устанавливает пиксель в заданных координатах в заданном цвете
        /// </summary>
        /// <param name="p">Point. Координаты точки</param>
        /// <param name="colorData">Массив. Цвет в aRGB</param>
        public void SetPixel(WriteableBitmap wb, Point p, byte[] colorData)
        {
            if (p.X > 0 && p.X < (int)wb.Width && p.Y > 0 && p.Y < (int)wb.Height)
            {
                Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
                wb.WritePixels(rect, colorData, 4, 0);
                //   curState.WritePixels(rect, colorData, 4, 0);
                //MainImage.Source = wb; !!!!! не надо подменять, тк рисовалка уже указывает на этот холст, а при очистка надо пересоздавать !!!!!! (с) Иван
            }
        }
    }
}
