using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Thickness
{
    class MediumThickness : Thickness
    {
        readonly List<Point> list = new List<Point>();

        private List<Point> Method(Point p)
        {
            list.Add(p);
            return list;
        }
    }
}
