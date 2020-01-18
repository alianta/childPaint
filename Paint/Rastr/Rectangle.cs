﻿using System;
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
        static byte[] colorData = { 0, 0, 0, 255 };
        private Figure line = new Line();

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

            line.Draw(wb,pStart, pFinish, shift);

            double katet1, katet2;
            katet1 = pStart.Y - pFinish.Y;
            katet2 = pFinish.X - pStart.X;

            Point pointD = new Point();
            pointD.X = pStart.X + katet1;
            pointD.Y = pStart.Y + katet2;

            Point pointC = new Point();
            pointC.X = pFinish.X + katet1;
            pointC.Y = pFinish.Y + katet2;

            line.Draw(wb,pFinish, pointC,shift);
            line.Draw(wb,pointC, pointD,shift);
            line.Draw(wb,pointD, pStart,shift);
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
            Point pointD = new Point();
            pointD.X = pStart.X;
            pointD.Y = pFinish.Y;

            Point pointB = new Point();
            pointB.X = pFinish.X;
            pointB.Y = pStart.Y;
            line.Draw(wb,pStart, pointB,shift);
            line.Draw(wb,pointB, pFinish, shift);
            line.Draw(wb,pFinish, pointD,shift);
            line.Draw(wb,pointD, pStart,shift);
        }
    }
}