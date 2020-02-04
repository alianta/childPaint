using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using Paint.SurfaceStrategy;

namespace Paint
{
    class DrawByDots : IDrawer
    {
        public ISurfaceStrategy SurfaceStrategy { get; set; }

        public void DrawOnSurface(Point pStart, Point pFinish)
        {
            //for (int i = 0; i < figurePoints.Count; i += 10)
            //{
            //    Pixel.Draw(figurePoints[i + 5], CurrentBrush.BrushColor.HexToRGBConverter());
            //}
        }
    }
}
