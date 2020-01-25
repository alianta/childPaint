using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class Pixel
    {
        static byte[] colorData = { 0, 0, 0, 255 };
        public void Draw(WriteableBitmap wb, Point p, byte[] colorData)
        {
            if (p.X > 0 && p.X < (int)wb.Width && p.Y > 0 && p.Y < (int)wb.Height)
            {
                Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
                wb.WritePixels(rect, colorData, 4, 0);
                //   curState.WritePixels(rect, colorData, 4, 0);
                //MainImage.Source = wb;
            }
        }

    }
}
