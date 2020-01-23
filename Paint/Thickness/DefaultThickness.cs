using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Collections;

namespace Paint.Thickness
{
    class DefaultThickness : Thickness
    {
        readonly List<Point> list = new List<Point>();

        private List<Point> Method(Point p)
        {
            list.Add(p);
            return list;
        }
    }
}
