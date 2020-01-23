using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class StraightLine : Figure
    {
        
        private byte[] colorData;
        private Figure line;
        private Figure pixel = new Pixel();
        List<Point> listOfPixels = new List<Point>();
        int thickness;

        public StraightLine(byte[] colorData, int thickness)
        {
            this.colorData = colorData;
            this.thickness = thickness;
            line = new Line(colorData, thickness);
        }

        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            Draw_StraightLine(wb, pStart, pFinish);
        }
        private void Draw_StraightLine(WriteableBitmap wb, Point pStart, Point pFinish)
        {
            listOfPixels.Add(pStart);
            listOfPixels.Add(pFinish);

            line.Draw(wb, pStart, pFinish, thickness, false);
            


        }
    }
}
