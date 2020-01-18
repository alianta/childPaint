﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class Ellipce : Figure
    {
        static byte[] colorData = { 0, 0, 0, 255 };

        /// <summary>
        /// Рисование эллипса и круга
        /// </summary>
        /// <param name="wb">Холст</param>
        /// <param name="pStart">Начальная точка</param>
        /// <param name="pFinish">конечная точка</param>
        /// /// <param name="shift">нажат ли shift</param>
        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            if (shift)
            {
                DrawCircle(wb, pStart, pFinish, shift);
                
            }
            else
            {
                DrawEllipce(wb, pStart, pFinish, shift);
            }
        }

        /// <summary>
        /// Рисование прямоугольного треугольника попиксельноKeyboard.IsKeyDown(Key.LeftShift)
        /// </summary>
        /// <param name="pStart">Начальная точка по клику</param>
        /// <param name="pFinish">Конечная точка по клику</param>
        private void DrawEllipce(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            //wb = new WriteableBitmap(curState);
            double a = (pFinish.X > pStart.X) ? pFinish.X - pStart.X : pStart.X - pFinish.X;
            double b = (pFinish.Y > pStart.Y) ? pFinish.Y - pStart.Y : pStart.Y - pFinish.Y;
            double aInPowerTwo = a * a;
            double bInPowerTwo = b * b;

            for (int i = 0; i < b; i++)
            {
                int newX1 = (int)(pStart.X + Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                SetPixel(wb, new Point(newX1, pStart.Y + i), colorData);
                SetPixel(wb, new Point(newX1, pStart.Y - i), colorData);

                int newX2 = (int)(pStart.X - Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                SetPixel(wb, new Point(newX2, pStart.Y + i), colorData);
                SetPixel(wb, new Point(newX2, pStart.Y - i), colorData);
            }
            for (int i = 0; i < a; i++)
            {
                int newY1 = (int)(pStart.Y + Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                SetPixel(wb, new Point(pStart.X + i, newY1), colorData);
                SetPixel(wb, new Point(pStart.X - i, newY1), colorData);

                int newY2 = (int)(pStart.Y - Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                SetPixel(wb, new Point(pStart.X + i, newY2), colorData);
                SetPixel(wb, new Point(pStart.X - i, newY2), colorData);
            }
        }

        /// <summary>
        /// Рисование окружности попиксельно
        /// </summary>
        /// <param name="pStart">Начальная точка по клику</param>
        /// <param name="pFinish">Конечная точка по клику</param>
        private void DrawCircle(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            //wb = new WriteableBitmap(curState);
            int R = (int)Math.Sqrt((Math.Pow((pFinish.X - pStart.X), 2)) + (Math.Pow((pFinish.Y - pStart.Y), 2)));
            double a = Math.Sqrt(2) / 2;

            for (int i = (int)(-a * R); i < (int)(a * R); i++)
            {
                int newY1 = (int)(pStart.Y - Math.Sqrt(R * R - i * i));

                SetPixel(wb, new Point(pStart.X + i, newY1), colorData);
                SetPixel(wb, new Point(pStart.X - i, newY1), colorData);

                int newY2 = (int)(pStart.Y + Math.Sqrt(R * R - i * i));
                SetPixel(wb, new Point(pStart.X + i, newY2), colorData);
                SetPixel(wb, new Point(pStart.X - i, newY2), colorData);
            }
            for (int i = (int)(-a * R); i < (int)(a * R); i++)
            {
                int newX1 = (int)(pStart.X - Math.Sqrt(R * R - i * i));

                SetPixel(wb, new Point(newX1, pStart.Y - i), colorData);
                SetPixel(wb, new Point(newX1, pStart.Y + i), colorData);
                int newX2 = (int)(pStart.X + Math.Sqrt(R * R - i * i));
                SetPixel(wb, new Point(newX2, pStart.Y + i), colorData);
                SetPixel(wb, new Point(newX2, pStart.Y - i), colorData);
            }
        }

    }
}
