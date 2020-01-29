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
        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            List<Point> figurePoints = new List<Point>();

            figurePoints.Add(pStart);
            figurePoints.Add(pFinish);

            return new Line(figurePoints);
        }
    }
}
