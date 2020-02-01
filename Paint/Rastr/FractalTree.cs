using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class FractalTree : Figure
    {        
        public FractalTree(List<Point> figurePoints) : base(figurePoints)
        { }

        public override void DoDraw()
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                DrawerRealisation.DrawOnSurface(Points[i], Points[i + 1]);
            }           
        }
    }
}
