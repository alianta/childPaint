using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Thickness
{
    class ExtraboldThickness : ThicknessStrategy
    {
        public override List<Point> GetPoints(Point point)
        {
            List<Point> list = new List<Point>
            {
                point,
                new Point(point.X - 1, point.Y),
                new Point(point.X + 1, point.Y),
                new Point(point.X + 2, point.Y),
                new Point(point.X - 1, point.Y - 1),
                new Point(point.X, point.Y - 1),
                new Point(point.X + 1, point.Y - 1),
                new Point(point.X + 2, point.Y - 1),
                new Point(point.X, point.Y + 1),
                new Point(point.X + 1, point.Y + 1),
                new Point(point.X, point.Y + 2),
                new Point(point.X + 1, point.Y + 2)
            };
            return list;
        }
    }
}
