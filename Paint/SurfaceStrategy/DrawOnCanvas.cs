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

        public MyCanvas MyCanvas { get; set; }

        public void Draw(List<Line> listOfLines)
        {
            //Line line = new Line();

            //line.Stroke = System.Windows.Media.Brushes.Black;
            //line.X1 = pStart.X;
            //line.Y1 = pStart.Y;
            //line.X2 = pFinish.X;
            //line.Y2 = pFinish.Y;
            // лист из векторФигуры подтянуть
            // синглтон канваса - экземпляр
            for (int i = 0; i < listOfLines.Count; i++)
            {
                MyCanvas.Instance.Children.Add(listOfLines[i]);
            }
        }

        public void Draw(Point pStart, Point pFinish)
        {
            throw new NotImplementedException();
        }
    }
}
