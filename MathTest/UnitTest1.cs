using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint.Fabric
{
    [TestFixture]
    public class Tests
    {


        public Point GetPointByName(string name)
        {
            switch (name)
            {
                case "point1":
                    return new Point(1, 4);
                case "point2":
                    return new Point(4, 50);

                case "point3":
                    return new Point(-450,-100);
                case "point4":
                    return new Point(-300,0);
                case "point5":
                    return new Point(5,10);
                case "point6":
                    return new Point(10,5);
                case "point7":
                    return new Point(0,0);
                case "point8":
                    return new Point(0,0);
                case "point9":
                    return new Point(0, -5);
                case "point10":
                    return new Point(-5, 0);
                case "point11":
                    return new Point(800,0);
                case "point12":
                    return new Point(0,800);
                case "pointStartSquer1":
                    return new Point(20, 40);
                case "pointFinishSquer1":
                    return new Point(30, 100);
                default:
                    return new Point();
            }

        }

        public List<Point> GetPointsListByName(string name)
        {
            switch (name)
            {
                case "pointList1":
                    return new List<Point>() {
                        new Point(1,4),
                        new Point(4,4),
                        new Point(4,7),
                        new Point(1,7),
                    };
                case "pointList2":
                    return new List<Point>() {
                        new Point(-450,-100),
                        new Point(-300,-100),
                        new Point(-300,50),
                        new Point(-450,50),
                    };
                case "pointList3":
                    return new List<Point>() {
                        new Point(5,10),
                        new Point(5,5),
                        new Point(10,5),
                        new Point(10,10),
                    };
                case "pointList4":
                    return new List<Point>() {
                        new Point(0,0),
                        new Point(0,0),
                        new Point(0,0),
                        new Point(0,0),
                    };
                case "pointList5":
                    return new List<Point>() {
                        new Point(0, -5),
                        new Point(0,0),
                        new Point(-5,0),
                        new Point(-5,-5),
                    };
                case "pointList6":
                    return new List<Point>() {
                        new Point(800,0),
                        new Point(800,800),
                        new Point(0,800),
                        new Point(0,0),
                    };
                case "pointListSquer1":
                    return new List<Point>() {
                        new Point(20,40),
                        new Point(30,40),
                        new Point(30,50),
                        new Point(20,50),
                    };
                default:
                    return new List<Point>();
            }
        }

        [TestCase("point1", "point2", "pointList1")]
        [TestCase("point3", "point4", "pointList2")]
        [TestCase("pointStartSquer1", "pointFinishSquer1", "pointListSquer1")]
        public void CreateSquereTest(string pStartName, string pFinishName, string expectedPointsListName)
        {
            RectangleCreator rect = new RectangleCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateSquere(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }





        [TestCase("point5", "point6", "pointList3")]
        [TestCase("point7", "point8", "pointList4")]
        [TestCase("point9", "point10", "pointList5")]
        [TestCase("point11", "point12", "pointList6")]
        public void CreateIsoscaleRectangleTest(string pStartName, string pFinishName, string expectedPointsListName)
        {
            RectangleCreator rect = new RectangleCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateIsoscaleRectangle(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }




    }

}