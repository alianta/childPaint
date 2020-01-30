using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using Rectangle = Paint.Rastr.Rectangle;

namespace Paint.Fabric
{
  public class RectangleCreator : FigureCreator
    {
        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            if (shiftPressed)
            {
                return CreateSquere(pStart, pFinish);
            }

            return CreateIsoscaleRectangle(pStart, pFinish);
        }

        private Figure CreateIsoscaleRectangle(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
            figurePoints.Add(pStart);
            figurePoints.Add(new Point(pStart.X, pFinish.Y));
            figurePoints.Add(pFinish);
            figurePoints.Add(new Point(pFinish.X, pStart.Y));
            figurePoints.Add(pStart);

            return new Rectangle(figurePoints);
        }

        public Figure CreateSquere(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
            figurePoints.Add(pStart);

            Point pointA = new Point();
            pointA.X = pFinish.X;
            pointA.Y = pStart.Y;

            Point pointB = new Point();
            pointB.X = pStart.X;
            pointB.Y = pStart.Y + (pFinish.X - pStart.X);

            pFinish.Y = pStart.Y + (pFinish.X - pStart.X);

            figurePoints.Add(pointA);
            figurePoints.Add(pFinish);
            figurePoints.Add(pointB);


            return new Rectangle(figurePoints);
        }
    }
}
