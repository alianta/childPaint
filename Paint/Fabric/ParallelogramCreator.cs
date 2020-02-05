using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Fabric
{
    public class ParallelogramCreator : FigureCreator
    {
        /// <summary>
        /// Ромб
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <returns></returns>
        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            return CreateParallelogram(pStart, pFinish);
        }

        public Figure CreateParallelogram(Point pStart, Point pFinish)
        {
            List<Point> figurePoints = new List<Point>();
            figurePoints.Add(pStart);
            if (pStart != pFinish)
            {
                int h = Math.Abs(pFinish.Y - pStart.Y);
                double side = h / (Math.Sqrt(3) / 2);
                double delta = Math.Sqrt(Math.Pow(side, 2) - Math.Pow(h, 2));
                figurePoints.Add(new Point((int)(pStart.X + h + delta), pStart.Y));
                figurePoints.Add(new Point((int)(pStart.X + h), pFinish.Y));
                figurePoints.Add(new Point((int)(pStart.X - delta), pFinish.Y));
            }
            return new Parallelogram(figurePoints);
        }
    }

    //double side = Math.Sqrt(Math.Pow(pStart.X - pFinish.X, 2) + Math.Pow(pStart.Y - pFinish.Y, 2));
    //double delta = Math.Sqrt(Math.Pow(side, 2) - Math.Pow(pStart.Y - pFinish.Y, 2));
    //figurePoints.Add(pStart);
    //figurePoints.Add(new Point((int)(pStart.X + side), pStart.Y));
    //figurePoints.Add(new Point((int)(pStart.X + delta), pFinish.Y));
    //figurePoints.Add(pFinish);

}
