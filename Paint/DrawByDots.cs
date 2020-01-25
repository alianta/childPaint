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
    class DrawByDots : IDrawer
    {
        public Brush CurrentBrush { set; get; }

        public void Draw(List<Point> figurePoints, WriteableBitmap wb, Brush curBrush)
        {
            Line line = new Line();
            for (int i = 0; i < figurePoints.Count; i += 10)
            {
                line.Draw(wb, figurePoints[i], figurePoints[i+5], curBrush.BrushColor);
            }
        }
    }
}
