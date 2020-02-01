using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.SurfaceStrategy
{
    public class DrawOnCanvas : ISurfaceStrategy
    {
        public Brush CurrentBrush { get; set; }

        public void DrawLine(Point pStart, Point pFinish)
        {
            throw new NotImplementedException();
        }
    }
}
