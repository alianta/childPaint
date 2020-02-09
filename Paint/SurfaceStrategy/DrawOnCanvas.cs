using Paint.Thickness;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.SurfaceStrategy
{
    public class DrawOnCanvas : ISurfaceStrategy
    {
        public Brush CurrentBrush { get; set; }

        public void Draw(Point pStart, Point pFinish)
        {

            string bcolor = CurrentBrush.BrushColor.getHexColor();
            Line line = new Line
            {
                X1 = pStart.X,
                Y1 = pStart.Y,
                X2 = pFinish.X,
                Y2 = pFinish.Y,
               // Stroke = new SolidColorBrush(Colors.Red),
                Stroke = new BrushConverter().ConvertFromString(bcolor) as SolidColorBrush,
                StrokeThickness = getVectorThickness(),
            };
            line.Tag = MyCanvas.CurrentFigure;
            MyCanvas.GetInstanceCopy().Children.Add(line);
        }

        private double getVectorThickness()
        {
            double thickness = 1;

            switch (CurrentBrush.BrushThickness.GetType().Name)
            {
                case nameof(DefaultThickness):
                    thickness = 1;
                    break;
                case nameof(MediumThickness):
                    thickness = 2;
                    break;
                case nameof(BoldThickness):
                    thickness = 3;
                    break;
                case nameof(ExtraboldThickness):
                    thickness = 4;
                    break;
            }

            return thickness;
        }
    }
}
