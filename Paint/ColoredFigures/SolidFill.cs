using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Paint
{
    class SolidFill : ColoredFiguresStrategy
    {
        // переделать метод

        byte[] pixelsCopy = new byte[] { };



        //bool isPressed = false;

        private byte[] GetPixelColorData(Point currentPoint)//возвращает цвет пикселя на битмапе
        {

            WriteableBitmap wb = MyBitmap.GetBitmap().btm;
            int bytePerPixel = 4;
            int stride = 4 * Convert.ToInt32(wb.Width);
            byte[] pixels = new byte[wb.PixelWidth * wb.PixelHeight * 4];
            wb.CopyPixels(pixels, stride, 0);
            int currentPixel = (int)currentPoint.X * bytePerPixel + (stride * (int)currentPoint.Y);
            byte[] color = new byte[] { pixels[currentPixel], pixels[currentPixel + 1], pixels[currentPixel + 2], 0 };

            return color;
        }

        private bool IsColorsEqual(byte[] colorData1, byte[] colorData2)
        {

            if (colorData1[0] == colorData2[0] && colorData1[1] == colorData2[1] && colorData1[2] == colorData2[2] && colorData1[3] == colorData2[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FillFigure(byte[] colorData, Point startPoint)// цветзаливки, кудаткнули
        {
            WriteableBitmap wb = MyBitmap.GetBitmap().btm;
            int bytePerPixel = 4;
            int stride = bytePerPixel * Convert.ToInt32(wb.Width);
            pixelsCopy = new byte[wb.PixelWidth * wb.PixelHeight * bytePerPixel];
            wb.CopyPixels(pixelsCopy, stride, 0);
            byte[] colorStart = GetPixelColorData(startPoint);
            FillFigureStep(new byte[4] { 255, 0, 0, 255 }, colorStart, startPoint);
        }

        public void FillFigureStep(byte[] colorData, byte[] startColorData, Point currentPoint)// цветзаливки, цветстартовогопикселя, кудаткнули
        {
            Pixel pixel = new Pixel();
            WriteableBitmap wb = MyBitmap.GetBitmap().btm;
            Point tmpPoint = currentPoint;

            while (IsColorsEqual(GetPixelColorData(tmpPoint), startColorData) && tmpPoint.X > 0)
            {
                Pixel.Draw(tmpPoint, new byte[4] { 255, 0, 0, 255 });
                tmpPoint.X--;
            }

            if (!IsColorsEqual(GetPixelColorData(tmpPoint), startColorData))
            {
                tmpPoint.X++;
            }

            Point left = tmpPoint;
            tmpPoint.X = currentPoint.X + 1;

            while (IsColorsEqual(GetPixelColorData(tmpPoint), startColorData) && tmpPoint.X < wb.Width - 1)
            {
                Pixel.Draw(tmpPoint, new byte[4] { 255, 0, 0, 255 });
                tmpPoint.X++;
            }
            if (!IsColorsEqual(GetPixelColorData(tmpPoint), startColorData))
            {
                tmpPoint.X--;
            }
            else
            {
                Pixel.Draw(tmpPoint, new byte[4] { 255, 0, 0, 255 });
            }
            Point right = tmpPoint;


            for (int i = (int)left.X; i <= (int)right.X; i++)
            {
                Point point1 = new Point(i, currentPoint.Y + 1);

                if ((IsColorsEqual(GetPixelColorData(point1), startColorData) && point1.Y < wb.Height - 1))
                {
                    FillFigureStep(new byte[4] { 255, 0, 0, 255 }, startColorData, point1);
                }


            }

            for (int i = (int)left.X; i <= (int)right.X; i++)
            {
                Point point2 = new Point(i, currentPoint.Y - 1);

                if ((IsColorsEqual(GetPixelColorData(point2), startColorData) && point2.Y > 0))
                {
                    FillFigureStep(new byte[4] { 255, 0, 0, 255 }, startColorData, point2);
                }
            }
            //isPressed = false;











        }
    }
}
