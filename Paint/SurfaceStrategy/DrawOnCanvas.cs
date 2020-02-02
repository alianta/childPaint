using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.SurfaceStrategy
{
    public class DrawOnCanvas : ISurfaceStrategy
    {
        public Brush CurrentBrush { get; set; }

        public void DrawLine(Point pStart, Point pFinish)
        {
            Line line = new Line();

            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = pStart.X;
            line.Y1 = pStart.Y;
            line.X2 = pFinish.X;
            line.Y2 = pFinish.Y;
        }
    }
}
