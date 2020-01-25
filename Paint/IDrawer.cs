using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint
{
    public interface IDrawer
    {
        Brush CurrentBrush { get; set; }
        void Draw(List<Point> figurePoints, WriteableBitmap wb, Brush curBrush);
    }
}
