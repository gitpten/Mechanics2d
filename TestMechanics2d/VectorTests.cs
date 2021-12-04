using Mechanics2d;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMechanics2d
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void IsCollidingTestWithPolygon()
        {
            Ball b1 = new Ball()
            {
                R = new Vector(4, 4),
                Radius = 6
            };
            Ball b2 = new Ball()
            {
                R = new Vector(19, 3),
                Radius = 2
            };
            Ball b3 = new Ball()
            {
                R = new Vector(17, 11),
                Radius = 2
            };

            Coords p1 = new Coords(7, 7);
            Coords p2 = new Coords(17, 7);
            Coords p3 = new Coords(15, 16);
            Coords p4 = new Coords(9, 14);

            Coords[] coords = { p1, p2, p3, p4 };

            Assert.IsTrue(b1.IsColliding(p1));
            Assert.IsFalse(b2.IsColliding(p2, p3));
            Assert.IsTrue(b3.IsColliding(p2, p3));
            //Assert.IsTrue(b3.IsColliding(coords));
            //Assert.IsTrue(b1.IsColliding(coords));
            //Assert.IsFalse(b2.IsColliding(coords));
        }
    }
}
