using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Paint
{
    class Stack
    {
        List<WriteableBitmap> myBitmaps = new List<WriteableBitmap>();

        public void AddMyBitmap(WriteableBitmap a)
        {
            myBitmaps.Add(a);
        }

        public WriteableBitmap GetMyBitmap()
        {
            WriteableBitmap myBitmap = myBitmaps[myBitmaps.Count - 1];
            myBitmaps.RemoveAt(myBitmaps.Count - 1);

            return myBitmap;
        }

        public int GetSize()
        {
            return myBitmaps.Count;
        }
    }
}
