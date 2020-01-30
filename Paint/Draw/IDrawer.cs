﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Paint
{
    public interface IDrawer
    {
        Brush CurrentBrush { get; set; }
        void Draw(Point pStart, Point pFinish);
    }
}
