using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.SurfaceStrategy
{
    public interface ISurfaceStrategy
    {
        Brush CurrentBrush { get; set; }
        void DrawLine(Point pStart, Point pFinish);
        void Draw(Point pStart, Point pFinish);
    }
}
