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
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            edges = new Coords[]
            {
                new Coords(0,0),
                new Coords(panel1.Width / scale, 0),
                new Coords(panel1.Width / scale, panel1.Height / scale),
                new Coords(0, panel1.Height / scale)
            };
        }

        public void UpdateBalls()
        {
            g.Clear(panel1.BackColor);
            foreach (var b in balls)
            {
                b.MoveWithCollision(balls, timer1.Interval / 1000.0,  new Vector(0, 0));

                b.Collide(edges);

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
