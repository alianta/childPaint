using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    abstract class Figure
    {
        public List<Point> Points { get; set; }
        public IDrawer DrawerRealisation { get; set; }

        public Figure(List<Point> points)
        {
            Points = points;
        }

        public void DoDraw() {
            DrawerRealisation.Draw();
        }
    }
}
