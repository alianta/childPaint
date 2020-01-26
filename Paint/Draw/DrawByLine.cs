using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint
{
    class DrawByLine : IDrawer
    {
        public Brush CurrentBrush { set; get; }
        public void Draw(List<Point> figurePoints, WriteableBitmap wb) {
            Pixel pixel = new Pixel();
            for (int i = 0; i < figurePoints.Count; i++)
            {
                pixel.Draw(wb, figurePoints[i], CurrentBrush.BrushColor.HexToRGBConverter());
            }
        }
    }
}
