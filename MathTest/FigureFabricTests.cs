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
                    return new Point(-450, -100);
                case "point4":
                    return new Point(-300, 0);
                case "point5":
                    return new Point(5, 10);
                case "point6":
                    return new Point(10, 5);
                case "point7":
                    return new Point(0, 0);
                case "point8":
                    return new Point(0, 0);
                case "point9":
                    return new Point(0, -5);
                case "point10":
                    return new Point(-5, 0);
                case "point11":
                    return new Point(800, 0);
                case "point12":
                    return new Point(0, 800);
                case "pointStartSquer1":
                    return new Point(20, 40);
                case "pointFinishSquer1":
                    return new Point(30, 100);
                case "pointStartIsoscaleTriangle1":
                    return new Point(10, 20);
                case "pointFinishIsoscaleTriangle1":
                    return new Point(30, 10);
                case "pointStartIsoscaleTriangle2":
                    return new Point(129, 170);
                case "pointFinishIsoscaleTriangle2":
                    return new Point(331, 40);

                case "pointStartRectangularTriangle1":
                    return new Point(2, 3);
                case "pointFinishRectangularTriangle1":
                    return new Point(5, 1);
                case "pointStartRectangularTriangle3":
                    return new Point(0, 0);
                case "pointFinishRectangularTriangle4":
                    return new Point(-100, 100);
                case "pointStartRectangularTriangle5":
                    return new Point(0, 0);
                case "pointFinishRectangularTriangle6":
                    return new Point(0, 0);
                case "pointStartRectangularTriangle7":
                    return new Point(0, 900);
                case "pointFinishRectangularTriangle8":
                    return new Point(-600, 0);
                case "pointStartRectangularTriangle9":
                    return new Point(-230, -800);
                case "pointFinishRectangularTriangle10":
                    return new Point(-900, -700);
                case "pointStartRectangularTriangle11":
                    return new Point(-200, -200);
                case "pointFinishRectangularTriangle12":
                    return new Point(-200, -200);

                case "pointStartPolugon1":
                    return new Point(500, 200);
                case "pointFinishPolygon1":
                    return new Point(300, 100);
                case "pointStartPolugon2":
                    return new Point(50, 500);
                case "pointFinishPolygon2":
                    return new Point(400, 400);
                case "pointStartPolugon3":
                    return new Point(300, 300);
                case "pointFinishPolygon3":
                    return new Point(400, 400);


                case "pointStartCircle1":
                    return new Point(0, 0);
                case "pointFinishCircle1":
                    return new Point(3, 4);

                case "pointStartEllipce1":
                    return new Point(8, 3);
                case "pointFinishEllipce1":
                    return new Point(10, 6);

                case "pointStartRhombus1":
                    return new Point(300, 300);
                case "pointFinishRhombus1":
                    return new Point(300, 340);

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
                case "pointListIsoscaleTriangle1":
                    return new List<Point>() {
                        new Point(10,20),
                        new Point(30,10),
                        new Point(10,10),
                        
                    };
                case "pointListIsoscaleTriangle2":
                    return new List<Point>() {
                        new Point(129,170),
                        new Point(331,40),
                        new Point(129,40),
                       
                    };


                case "pointListRectangularTriangle1":
                    return new List<Point>() {
                        new Point(2,3),
                        new Point(-1,1),
                        new Point(5,1),
                        
                    };
                case "pointListRectangularTriangle2":
                    return new List<Point>() {
                        new Point(0,0),
                        new Point(100,100),
                        new Point(-100,100),
                       
                    };
                case "pointListRectangularTriangle3":
                    return new List<Point>() {
                        new Point(0,0),
                        new Point(0,0),
                        new Point(0,0),
                        new Point(0,0),
                        new Point(0,0),
                        new Point(0,0),
                    };
                case "pointListRectangularTriangle4":
                    return new List<Point>() {
                        new Point(0,900),
                        new Point(-600,0),
                        new Point(0,900),
                        new Point(600,0),
                        new Point(600,0),
                        new Point(-600,0),
                    };
                case "pointListRectangularTriangle5":
                    return new List<Point>() {
                        new Point(-230,-800),
                        new Point(-900,-700),
                        new Point(-230,-800),
                        new Point(440,-700),
                        new Point(440,-700),
                        new Point(-900,-700),
                    };
                case "pointListRectangularTriangle6":
                    return new List<Point>() {
                        new Point(-200,-200),
                        new Point(-200,-200),
                        new Point(-200,-200),
                        new Point(-200,-200),
                        new Point(-200,-200),
                        new Point(-200,-200),
                    };
                case "pointListPolygon1":
                    return new List<Point>() {
                        new Point(300, 100),
                        new Point(513, 423),
                        new Point(687, 77)
                    };
                case "pointListPolygon2":
                    return new List<Point>() {
                        new Point(400, 400),
                        new Point(63, 136),
                        new Point(-292, 375),
                        new Point(-174, 787),
                        new Point(253, 802)
                    };
                case "pointListPolygon3":
                    return new List<Point>() {
                        new Point(400, 400),
                        new Point(441, 300),
                        new Point(400, 200),
                        new Point(300, 159),
                        new Point(200, 200),
                        new Point(159, 300),
                        new Point(200, 400),
                        new Point(300, 441)
                    };
                case "pointListCircle1":
                    return new List<Point>() {
                        //������ ���
                        new Point(-0,-5),
                        new Point(1,-4),
                        new Point(2,-4),
                        new Point(3,-4),
                        //������ ���
                        new Point(4,-3),
                        new Point(4,-2),
                        new Point(4,-1),
                        //������ ���
                        new Point(5,0),
                        new Point(4,1),
                        new Point(4,2),
                        new Point(4,3),
                        //��������� ���
                        new Point(3,4),
                        new Point(2,4),
                        new Point(1,4),
                        new Point(0,5),
                        new Point(-1,4),
                        new Point(-2,4),
                        new Point(-3,4),
                        //������ ���
                        new Point(-4,3),
                        new Point(-4,2),
                        new Point(-4,1),
                        //������� ���
                        new Point(-5,0),
                        new Point(-4,-1),
                        new Point(-4,-2),
                        new Point(-4,-3),
                        //������� ���
                        new Point(-3,-4),
                        new Point(-2,-4),
                        new Point(-1,-4),

                    };

                case "pointListEllipce1":
                    return new List<Point>() {
                        new Point(8,0),
                        new Point(9,0),
                        new Point(9,0),
                        new Point(9,1),
                        new Point(9,2),
                        new Point(10,3),
                        new Point(9,4),
                        new Point(9,5),
                        new Point(9,5),
                        new Point(8,5),
                        new Point(8,6),
                        new Point(7,5),
                        new Point(6,5),
                        new Point(6,4),
                        new Point(6,3),
                        new Point(6,3),
                        new Point(6,2),
                        new Point(6,1),
                        new Point(6,0),
                        new Point(7,0),
                    };

                case "pointListRhombus":
                    return new List<Point>() {
                        new Point(300,300),
                        new Point(363,300),
                        new Point(340,340),
                        new Point(276,340)
                    };

                default:
                    return new List<Point>();
            }
        }

        //�������
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

        //�������������
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

        //�������������� �����������
        [TestCase("pointStartIsoscaleTriangle1", "pointFinishIsoscaleTriangle1", "pointListIsoscaleTriangle1")]
        [TestCase("pointStartIsoscaleTriangle2", "pointFinishIsoscaleTriangle2", "pointListIsoscaleTriangle2")]
        public void CreateIsoscaleTriangleTest(string pStartName, string pFinishName, string expectedPointsListName)
        {
            TriangleCreator rect = new TriangleCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateIsoscaleTriangle(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }

        //������������� �����������
        [TestCase("pointStartRectangularTriangle1", "pointFinishRectangularTriangle1", "pointListRectangularTriangle1")]
        [TestCase("pointStartRectangularTriangle3", "pointFinishRectangularTriangle4", "pointListRectangularTriangle2")]
        //[TestCase("pointStartRectangularTriangle5", "pointFinishRectangularTriangle6", "pointListRectangularTriangle3")]
        //[TestCase("pointStartRectangularTriangle7", "pointFinishRectangularTriangle8", "pointListRectangularTriangle4")]
        //[TestCase("pointStartRectangularTriangle9", "pointFinishRectangularTriangle10", "pointListRectangularTriangle5")]
        //[TestCase("pointStartRectangularTriangle11", "pointFinishRectangularTriangle12", "pointListRectangularTriangle6")]
        public void CreateRectangularTriangle(string pStartName, string pFinishName, string expectedPointsListName)
        {
            TriangleCreator rect = new TriangleCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateRectangularTriangle(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }


        // ������������

        [TestCase("pointStartPolugon1", "pointFinishPolygon1", 3, "pointListPolygon1")]
        [TestCase("pointStartPolugon2", "pointFinishPolygon2", 5, "pointListPolygon2")]
        [TestCase("pointStartPolugon3", "pointFinishPolygon3", 8, "pointListPolygon3")]
        public void CreatePolygonTriangle(string pStartName, string pFinishName, int sides, string expectedPointsListName)
        {
            PolygonCreator polygon = new PolygonCreator(sides);
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = polygon.CreatePolygon(pStart, pFinish, sides).Points;

            CollectionAssert.AreEqual(expected, actual);
        }

        //����
        [TestCase("pointStartCircle1", "pointFinishCircle1", "pointListCircle1")]
        public void CreateCircle(string pStartName, string pFinishName, string expectedPointsListName)
        {
            EllipceCreator rect = new EllipceCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateCircle(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }

        //������
        [TestCase("pointStartEllipce1", "pointFinishEllipce1", "pointListEllipce1")]
        public void CreateEllipce(string pStartName, string pFinishName, string expectedPointsListName)
        {
            EllipceCreator rect = new EllipceCreator();
            Point pStart = GetPointByName(pStartName);
            Point pFinish = GetPointByName(pFinishName);
            List<Point> expected = GetPointsListByName(expectedPointsListName);

            List<Point> actual = rect.CreateEllipce(pStart, pFinish).Points;

            CollectionAssert.AreEqual(expected, actual);
        }



        public Point GetPointsByName(string name)
        {
            switch (name)
            {
                case "pointStartParal1":
                    return new Point(300, 300);
                case "pointFinishParal1":
                    return new Point(300, 340);
                case "pointStartParal2":
                    return new Point(300, 300);
                case "pointFinishParal2":
                    return new Point(300, 300);
                case "pointStartParal3":
                    return new Point(0, 0);
                case "pointFinishParal3":
                    return new Point(0, 0);
                case "pointStartParal4":
                    return new Point(-850, -600);
                case "pointFinishParal4":
                    return new Point(-900, -700);
                default:
                    return new Point();
            }
        }

        public List<Point> GetPointsListByName1(string name)
        {
            switch (name)
            {
                case "pointListParal1":
                    return new List<Point>() {
                        new Point(300,300),
                        new Point(363,300),
                        new Point(340,340),
                        new Point(276,340)
                    };
                case "pointListParal2":
                    return new List<Point>() {
                        new Point(300,300)
                    };

                        case "pointListParal3":
                    return new List<Point>() {
                        new Point(0,0)
                    };
                         case "pointListParal4":
                    return new List<Point>() {
                        new Point(-850,-600),
                        new Point(-692,-600),
                        new Point(-750,-700),
                        new Point(-907,-700),
                        
                    };
                default:
                    return new List<Point>();
            }
        }

        //��������������
        [TestCase("pointStartParal1", "pointFinishParal1", "pointListParal1")]
        [TestCase("pointStartParal2", "pointFinishParal2", "pointListParal2")]
        [TestCase("pointStartParal3", "pointFinishParal3", "pointListParal3")]
        [TestCase("pointStartParal4", "pointFinishParal4", "pointListParal4")]
        public void CreateParallelogramTest(string pStartName, string pFinishName, string expectedPointsListName)
        {
            ParallelogramCreator rhombus = new ParallelogramCreator();
            Point pStart = GetPointsByName(pStartName);
            Point pFinish = GetPointsByName(pFinishName);
            List<Point> expected = GetPointsListByName1(expectedPointsListName);
            List<Point> actual = rhombus.CreateParallelogram(pStart, pFinish).Points;
            CollectionAssert.AreEqual(expected, actual);
        }
    }

}