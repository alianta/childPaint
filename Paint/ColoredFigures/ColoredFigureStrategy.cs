using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
	public interface ColoredFiguresStrategy
	{
		void FillFigure(byte[] colorData, Point startPoint);
	}
}
