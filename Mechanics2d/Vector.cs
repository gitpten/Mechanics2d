using Mechanics2d;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics2d
{
    public class Vector
    {
        public Vector() : this(0, 0) { }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point p) : this(p.X, p.Y) { }

        public Vector(Coords p) : this(p.X, p.Y) { }

        public Vector(Point p1, Point p2) : this(p2.X - p1.X, p2.Y - p1.Y) { }
        public Vector(Coords p1, Coords p2) : this(p2.X - p1.X, p2.Y - p1.Y) { }

        public double X { get; set; }
        public double Y { get; set; }
        
        public double SqAbs
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        public double Abs => Math.Sqrt(SqAbs);

        public Vector Normal
        {
            get
            {
                return new Vector(-Y, X);
            }
        }

        public Vector E => this / Abs;

        public static Vector operator+(Vector v1, Vector v2)
        {             
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator-(Vector v)
        {
            return new Vector(-v.X, -v.Y);
        }

        public static Vector operator-(Vector v1, Vector v2)
        {
            return v1 + -v2;
        }

        public static Vector operator*(Vector v, double n)
        {
            return new Vector(n * v.X, n * v.Y);
        }

        public static Vector operator*(double n, Vector v)
        {
            return v * n;
        }

        public static double operator*(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector operator/(Vector v, double n)
        {
            return v * (1 / n);
        }

        public Point GetPoint() => new Point((int)X, (int)Y);
        public Coords GetCoords() => new Coords(X, Y);


        public Vector Projection(Vector v)
        {
            return (this * v) / (v * v) * v;
        }

        public Vector Mirror(Vector v)
        {
            return this - 2 * this.Projection(v.Normal);
        }

        public Vector Mirror(Point p1, Point p2)
        {
            return Mirror(new Vector(p1, p2));
        }

        public Vector Mirror(Coords p1, Coords p2)
        {
            return Mirror(new Vector(p1, p2));
        }

        public void HorizontalBounce()
        {
            Y = -Y;
        }

        public void VerticalBounce()
        {
            X = -X;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector && ((Vector)obj).X == X && ((Vector)obj).Y == Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
