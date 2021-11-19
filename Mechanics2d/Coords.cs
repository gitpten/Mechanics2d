using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanics2d
{
    public struct Coords
    {
        public Coords(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

    }
}
