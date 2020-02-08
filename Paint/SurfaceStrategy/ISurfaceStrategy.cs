using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Paint.SurfaceStrategy
{
    public interface ISurfaceStrategy
    {
        Brush CurrentBrush { get; set; }


        void Draw(Point pStart, Point pFinish);

        //void DrawVector(List<Line> listOfLines);
    }
}
