using Paint.Thickness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    public class Brush
    {
        public ThicknessStrategy BrushThickness { get; set; }
        public Color BrushColor { set; get; }

        public Brush()
        {
            BrushThickness = new DefaultThickness();
            BrushColor = new Color("#000000");
        }

        public Brush (ThicknessStrategy brushThickness, Color brushColor)
        {
            this.BrushThickness = brushThickness;
            this.BrushColor = brushColor;
        }

    }
}
