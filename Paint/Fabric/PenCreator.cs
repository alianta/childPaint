using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Fabric
{
    class PenCreator : FigureCreator
    {
        public override Figure CreateFigure(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();

            if (pFinish.X >= pStart.X && pFinish.Y >= pStart.Y)//прямая начинается с права с верху в низ
            {
                figurePoints = DrawLineInThirdTourthQuarters(pStart, pFinish);
            }
            else if (pFinish.X <= pStart.X && pFinish.Y <= pStart.Y)//прямая начинается с права с низу в верх
            {
                figurePoints = DrawLineInThirdTourthQuarters(pFinish, pStart);
            }
            else if (pFinish.X > pStart.X && pFinish.Y < pStart.Y)//прямая начинается с лева с низу в верх
            {
                figurePoints = DrawLineInFirstSecondQuarters(pFinish, pStart);
            }
            else if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)//прямая начинается с лева с верху в низ
            {
                figurePoints = DrawLineInFirstSecondQuarters(pStart, pFinish);
            }

            return new Line(figurePoints);
        }

        /// <summary>
        /// Метод риcует линию по двум координатам дял 3 и 4 четвертей 
        /// </summary>
        /// <param name="pStart">Point точка старта</param>
        /// <param name="pFinish">Point точка финиша</param>
        private List<Point> DrawLineInThirdTourthQuarters(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
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

                    figurePoints.Add(newP);
                }
            }
            else
            {
                double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {
                    newP.X = (k * i + pStart.X);
                    newP.Y = i + pStart.Y;

                    figurePoints.Add(newP);
                }
            }

            return figurePoints;
        }

        /// <summary>
        /// Метод римует линию по по двум координатам для 1 и 2 четвертей
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        private List<Point> DrawLineInFirstSecondQuarters(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
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

                    figurePoints.Add(newP);
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

                    figurePoints.Add(newP);
                }
            }

            return figurePoints;
        }
    }
}
