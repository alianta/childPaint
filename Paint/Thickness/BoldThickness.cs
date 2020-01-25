using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Thickness
{
    class BoldThickness : ThicknessStrategy
    {
        private readonly List<Point> list = new List<Point>();

        public override List<Point> GetPoints(Point p)
        {
            list.Add(p);
            list.Add(new Point(p.X - 1, p.Y));
            list.Add(new Point(p.X, p.Y - 1));
            list.Add(new Point(p.X - 1, p.Y - 1));
            list.Add(new Point(p.X, p.Y + 1));
            return list;
        }
    }
}
