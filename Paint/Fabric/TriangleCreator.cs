using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Fabric
{
    public class TriangleCreator : FigureCreator
    {
        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            if (shiftPressed)
            {
                return CreateRectangularTriangle(pStart, pFinish);
            }

            return CreateIsoscaleTriangle (pStart, pFinish);
        }

        /// <summary>
        /// равнобедренный треугольник
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <returns></returns>
        public Figure CreateIsoscaleTriangle(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
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

            figurePoints.Add(pStart);
            figurePoints.Add(new Point((int)(pStart.X + w), pFinish.Y));
            figurePoints.Add(pStart);
            figurePoints.Add(new Point(pStart.X, (int)(pStart.Y + h)));
            figurePoints.Add(new Point(pStart.X, (int)(pStart.Y + h)));
            figurePoints.Add(new Point((int)(pStart.X + w), pFinish.Y));

            return new Triangle(figurePoints);
        }

        public Figure CreateRectangularTriangle(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
            double w = Math.Abs(pFinish.X - pStart.X);

            if (pFinish.X < pStart.X)
            {
                w *= -1;
            }

            figurePoints.Add(pStart);
            figurePoints.Add(pFinish);
            figurePoints.Add(pStart);
            figurePoints.Add(new Point((int)(pFinish.X - (w * 2)), pFinish.Y));
            figurePoints.Add(new Point((int)(pFinish.X - (w * 2)), pFinish.Y));
            figurePoints.Add(pFinish);

            return new Triangle(figurePoints);
        }
    }
}
