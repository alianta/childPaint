using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint.Thickness
{
    class ExtraboldThickness : ThicknessStrategy
    {
        readonly List<Point> list = new List<Point>();

        public override List<Point> GetPoints(Point point)
        {
            list.Add(point);
            list.Add(new Point(point.X - 1, point.Y));
            list.Add(new Point(point.X + 1, point.Y));
            list.Add(new Point(point.X + 2, point.Y));
            list.Add(new Point(point.X - 1, point.Y - 1));
            list.Add(new Point(point.X, point.Y - 1));
            list.Add(new Point(point.X + 1, point.Y - 1));
            list.Add(new Point(point.X + 2, point.Y - 1));
            list.Add(new Point(point.X, point.Y + 1));
            list.Add(new Point(point.X + 1, point.Y + 1));
            list.Add(new Point(point.X, point.Y + 2));
            list.Add(new Point(point.X + 1, point.Y + 2));
            return list;
        }
    }
}
