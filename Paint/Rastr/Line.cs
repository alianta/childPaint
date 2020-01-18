using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class Line : Figure
    {
        static byte[] colorData = { 0, 0, 0, 255 };

        /// <summary>
        /// Рисование линии попиксельно
        /// </summary>
        /// <param name="wb">Холст</param>
        /// <param name="pStart">Начальная точка</param>
        /// <param name="pFinish">конечная точка</param>
        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            if (pFinish.X >= pStart.X && pFinish.Y >= pStart.Y)//прямая начинается с права с верху в низ
            {
                DrawLineInThirdTourthQuarters(wb, pStart, pFinish);
            }
            else if (pFinish.X <= pStart.X && pFinish.Y <= pStart.Y)//прямая начинается с права с низу в верх
            {
                DrawLineInThirdTourthQuarters(wb, pFinish, pStart);
            }
            else if (pFinish.X > pStart.X && pFinish.Y < pStart.Y)//прямая начинается с лева с низу в верх
            {
                DrawLineInFirstSecondQuarters(wb, pFinish, pStart);
            }
            else if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)//прямая начинается с лева с верху в низ
            {
                DrawLineInFirstSecondQuarters(wb, pStart, pFinish);
            }
        }

        /// <summary>
        /// Метод риcует линию по двум координатам дял 3 и 4 четвертей 
        /// </summary>
        /// <param name="pStart">Point точка старта</param>
        /// <param name="pFinish">Point точка финиша</param>
        private void DrawLineInThirdTourthQuarters(WriteableBitmap wb, Point pStart, Point pFinish)
        {
            Point newP = new Point();
            double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
            double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;

            if (deltaX > deltaY)
            {
                double k = deltaY / deltaX;
                for (int i = 0; i < deltaX; i++)
                {
                    newP.Y = (k * i + pStart.Y);
                    newP.X = i + pStart.X;
                    SetPixel(wb, newP, colorData);
                }
            }
            else
            {
                double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {
                    newP.X = (k * i + pStart.X);
                    newP.Y = i + pStart.Y;
                    SetPixel(wb, newP, colorData);
                }
            }
        }

        /// <summary>
        /// Метод римует линию по по двум координатам для 1 и 2 четвертей
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        private void DrawLineInFirstSecondQuarters(WriteableBitmap wb, Point pStart, Point pFinish)
        {
            Point newP = new Point();
            double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
            double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;

            if (deltaX > deltaY)
            {
                double k = deltaY / deltaX;
                for (int i = 0; i < deltaX; i++)
                {
                    newP.Y = (k * i + pStart.Y);
                    newP.X = pStart.X - i;
                    SetPixel(wb, newP, colorData);
                }
            }
            else
            {
                if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)
                {
                    pStart = pFinish;
                }
                double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {

                    newP.X = (k * i + pStart.X);
                    newP.Y = pStart.Y - i;
                    SetPixel(wb, newP, colorData);
                }
            }
        }

        ///// <summary>
        ///// Метод устанавливает пиксель в заданных координатах в заданном цвете
        ///// </summary>
        ///// <param name="p">Point. Координаты точки</param>
        ///// <param name="colorData">Массив. Цвет в aRGB</param>
        //public void SetPixel(WriteableBitmap wb, Point p, byte[] colorData)
        //{
        //    if (p.X > 0 && p.X < (int)wb.Width && p.Y > 0 && p.Y < (int)wb.Height)
        //    {
        //        Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
        //        wb.WritePixels(rect, colorData, 4, 0);
        //        //   curState.WritePixels(rect, colorData, 4, 0);
        //        //MainImage.Source = wb; !!!!! не надо подменять, тк рисовалка уже указывает на этот холст, а при очистка надо пересоздавать !!!!!! (с) Иван
        //    }
        //}
    }
}
