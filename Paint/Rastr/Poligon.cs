using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class Poligon : Figure
    {
        private byte[] colorData = { 0, 0, 0, 255 };
        private Figure line;
        int thickness;
        int numberOfSides;
        List<Point> listOfPixels = new List<Point>();


        public Poligon(byte[] colorData, int thickness, int n)
        {
            line = new Line(colorData, thickness);
            numberOfSides = n;
            this.thickness = thickness;
            this.colorData = colorData;
        }
        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            DrawPoligon(wb, pStart, pFinish, numberOfSides);
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
            Point pV1 = new Point(pV.X / R, pV.Y / R);

            /// Угол между положительным направлением оси OX (вектор (1;0)) 
            /// и нормированным направляющим вектором отрезка
            float alpha = (float)(Math.Acos(pV1.X) * 180 / Math.PI);

            if (pStart.Y < pFinish.Y)
                return alpha;
            else
                return 360 - alpha;

        }
        /// <summary>
        /// Метод создает массив точек многоугольника
        /// </summary>
        /// <param name="angle"></param>
        private void lineAngle(double angle, Point pStart, Point pFinish, int n, Point[] p)
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
                listOfPixels.Add(new Point(x, y));
                z = z + angle;
                i++;
            }
        }

        /// <summary>
        /// Метод рисует многоугольник
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <param name="n"></param>
        public void DrawPoligon(WriteableBitmap wb, Point pStart, Point pFinish, int n)
        {
            Point[] p = new Point[n + 1];

            lineAngle((double)(360.0 / (double)n), pStart, pFinish, n, p);

            int i = n;
            while (i > 0)
            {
                line.Draw(wb, listOfPixels[i], listOfPixels[i - 1], thickness, false);
                i = i - 1;
            }
        }
    }
}
