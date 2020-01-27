﻿using Paint.Rastr;
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
        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            if (shiftPressed)
            {
                return CreateIsoscaleTriangle(pStart, pFinish);
            }

            return CreateRectangularTriangle(pStart, pFinish);
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
            figurePoints.Add(new Point(pStart.X + w, pFinish.Y));
            figurePoints.Add(pStart);
            figurePoints.Add(new Point(pStart.X, pStart.Y + h));
            figurePoints.Add(new Point(pStart.X, pStart.Y + h));
            figurePoints.Add(new Point(pStart.X + w, pFinish.Y));

            return new Triangle(figurePoints);
        }

        private Figure CreateRectangularTriangle(Point pStart, Point pFinish)
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
            figurePoints.Add(new Point(pFinish.X - (w * 2), pFinish.Y));
            figurePoints.Add(new Point(pFinish.X - (w * 2), pFinish.Y));
            figurePoints.Add(pFinish);

            return new Triangle(figurePoints);
        }
    }
}
