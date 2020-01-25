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
    class DefaultThickness : ThicknessStrategy
    {
        private readonly List<Point> list = new List<Point>();

        public override List<Point> GetPoints(Point p)
        {
            list.Add(p);
            return list;
        }
    }
}
