using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    public class NeLine : Figure
    {
        public NeLine(List<Point> points) : base(points)
        { }

        public override void DoDraw()
        {
            Color tmp = DrawerRealisation.SurfaceStrategy.CurrentBrush.BrushColor;
            DrawerRealisation.SurfaceStrategy.CurrentBrush.BrushColor=new Color("#FFFFFFFF");


            for (int i = 0; i < Points.Count - 1; i++)
            {
                DrawerRealisation.DrawOnSurface(Points[i], Points[i + 1]);
            }
            DrawerRealisation.DrawOnSurface(Points[0], Points[Points.Count - 1]);


            DrawerRealisation.SurfaceStrategy.CurrentBrush.BrushColor = tmp;
        }


    }
}
