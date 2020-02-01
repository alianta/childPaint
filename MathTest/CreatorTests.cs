using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Paint.Rastr;
using Rectangle = Paint.Rastr.Rectangle;

namespace Paint.Fabric

{
    [TestFixture]
    public class CreatorTests
    {

        [TestCase()]
        public void EllipceCreatorTest()
        {
            EllipceCreator figureCreator = new EllipceCreator();
            Figure ellipse = figureCreator.CreateEllipce(new Point(), new Point());

            Assert.IsTrue(ellipse is Ellipce);
        }
        [TestCase()]
        public void CircleCreatorTest()
        {
            EllipceCreator figureCreator = new EllipceCreator();
            Figure circle= figureCreator.CreateCircle(new Point(), new Point());

            Assert.IsTrue(circle is Ellipce);
        }

        [TestCase()]
        public void TriangleRectangularCreatorTest()
        {
            TriangleCreator figureCreator = new TriangleCreator();
            Figure triangle = figureCreator.CreateRectangularTriangle(new Point(), new Point());

            Assert.IsTrue(triangle is Triangle);
        }

        [TestCase()]
        public void TriangleIsoscaleCreatorTest()
        {
            TriangleCreator figureCreator = new TriangleCreator();
            Figure triangle = figureCreator.CreateIsoscaleTriangle(new Point(), new Point());

            Assert.IsTrue(triangle is Triangle);
        }

        [TestCase()]
        public void RectangleIsoscaleCreatorTest()
        {
            RectangleCreator figureCreator = new RectangleCreator();
            Figure rectangle = figureCreator.CreateIsoscaleRectangle(new Point(), new Point());

            Assert.IsTrue(rectangle is Rectangle);
        }

        [TestCase()]
        public void SquereCreatorTest()
        {
            RectangleCreator figureCreator = new RectangleCreator();
            Figure squere = figureCreator.CreateSquere(new Point(), new Point());

            Assert.IsTrue(squere is Rectangle);
        }
        //[TestCase()]
        //public void PolygonCreatorTest()
        //{
        //    PolygonCreator figureCreator = new PolygonCreator();
        //    Figure polygon = figureCreator.CreatePolygon(new Point(), new Point());

        //    Assert.IsTrue(polygon is Polygon);
        //}



    }
}
