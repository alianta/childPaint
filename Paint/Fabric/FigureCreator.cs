﻿using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Paint.Fabric
{
    abstract class FigureCreator
    {
        public abstract Figure CreateFigure(Point pStart, Point pFinish);// создает объект фигуры

    }
}
