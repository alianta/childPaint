using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using Point = System.Drawing.Point;

namespace Paint.Rastr
{
    class Pixel
    {
        public static void Draw(Point p, byte[] colorData)
        {
           MyBitmap wb = MyBitmap.GetBitmap();

            if (p.X > 0 && p.X < (int)wb.btm.Width && p.Y > 0 && p.Y < (int)wb.btm.Height)
            {
                Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
                wb.btm.WritePixels(rect, colorData, 4, 0);
            }
        }
    }
}
