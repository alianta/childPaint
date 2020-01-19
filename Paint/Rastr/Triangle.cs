using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class Triangle : Figure
    {
        static byte[] colorData = { 0, 0, 0, 255 };
        private Figure line = new Line();
        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish,int  thickness,  bool shift)
        {
            {
                if (shift)
                {
                    DrawTriangle(wb, pStart, pFinish, thickness,shift);
                }
                else
                {
                    DrawIsoscelesTriangle(wb, pStart, pFinish, thickness,  shift);
                }

            }
        }

        /// <summary>
        /// Рисование прямоугольного треугольника попиксельноKeyboard.IsKeyDown(Key.LeftShift)
        /// </summary>
        /// <param name="pStart">Начальная точка по клику</param>
        /// <param name="pFinish">Конечная точка по клику</param>
        private void DrawTriangle(WriteableBitmap wb, Point pStart, Point pFinish,int thickness, bool shift)
        {
            double w = Math.Abs(pFinish.X - pStart.X);
            double h = Math.Abs(pFinish.Y - pStart.Y);

            if (pStart.X > pFinish.X)
            {
                w *= -1;
            }

            if (pStart.Y > pFinish.Y)
            {
                h *= -1;
            }
            Point pointB = new Point();
            pointB.Y = pStart.Y + h;
            pointB.X = pStart.X;

            Point pointC = new Point();
            pointC.Y = pointB.Y;
            pointC.X = pointB.X + w;

            line.Draw(wb, pStart, pointC, thickness, shift);
            line.Draw(wb, pStart, pointB, thickness, shift);
            line.Draw(wb, pointB, pointC, thickness, shift);
        }

        /// <summary>
        /// Рисование равнобедренного треугольника попиксельно
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <param name="shift"></param>
        private void DrawIsoscelesTriangle(WriteableBitmap wb, Point pStart, Point pFinish, int thickness, bool shift)
        {
            double w = Math.Abs(pFinish.X - pStart.X);

            if (pFinish.X < pStart.X)
            {
                w *= -1;
            }

            Point pointB = new Point();
            pointB.X = pFinish.X - (w * 2);
            pointB.Y = pFinish.Y;

            line.Draw(wb, pStart, pFinish, thickness, shift);
            line.Draw(wb, pStart, pointB, thickness,  shift);
            line.Draw(wb, pointB, pFinish, thickness, shift);
        }
    }
}
