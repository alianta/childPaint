using Paint.Thickness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    class Brush
    {
        public ThicknessStrategy brushThickness { get; set; }
        public Color brushColor { set; get; }

        public Brush()
        {
            brushThickness = new DefaultThickness();
            brushColor = new Color("#000000");
        }

        public Brush (ThicknessStrategy brushThickness, Color brushColor)
        {
            this.brushThickness = brushThickness;
            this.brushColor = brushColor;
        }

    }
}
