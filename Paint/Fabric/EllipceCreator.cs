using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Fabric
{
    class EllipceCreator : FigureCreator
    {

        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            if (shiftPressed)
            {
                return CreateCircle(pStart, pFinish);
            }

            return CreateEllipce(pStart, pFinish);
        }

        private Figure CreateCircle(Point pStart, Point pFinish)
        {
            List<Point> listOfPixels = new List<Point>();
            int R = (int)Math.Sqrt((Math.Pow((pFinish.X - pStart.X), 2)) + (Math.Pow((pFinish.Y - pStart.Y), 2)));
            double a = Math.Sqrt(2) / 2;

            for (int i = 0; i <= (int)(a * R); i++)
            {
                int newY1 = (int)(pStart.Y - Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(pStart.X + i, newY1));
            }
            for (int i = (int)(a * R); i > 0; i--)
            {
                int newX2 = (int)(pStart.X + Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(newX2, pStart.Y - i));
            }
            for (int i = 0; i <= (int)(a * R); i++)
            {
                int newX2 = (int)(pStart.X + Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(newX2, pStart.Y + i));
            }
            for (int i = (int)(a * R); i > 0; i--)
            {
                int newY2 = (int)(pStart.Y + Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(pStart.X + i, newY2));
            }
            for (int i = 0; i <= (int)(a * R); i++)
            {
                int newY2 = (int)(pStart.Y + Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(pStart.X - i, newY2));
            }
            for (int i = (int)(a * R); i > 0; i--)
            {
                int newX1 = (int)(pStart.X - Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(newX1, pStart.Y + i));
            }
            for (int i = 0; i <= (int)(a * R); i++)
            {
                int newX1 = (int)(pStart.X - Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(newX1, pStart.Y - i));
            }
            for (int i = (int)(a * R); i > 0; i--)
            {
                int newY1 = (int)(pStart.Y - Math.Sqrt(R * R - i * i));
                listOfPixels.Add(new Point(pStart.X - i, newY1));
            }
            return new Ellipce(listOfPixels);
        }

        private Figure CreateEllipce(Point pStart, Point pFinish)
        {
            List<Point> listOfPixels = new List<Point>();
            double a = (pFinish.X > pStart.X) ? pFinish.X - pStart.X : pStart.X - pFinish.X;
            double b = (pFinish.Y > pStart.Y) ? pFinish.Y - pStart.Y : pStart.Y - pFinish.Y;
            double aInPowerTwo = a * a;
            double bInPowerTwo = b * b;

            for (double i = 0; i < (Math.Sqrt(2) / 2) * a; i++)
            {
                int newY2 = (int)(pStart.Y - Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                listOfPixels.Add(new Point(pStart.X + i, newY2));
            }
            for (double i = (Math.Sqrt(2) / 2) * b; i > 0; i--)
            {
                int newX1 = (int)(pStart.X + Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                listOfPixels.Add(new Point(newX1, pStart.Y - i));
            }
            for (double i = 0; i < (Math.Sqrt(2) / 2) * b; i++)
            {
                int newX1 = (int)(pStart.X + Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                listOfPixels.Add(new Point(newX1, pStart.Y + i));
            }
            for (double i = (Math.Sqrt(2) / 2) * a; i > 0; i--)
            {
                int newY1 = (int)(pStart.Y + Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                listOfPixels.Add(new Point(pStart.X + i, newY1));
            }
            for (double i = 0; i < (Math.Sqrt(2) / 2) * a; i++)
            {
                int newY1 = (int)(pStart.Y + Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                listOfPixels.Add(new Point(pStart.X - i, newY1));
            }
            for (double i = (Math.Sqrt(2) / 2) * b; i > 0; i--)
            {
                int newX2 = (int)(pStart.X - Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                listOfPixels.Add(new Point(newX2, pStart.Y + i));
            }
            for (double i = 0; i < (Math.Sqrt(2) / 2) * b; i++)
            {
                int newX2 = (int)(pStart.X - Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                listOfPixels.Add(new Point(newX2, pStart.Y - i));
            }
            for (double i = (Math.Sqrt(2) / 2) * a; i > 0; i--)
            {
                int newY2 = (int)(pStart.Y - Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                listOfPixels.Add(new Point(pStart.X - i, newY2));
            }
            return new Ellipce(listOfPixels);
        }
    }
}