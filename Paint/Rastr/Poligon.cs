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
        /// Метод создает массив точек многоугольника
        /// </summary>
        /// <param name="angle"></param>
        private void lineAngle(double angle, Point pStart, int n, Point[] p, double R)
        {
            double z = 0; int i = 0;
            while (i < n + 1)
            {
                p[i].X = pStart.X + (int)(Math.Round(Math.Cos(z / 180 * Math.PI) * R));
                p[i].Y = pStart.Y - (int)(Math.Round(Math.Sin(z / 180 * Math.PI) * R));
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
            double R = Math.Sqrt(Math.Pow((pFinish.X - pStart.X), 2) + Math.Pow((pFinish.Y - pStart.Y), 2));

            Point[] p = new Point[n + 1];
            lineAngle((double)(360.0 / (double)n), pStart, n, p, R);
            int i = n;

            while (i > 0)
            {
                line.Draw(wb, p[i], p[i - 1], thickness, false);
                i = i - 1;
            }
        }

    }
}
