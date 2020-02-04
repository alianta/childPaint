using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint.Rastr
{
  public abstract class Figure
    {
        public List<Point> Points { get; set; }
        public IDrawer DrawerRealisation { get; set; }

        public Figure(List<Point> points)
        {
            Points = points;
        }

        public virtual void DoDraw() {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                DrawerRealisation.DrawOnSurface(Points[i], Points[i + 1]);
            }
            DrawerRealisation.DrawOnSurface(Points[0], Points[Points.Count - 1]);
        }
    }
}
