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
        public static void Draw(Point p, byte[] colorData)
        {
           MyBitmap wb = MyBitmap.GetBitmap();

            if (p.X > 0 && p.X < (int)wb.btm.Width && p.Y > 0 && p.Y < (int)wb.btm.Height)
            {
                Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
                wb.btm.WritePixels(rect, colorData, 4, 0);
            }
        }

        //public static void JewishDraw(Point p, byte[] colorData)
        //{
        //    MyBitmap wb = MyBitmap.GetBitmap();
        //    try
        //    {
        //        // Reserve the back buffer for updates.
        //        wb.btm.Lock();

        //        unsafe
        //        {
        //            // Get a pointer to the back buffer.
        //            IntPtr pBackBuffer = wb.btm.BackBuffer;

        //            // Find the address of the pixel to draw.
        //            pBackBuffer += (int)p.Y * wb.btm.BackBufferStride;
        //            pBackBuffer += (int)p.X * 4;

        //            // Compute the pixel's color.
        //            int color_data = 255 << 16; // R
        //            color_data |= 128 << 8;   // G
        //            color_data |= 255 << 0;   // B

        //            // Assign the color data to the pixel.
        //            *((int*)pBackBuffer) = color_data;
        //        }

        //        // Specify the area of the bitmap that changed.
        //        wb.btm.AddDirtyRect(new Int32Rect((int)p.X, (int)p.Y, 1, 1));
        //    }
        //    finally
        //    {
        //        // Release the back buffer and make it available for display.
        //        wb.btm.Unlock();
        //    }
        //}

    }
}
