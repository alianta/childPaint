using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint
{
    class DrawByLine : IDrawer
    {
        public Brush CurrentBrush { set; get; }
        public void Draw(List<Point> figurePoints)
        {

            for (int i = 0; i < figurePoints.Count - 1; i++)
            {
                DrawLine(figurePoints[i], figurePoints[i + 1]);
            }
        }


        // ТОЛСТАЯ ОТРИСОВКА ФИГУР
        //public void Draw(List<Point> figurePoints, WriteableBitmap wb)
        //{
        //    List<Point> first = new List<Point>();
        //    List<Point> second = new List<Point>();
        //    for (int i = 0; i < figurePoints.Count - 1; i += 2)
        //    {
        //        first.AddRange(CurrentBrush.BrushThickness.GetPoints(figurePoints[i]));
        //        second.AddRange(CurrentBrush.BrushThickness.GetPoints(figurePoints[i + 1]));
        //        for (int j = 0; j < first.Count - 1; j++)
        //        {
        //            DrawLine(wb, first[j], second[j]);
        //        }
        //    }
        //}


        /// <summary>
        /// Рисование линии попиксельно
        /// </summary>
        /// <param name="wb">Холст</param>
        /// <param name="pStart">Начальная точка</param>
        /// <param name="pFinish">конечная точка</param>
        public void DrawLine(Point pStart, Point pFinish) //переписать в private как все фигуры передалем не предыдущий метод с толщиой (в фигурах должен вызываться предыдущий метод с толщиной)
        {
            if (pFinish.X >= pStart.X && pFinish.Y >= pStart.Y)//прямая начинается с права с верху в низ
            {
                DrawLineInThirdTourthQuarters(pStart, pFinish);
            }
            else if (pFinish.X <= pStart.X && pFinish.Y <= pStart.Y)//прямая начинается с права с низу в верх
            {
                DrawLineInThirdTourthQuarters(pFinish, pStart);
            }
            else if (pFinish.X > pStart.X && pFinish.Y < pStart.Y)//прямая начинается с лева с низу в верх
            {
                DrawLineInFirstSecondQuarters(pFinish, pStart);
            }
            else if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)//прямая начинается с лева с верху в низ
            {
                DrawLineInFirstSecondQuarters(pStart, pFinish);
            }
        }

        /// <summary>
        /// Метод риcует линию по двум координатам дял 3 и 4 четвертей 
        /// </summary>
        /// <param name="pStart">Point точка старта</param>
        /// <param name="pFinish">Point точка финиша</param>
        private void DrawLineInThirdTourthQuarters(Point pStart, Point pFinish)
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
                    Pixel.Draw(newP, CurrentBrush.BrushColor.HexToRGBConverter());
                }
            }
            else
            {
                double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {
                    newP.X = (k * i + pStart.X);
                    newP.Y = i + pStart.Y;
                    Pixel.Draw(newP, CurrentBrush.BrushColor.HexToRGBConverter());
                }
            }
        }

        /// <summary>
        /// Метод римует линию по по двум координатам для 1 и 2 четвертей
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        private void DrawLineInFirstSecondQuarters(Point pStart, Point pFinish)
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
                    Pixel.Draw( newP, CurrentBrush.BrushColor.HexToRGBConverter());
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
                    Pixel.Draw( newP, CurrentBrush.BrushColor.HexToRGBConverter());
                }
            }
        }
    }
}
