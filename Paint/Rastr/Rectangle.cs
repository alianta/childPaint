﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;


namespace Paint.Rastr
{
    public class Rectangle : Figure
    {
        public Rectangle(List<Point> figurePoints) : base(figurePoints)
        { }
    }
}
