using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Mechanics2d
{
    public static class Extantions
    {
        public static void DrawVector(this Graphics g, Pen pen, Vector vector, Point start, double scale = 1)
        {
            Vector vStart = new Vector(start);
            Vector vFinish = vStart + vector * scale;
            Point finish = vFinish.GetPoint();
            g.DrawLine(pen, start, finish);
            double w = pen.Width * 1.8;
            double l = w * 3;
            Vector e = vector.E;
            Vector left = vStart + (vector - e * l + e.Normal * w) * scale;
            Vector right = vStart + (vector - e * l + e.Normal * -w) * scale;
            g.DrawLine(pen, finish, left.GetPoint());
            g.DrawLine(pen, finish, right.GetPoint());
        }
    }
}
