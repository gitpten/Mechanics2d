using System;
using System.Collections.Generic;

namespace Mechanics2d
{
    public class Ball
    {
        public double M { get; set; }
        public double Radius { get; set; }
        public Vector V { get; set; }
        public Vector A { get; set; }
        public Vector R { get; set; }

        public void Move(double dt)
        {
            V += A * dt;
            R += V * dt;
        }

        public void Move(double dt, Vector F)
        {
            A = F / M;
            Move(dt);
        }
        public void MoveWithCollision(List<Ball> balls, double dt)
        {
            MoveWithCollision(balls, dt, new Vector());
        }

        public void MoveWithCollision(List<Ball> balls, double dt, Vector F)
        {
            Move(dt, F);
            foreach (var ball in balls)
            {
                if (ball == this) continue;
                if(IsColliding(ball))
                {
                    Collide(ball);
                }
            }
        }

        public bool IsColliding(Coords p1, Coords p2)
        {
            Vector p1_v = new Vector(p1);
            Vector p1_b_v = R - p1_v;
            Vector p2_v = new Vector(p2);
            Vector p2_b_v = R - p2_v;
            Vector p1p2_v = new Vector(p1, p2);
            
            return p1_b_v.Projection(p1p2_v) * p2_b_v.Projection(p1p2_v) < 0 
                && p1_b_v.Projection(p1p2_v.Normal).SqAbs <= Radius * Radius ;
        }

        public bool IsColliding(Coords p)
        {            
            return (new Vector(p) - R).SqAbs <= Radius * Radius;
        }

        public void Collide(Coords[] points)
        {
            foreach (var p in points)
            {
                if (IsColliding(p))
                {
                    Collide(p);
                    return;
                }
            }
            for (int i = 0; i < points.Length - 1; i++)
            {
                if(IsColliding (points[i], points[i + 1]))
                    Collide(points[i], points[i + 1]);
            }
            if(IsColliding(points[points.Length - 1], points[0])) Collide(points[points.Length - 1], points[0]);
        }

        public void Collide(Coords p1, Coords p2)
        {
                ShiftBack(p1, p2);
                V = V.Mirror(new Vector(p1, p2));
        }

        public void Collide(Coords p)
        {
            ShiftBack(p);
            V = V.Mirror(R - new Vector(p).Normal);
        }

        private void ShiftBack(Coords p)
        {
            var R_p = new Vector(p) - R;
            var R_v = R_p.E * Radius;
            R -= (R_v - R_p)*1.1;
        }

        public bool IsColliding(Ball b)
        {
            double minDist = b.Radius + Radius;
            return (R - b.R).SqAbs <= minDist * minDist;
        }

        public void Collide(Ball b)
        {
            ShiftBack(b);

            Vector OO = R - b.R;

            Vector v0 = V.Projection(OO);
            Vector u0 = b.V.Projection(OO);

            Vector v0y = V - v0;
            Vector u0y = b.V - u0;

            Vector v = (2 * b.M * u0 + v0 * (M - b.M)) / (M + b.M);
            Vector u = v0 + v - u0;

            V = v + v0y;
            b.V = u + u0y;
        }

        private void ShiftBack(Ball b) //обязательно переделать
        {
            Vector OO = b.R - R;
            double dd = Radius + b.Radius;
            Vector n = OO.Projection(V.Normal);
            R = b.R - n + Math.Sqrt(dd * dd - n.SqAbs) * (V * -1).E;
        }

        private void ShiftBack(Coords p1, Coords p2) //обязательно переделать
        {
            
            Vector p1_v = new Vector(p1);
            Vector p1_b_v = R - p1_v;
            Vector p1p2_v = new Vector(p1, p2);
            Vector onWall = -p1_b_v.Projection(p1p2_v.Normal);

            R += onWall - onWall.E * Radius*1.1;
        }

    }
}
