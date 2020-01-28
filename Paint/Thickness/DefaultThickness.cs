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
   public class DefaultThickness : ThicknessStrategy
    {
        private readonly List<Point> list = new List<Point>();

        public override List<Point> GetPoints(Point p)
        {
            List<Point> list = new List<Point>
            {
                p
            };
            return list;
        }
    }
}
