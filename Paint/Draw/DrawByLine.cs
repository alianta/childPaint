using Paint.Rastr;
using Paint.SurfaceStrategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Paint
{
    public class DrawByLine : IDrawer
    {
        public ISurfaceStrategy SurfaceStrategy { get; set; }

        public void DrawOnSurface(Point pStart, Point pFinish)
        {
            if (SurfaceStrategy == null)
                return; // идите в жопу

            SurfaceStrategy.Draw(pStart, pFinish);

        }

        //public void DrawOnSurface(List<Line> listOfLines) {
        //    if (SurfaceStrategy == null)
        //        return; // идите в жопу
        //    SurfaceStrategy.DrawVector(listOfLines);
        //}
    }
}
