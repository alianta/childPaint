﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Thickness
{
    class MediumThickness : ThicknessStrategy
    {
        public override List<Point> GetPoints(Point p)
        {
            List<Point> list = new List<Point>
            {
                p,
                new Point(p.X - 1, p.Y),
                new Point(p.X, p.Y - 1),
                new Point(p.X - 1, p.Y - 1)
            };

            return list;
        }
    }
}
