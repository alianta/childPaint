using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Fabric
{
    class StraightLineCreator : FigureCreator
    {
        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            List<Point> figurePoints = new List<Point>();

            figurePoints.Add(pStart);
            figurePoints.Add(pFinish);

            return new StraightLine(figurePoints);
        }
    }
}
