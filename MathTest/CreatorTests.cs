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
        //PolygonCreator figureCreator = new PolygonCreator();
        //    int numSides = 6;
        //    Figure polygon = figureCreator.CreatePolygon(new Point(), new Point(), numSides);

        //    Assert.IsTrue(polygon is Polygon);
        //}

        [TestCase()]
        public void EraserCreatorTest()
        {
            EraserCreator figureCreator = new EraserCreator();
            bool shiftPressed = false;
            Figure neLine = figureCreator.CreateFigure(new Point(), new Point(), shiftPressed);

            Assert.IsTrue(neLine is NeLine);
        }


        [TestCase()]
        public void ClosingLinesCreatorTest()
        {
            ClosingLinesCreator figureCreator = new ClosingLinesCreator();

            bool isDoubleClicked = false;
            Figure closingLines = figureCreator.CreateFigure(new Point(), new Point(), isDoubleClicked);

            Assert.IsTrue(closingLines is ClosingLines);
        }


        [TestCase()]
        public void StraightLineCreatorTest()
        {
            StraightLineCreator figureCreator = new StraightLineCreator();
            bool shiftPressed = false;
            Figure straightLine = figureCreator.CreateFigure(new Point(), new Point(), shiftPressed);
            Assert.IsTrue(straightLine is StraightLine);
        }



    }
}
