using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Fabric
{
    class EllipceByFabric : FigureFabric
    {
        public override Figure createFigure()
        {
            return new Ellipce();
        }
    }
}
