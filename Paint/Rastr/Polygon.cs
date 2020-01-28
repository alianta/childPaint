using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class Polygon : Figure
    {
        //public Polygon()
        //{ }

        public Polygon(List<Point> figurePoints) : base(figurePoints)
        { }
    }
}
