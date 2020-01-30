using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Fabric
{
    class PolygonCreator : FigureCreator
    {
        private int numSides;
        public PolygonCreator(int numSides)
        {
            this.numSides = Convert.ToInt32(numSides);
        }

        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            return CreatePolygon(pStart, pFinish, numSides);
        }



        /// <summary>
        /// Метод рисует многоугольник
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <param name="n"></param>
        public Figure CreatePolygon(Point pStart, Point pFinish, int numSides)
        {
            List<Point> figurePoints = new List<Point>();
            Point[] p = new Point[numSides + 1];

            lineAngle((double)(360.0 / (double)numSides), pStart, pFinish, numSides, p, figurePoints);

            return new Polygon(figurePoints);
        }

        /// <summary>
        /// Метод создает массив точек многоугольника
        /// </summary>
        /// <param name="angle"></param>
        private void lineAngle(double angle, Point pStart, Point pFinish, int n, Point[] p, List<Point> figurePoints)
        {
            Point v = new Point(pFinish.X - pStart.X, pFinish.Y - pStart.Y);
            double R = Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));

            double z = GetAngle(pFinish, pStart, v, R);

            int i = 0;
            double x, y;
            while (i <= n)
            {
                x = pStart.X + (int)(Math.Round(Math.Cos(z / 180 * Math.PI) * R));
                y = pStart.Y - (int)(Math.Round(Math.Sin(z / 180 * Math.PI) * R));
                figurePoints.Add(new Point((int)x, (int)y));
                z = z + angle;
                i++;
            }
        }

        /// <summary>
        /// Метод возвращает Угол между положительным направлением оси OX и нормированным направляющим вектором отрезка
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <param name="pV"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        float GetAngle(Point pStart, Point pFinish, Point pV, double R)
        {
            /// Нормированный направляющий вектор отрезка
            Point pV1 = new Point((int)(pV.X / R), (int)(pV.Y / R));

            /// Угол между положительным направлением оси OX (вектор (1;0)) 
            /// и нормированным направляющим вектором отрезка
            float alpha = (float)(Math.Acos(pV1.X) * 180 / Math.PI);

            if (pStart.Y < pFinish.Y)
                return alpha;
            else
                return 360 - alpha;
        }
    }
}
