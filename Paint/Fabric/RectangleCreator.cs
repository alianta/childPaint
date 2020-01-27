﻿using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Fabric
{
    class RectangleCreator : FigureCreator
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

        private Figure CreateSquere(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
            figurePoints.Add(pStart);
            double xc = (pStart.X + pFinish.X) / 2;
            double yc = (pStart.Y + pFinish.Y) / 2;

            double dx = (pFinish.X - pStart.X) / 2;
            double dy = (pFinish.Y - pStart.Y) / 2;

            figurePoints.Add(new Point(xc + dy, yc - dx));
            figurePoints.Add(pFinish);
            figurePoints.Add(new Point(xc - dy, yc + dx));
            figurePoints.Add(pStart);


            return new Rectangle(figurePoints);
        }
    }
}
