using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class StraightLine : Figure
    {
        public StraightLine(List<Point> figurePoints) : base(figurePoints)
        { }
    }
}
