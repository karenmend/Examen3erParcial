    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Muestra
    {//Esta clase tiene dos propiedades
        // x= el valor del instante de tiempo que sera tomada la muestra ()
        public double X { get; set; }
        //y= el valor de esa muestra en ese instante(amplitud)
        public double Y { get; set; }

        public Muestra( double x, double y)
        {
            X = x;
            Y = y;
        }
        public Muestra()
        {
            X= 0.0;
            Y = 0.0;
        }
    }
}
