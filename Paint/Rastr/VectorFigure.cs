using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Rastr
{
    public class VectorFigure : Figure
    {
        public List<Line> ListOfLines { get; set; }

        public VectorFigure(List<Point> points) : base(points)
        {
            ListOfLines = new List<Line>();
            for (int i = 0; i < points.Count-1; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = points[i].X;
                line.Y1 = points[i].Y;
                line.X2 = points[i + 1].X;
                line.Y2 = points[i + 1].Y;
                ListOfLines.Add(line);
            }
            Line lastLine = new Line();
            lastLine.X1 = points[points.Count-1].X;
            lastLine.Y1 = points[points.Count-1].Y;
            lastLine.X2 = points[0].X;
            lastLine.Y2 = points[0].Y;
            ListOfLines.Add(lastLine);
        }
    }
}
