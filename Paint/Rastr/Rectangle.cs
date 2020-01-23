using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Paint.Rastr
{
    class Rectangle : Figure
    {
        private byte[] colorData;
        private Figure line;
        private Figure pixel = new Pixel();
        List<Point> listOfPixels = new List<Point>();
        int thickness;
        public Rectangle(byte[] colorData, int thickness)
        {
            this.thickness = thickness;
            this.colorData = colorData;
            line = new Line(colorData, thickness);
        }

        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            if (shift)
            {
                Draw_Squere(wb, pStart, pFinish, shift);
            }
            else
            {
                Draw_Rectangle(wb, pStart, pFinish, shift);
            }
        }

        /// <summary>
        /// метод рисования квадрата 
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <param name="shift"></param>
        private void Draw_Squere(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            listOfPixels.Add(pStart);

            double xc = (pStart.X + pFinish.X) / 2;
            double yc = (pStart.Y + pFinish.Y) / 2;

            double dx = (pFinish.X - pStart.X) / 2;
            double dy = (pFinish.Y - pStart.Y) / 2;

            Point pointA = new Point();
            pointA.X = xc + dy;
            pointA.Y = yc - dx;
            listOfPixels.Add(pointA);
            listOfPixels.Add(pFinish);

            Point pointB = new Point();
            pointB.X = xc - dy;
            pointB.Y = yc + dx;
            listOfPixels.Add(pointB);
            listOfPixels.Add(pStart);

            for (int i = 0; i < listOfPixels.Count - 1; i++)
            {
                line.Draw(wb, listOfPixels[i], listOfPixels[i + 1], thickness, false);
            }
        }

        /// <summary>
        /// Метод рисует прямоугольник по двум точкам на противоположных углах
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        /// <param name="shift"></param>
        private void Draw_Rectangle(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            listOfPixels.Add(pStart);
            Point pointD = new Point();
            pointD.X = pStart.X;
            pointD.Y = pFinish.Y;

            Point pointB = new Point();
            pointB.X = pFinish.X;
            pointB.Y = pStart.Y;
            listOfPixels.Add(pointB);
            listOfPixels.Add(pFinish);
            listOfPixels.Add(pointD);
            listOfPixels.Add(pStart);

            for (int i = 0; i < listOfPixels.Count - 1; i++)
            {
                line.Draw(wb, listOfPixels[i], listOfPixels[i + 1], thickness, false);
            } 
        }
    }
}
