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
    public interface IDrawer
    {        
        ISurfaceStrategy SurfaceStrategy { get; set; }
        void DrawOnSurface(Point pStart, Point pFinish);
    }
}
