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
        private Figure pixel = new Pixel();
        private byte[] colorData;
        int thickness;

        public Line() { }

        public Line(byte[] colorData, int thickness)
        {
            this.colorData = colorData;
            this.thickness = thickness;
        }

        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, int thickness, bool shift)///запускается это
        {
           // SetPixel(curPoint, colorData);
            pixel.Draw(wb, pFinish, colorData);
            if (thickness == 1)
            {
                Draw(wb, pStart, pFinish, false);
            }
            if (thickness == 2)
            {
                Draw(wb, pStart, pFinish, false);
                Draw(wb, new Point(pStart.X - 1, pStart.Y), new Point(pFinish.X - 1, pFinish.Y),false);
                Draw(wb, new Point(pStart.X, pStart.Y - 1), new Point(pFinish.X, pFinish.Y - 1),false);
                Draw(wb, new Point(pStart.X - 1, pStart.Y - 1), new Point(pFinish.X - 1, pFinish.Y - 1),false);
            }
            else if (thickness == 3)
            {
                Draw(wb, pStart, pFinish, false);
                Draw(wb, new Point(pStart.X - 1, pStart.Y), new Point(pFinish.X - 1, pFinish.Y), false);
                Draw(wb, new Point(pStart.X + 1, pStart.Y), new Point(pFinish.X + 1, pFinish.Y), false);
                Draw(wb, new Point(pStart.X, pStart.Y - 1), new Point(pFinish.X, pFinish.Y - 1), false);
                Draw(wb, new Point(pStart.X, pStart.Y + 1), new Point(pFinish.X, pFinish.Y + 1), false);                
            }
            else if (thickness == 4)
            {
                Draw(wb, pStart, pFinish, false);

                Draw(wb, new Point(pStart.X - 1, pStart.Y), new Point(pFinish.X - 1, pFinish.Y), false);
                Draw(wb, new Point(pStart.X + 1, pStart.Y), new Point(pFinish.X + 1, pFinish.Y), false);
                Draw(wb, new Point(pStart.X + 2, pStart.Y), new Point(pFinish.X + 2, pFinish.Y), false);
                Draw(wb, new Point(pStart.X - 1, pStart.Y - 1), new Point(pFinish.X - 1, pFinish.Y - 1), false);
                Draw(wb, new Point(pStart.X, pStart.Y - 1), new Point(pFinish.X, pFinish.Y - 1), false);
                Draw(wb, new Point(pStart.X + 1, pStart.Y - 1), new Point(pFinish.X + 1, pFinish.Y - 1), false);
                Draw(wb, new Point(pStart.X + 2, pStart.Y - 1), new Point(pFinish.X + 2, pFinish.Y - 1), false);
                Draw(wb, new Point(pStart.X, pStart.Y + 1), new Point(pFinish.X, pFinish.Y + 1), false);
                Draw(wb, new Point(pStart.X + 1, pStart.Y + 1), new Point(pFinish.X + 1, pFinish.Y + 1), false);
                Draw(wb, new Point(pStart.X, pStart.Y + 2), new Point(pFinish.X, pFinish.Y + 2), false);
                Draw(wb, new Point(pStart.X + 1, pStart.Y + 2), new Point(pFinish.X + 1, pFinish.Y + 2), false);
                // DrawLine(new Point(prevPoint.X, prevPoint.Y), new Point(curPoint.X, curPoint.Y));
                //DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                //DrawLine(new Point(prevPoint.X + 1, prevPoint.Y), new Point(curPoint.X + 1, curPoint.Y));
                //DrawLine(new Point(prevPoint.X + 2, prevPoint.Y), new Point(curPoint.X + 2, curPoint.Y));
                //DrawLine(new Point(prevPoint.X - 1, prevPoint.Y - 1), new Point(curPoint.X - 1, curPoint.Y - 1));
                //DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));

                //DrawLine(new Point(prevPoint.X + 1, prevPoint.Y - 1), new Point(curPoint.X + 1, curPoint.Y - 1));
                //DrawLine(new Point(prevPoint.X + 2, prevPoint.Y - 1), new Point(curPoint.X + 2, curPoint.Y - 1));
                //DrawLine(new Point(prevPoint.X, prevPoint.Y + 1), new Point(curPoint.X, curPoint.Y + 1));//
                //DrawLine(new Point(prevPoint.X + 1, prevPoint.Y + 1), new Point(curPoint.X + 1, curPoint.Y + 1));
                //DrawLine(new Point(prevPoint.X, prevPoint.Y + 2), new Point(curPoint.X, curPoint.Y + 2));
                //DrawLine(new Point(prevPoint.X + 1, prevPoint.Y + 2), new Point(curPoint.X + 1, curPoint.Y + 2));
            }
        }
        /// <summary>
        /// Рисование линии попиксельно
        /// </summary>
        /// <param name="wb">Холст</param>
        /// <param name="pStart">Начальная точка</param>
        /// <param name="pFinish">конечная точка</param>
        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift) //переписать в private как все фигуры передалем не предыдущий метод с толщиой (в фигурах должен вызываться предыдущий метод с толщиной)
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
                    pixel.Draw(wb, newP, colorData);
                }
            }
            else
            {
                double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {
                    newP.X = (k * i + pStart.X);
                    newP.Y = i + pStart.Y;
                    pixel.Draw(wb, newP, colorData);
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
                    pixel.Draw(wb, newP, colorData);
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
                    pixel.Draw(wb, newP, colorData);
                }
            }
        }

    }
}
