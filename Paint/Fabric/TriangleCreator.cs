using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Fabric
{
    class TriangleCreator : FigureCreator
    {
        private TriangleType currentType;

        public TriangleCreator(TriangleType triangleType = TriangleType.Isoscaled)
        {
            currentType = triangleType;
        }

        public override Figure CreateFigure(Point pStart, Point pFinish)
        {
            switch (currentType)
            {
                case TriangleType.Rectangular:
                    return CreateRectangularTriangle(pStart, pFinish);
                default:
                    return CreateIsoscaleTriangle(pStart, pFinish);
            }
        }

        private Figure CreateIsoscaleTriangle(Point pStart, Point pFinish)
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

            Point pointB = new Point();
            pointB.Y = pStart.Y + h;
            pointB.X = pStart.X;

            figurePoints.Add(pointB);

            Point pointC = new Point();
            pointC.Y = pointB.Y;
            pointC.X = pointB.X + w;

            figurePoints.Add(pointC);

            return new Triangle(figurePoints);
        }

        private Figure CreateRectangularTriangle(Point pStart, Point pFinish)
        {
            return new Triangle();
        }
    }
}
