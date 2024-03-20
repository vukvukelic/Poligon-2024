using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poligon_2024
{
    public class tacka
    {
        public double x, y;
        public tacka(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public tacka() { }

        public double get_r()
        {
            double r = Math.Sqrt(x * x + y * y);
            return r;
        }

    }
}
