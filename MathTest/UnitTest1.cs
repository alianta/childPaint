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
                default:
                    return new List<Point>();
            }
        }

        [TestCase("point1", "point2", "pointList1")]
        public void CreateSquereTest(string pStartName, string pFinishName, string expectedPointsListName)
        {
            RectangleCreator rect = new RectangleCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateSquere(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }



    }

}