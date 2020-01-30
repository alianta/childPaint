using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Thickness
{
    public abstract class ThicknessStrategy
    {

        public abstract List<Point> GetPoints(Point p);
    }
}
