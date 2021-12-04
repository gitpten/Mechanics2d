using Mechanics2d;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mechanics2dDemo
{
    public partial class Form1 : Form
    {
        double scale = 100;
        List<Ball> balls = new List<Ball>();
        Graphics g;
        Coords[] edges;
        Coords[] e2;
        Point[] points, po2;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            edges = new Coords[]
            {
                new Coords(0,0),
                new Coords(panel1.Width / scale, 0),
                new Coords(panel1.Width / scale, panel1.Height / scale),
                new Coords(panel1.Width / 3 /scale, 5 * panel1.Height / 6 / scale),
                new Coords(0, panel1.Height / scale)
            };

            e2 = new Coords[]
            {
                new Coords(40  / scale,40  / scale),
                new Coords(60  / scale, 40 / scale),
                new Coords(60 / scale,80 / scale)
            };

            List<Point> pp = new List<Point>();
            foreach (var c in edges)
            {
                pp.Add(new Point((int)(c.X * scale), (int)(c.Y * scale)));
            }
            points = pp.ToArray();

            pp = new List<Point>();
            foreach (var c in e2)
            {
                pp.Add(new Point((int)(c.X * scale), (int)(c.Y * scale)));
            }
            po2 = pp.ToArray();
        }

        public void UpdateBalls()
        {
            g.Clear(panel1.BackColor);
            g.DrawPolygon(Pens.Red, points);
            g.DrawPolygon(Pens.Red, po2);
            foreach (var b in balls)
            {
                b.MoveWithCollision(balls, timer1.Interval / 1000.0,  new Vector(0, 0));

                b.Collide(edges);
                b.Collide(e2);
                g.DrawEllipse(Pens.Red, (int)((b.R.X - b.Radius) * scale),
                    (int)((b.R.Y - b.Radius) * scale), (int)(b.Radius * 2 * scale)
                    , (int)(b.Radius * 2 * scale));
            }
            Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            balls.Add(new Ball()
            {
                M = 5,
                V = new Vector(2, -8),
                Radius = 0.5,
                R = new Vector(1, 3)
            });

            balls.Add(new Ball()
            {
                M = 3,
                V = new Vector(-2, -5),
                Radius = 0.3,
                R = new Vector(5, 5)
            });

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateBalls();
        }
    }
}
