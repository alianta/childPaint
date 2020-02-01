using Paint.Rastr;
using Paint.SurfaceStrategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;

namespace Paint
{
    public class DrawByLine : IDrawer
    {
        public ISurfaceStrategy SurfaceStrategy { get; set; }

        public void DrawOnSurface(Point pStart, Point pFinish)
        {
            if (SurfaceStrategy == null)
                return; // идите в жопу

            SurfaceStrategy.DrawLine(pStart, pFinish);

            //List<Point> first = CurrentBrush.BrushThickness.GetPoints(pStart);
            //List<Point> second = CurrentBrush.BrushThickness.GetPoints(pFinish);
            //for (int i = 0; i < first.Count; i++)
            //{
            //    DrawLine(first[i], second[i]);
            //}
        }

        ///// <summary>
        ///// Рисование линии попиксельно
        ///// </summary>
        ///// <param name="wb">Холст</param>
        ///// <param name="pStart">Начальная точка</param>
        ///// <param name="pFinish">конечная точка</param>
        //public void DrawLine(Point pStart, Point pFinish)
        //{
        //    int d = 1;
        //    if (pFinish.X >= pStart.X && pFinish.Y >= pStart.Y)//прямая начинается с права с верху в низ
        //    {
        //        DrawLineInThirdTourthQuarters(pStart, pFinish, d);
        //    }
        //    else if (pFinish.X <= pStart.X && pFinish.Y <= pStart.Y)//прямая начинается с права с низу в верх
        //    {
        //        DrawLineInThirdTourthQuarters(pFinish, pStart, d);
        //    }
        //    else if (pFinish.X > pStart.X && pFinish.Y < pStart.Y)//прямая начинается с лева с низу в верх
        //    {
        //        d *= -1;
        //        DrawLineInThirdTourthQuarters(pFinish, pStart, d);
        //    }
        //    else if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)//прямая начинается с лева с верху в низ
        //    {
        //        d *= -1;
        //        DrawLineInThirdTourthQuarters(pStart, pFinish, d);
        //    }
        //}

        ///// <summary>
        ///// Метод риcует линию по двум координатам 
        ///// </summary>
        ///// <param name="pStart">Point точка старта</param>
        ///// <param name="pFinish">Point точка финиша</param>
        //private void DrawLineInThirdTourthQuarters(Point pStart, Point pFinish, int d)
        //{
        //    Point newP = new Point();

        //    double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
        //    double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;

        //    if (deltaX > deltaY)
        //    {
        //        double k = deltaY / deltaX;
        //        for (int i = 0; i < deltaX; i++)
        //        {
        //            newP.Y = ((int)(k * i + pStart.Y));
        //            newP.X = pStart.X + i * d;
        //            Pixel.Draw(newP, CurrentBrush.BrushColor.HexToRGBConverter());
        //        }
        //    }
        //    else
        //    {
        //        if (pFinish.X < pStart.X)
        //        {
        //            pStart = pFinish;
        //        }
        //        double k = deltaX / deltaY;
        //        for (int i = 0; i < deltaY; i++)
        //        {
        //            newP.X = ((int)(k * i + pStart.X));
        //            newP.Y = pStart.Y + i * d;
        //            Pixel.Draw(newP, CurrentBrush.BrushColor.HexToRGBConverter());
        //        }
        //    }
        //}
    }
}
