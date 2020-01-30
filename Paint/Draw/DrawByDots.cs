using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Paint
{
    class DrawByDots : IDrawer
    {
        public Brush CurrentBrush { set; get; }

        public void Draw(Point pStart, Point pFinish)
        {
            //for (int i = 0; i < figurePoints.Count; i += 10)
            //{
            //    Pixel.Draw(figurePoints[i + 5], CurrentBrush.BrushColor.HexToRGBConverter());
            //}
        }
    }
}
