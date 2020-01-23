using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Thickness
{
    abstract class Thickness
    {
        int thick;
        Thickness strategy;
        public void Draw()
        {
            switch (thick)
            {
                case 1:
                    strategy = new DefaultThickness();
                    break;
                case 2:
                    strategy = new MediumThickness();
                    break;
                case 3:
                    strategy = new HardThickness();
                    break;
                case 4:
                    strategy = new BoldThickness();
                    break;
            }

        }
        
    }
}
