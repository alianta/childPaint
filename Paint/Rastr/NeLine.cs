using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class NeLine : Figure
    {
        //public Line()
        //{ }
        
        public NeLine(List<Point> points) : base(points)
        {
            //Points = points;
        }

        public override void DoDraw()
        {
            Color tmp = DrawerRealisation.CurrentBrush.BrushColor;
            DrawerRealisation.CurrentBrush.BrushColor=new Color("#FFFFFFFF");


            for (int i = 0; i < Points.Count - 1; i++)
            {
                DrawerRealisation.Draw(Points[i], Points[i + 1]);
            }
            DrawerRealisation.Draw(Points[0], Points[Points.Count - 1]);


            DrawerRealisation.CurrentBrush.BrushColor = tmp;
        }


    }
}
