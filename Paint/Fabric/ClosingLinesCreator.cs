using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Fabric
{
    class ClosingLinesCreator : FigureCreator
    {

        List<Point> allFigurePoints = new List<Point>();
        public override Figure CreateFigure(Point pStart, Point pFinish, bool isDoubleClicked)
        {
            List<Point> figurePoints = new List<Point>();
            figurePoints.Add(pStart);
            figurePoints.Add(pFinish);

            if (allFigurePoints.Count == 0 || allFigurePoints.ElementAt(0) != pStart || allFigurePoints.Count > 1)
            {
                allFigurePoints.Add(pStart);
            }

            if (!isDoubleClicked)
            {
                return new ClosingLines(figurePoints);
            }
            else
            {
                return new ClosingLines(allFigurePoints);
            }
        }
    }
}
