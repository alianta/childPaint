using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    abstract class Figure
    {
        public virtual void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {

        }

        public virtual void Draw(WriteableBitmap wb, Point pStart, Point pFinish, string sides)
        {

        }

        public virtual void Draw(WriteableBitmap wb, Point pStart, byte[] colorData)
        {

        }

        
        public virtual void Draw(WriteableBitmap wb, Point pStart, Point pFinish, int thickness, bool shift) //фигуры должны вызывать этот метод! т.к. в нем есть толщина!
        {

        }
    }
}
