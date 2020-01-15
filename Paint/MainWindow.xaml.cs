using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WriteableBitmap wb; //создает новый холст Image для рисования 
        byte blue = 0;
        byte green = 0;
        byte red = 0;
        byte alpha = 255;
        byte[] colorData = { 0, 0, 0, 255 }; //все для создания цвета
        bool isPressed = false; //передает состаяние мыши
        Point prevPoint; //точка начала коордиат


        public MainWindow()
        {
            InitializeComponent();

            wb = new WriteableBitmap(650, 600, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;

        }
        private byte[] HexToRGBConverter(String s)  // ------------------тут поправила, это правильно . женя
        {
            if (s.IndexOf('#') != -1)
                s = s.Replace("#", "");
            byte[] rgbColor = new byte[4]; // sARGB
            rgbColor[3] = Convert.ToByte(int.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            rgbColor[2] = Convert.ToByte(int.Parse(s.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            rgbColor[1] = Convert.ToByte(int.Parse(s.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            rgbColor[0] = Convert.ToByte(int.Parse(s.Substring(6, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            // rgbColor[0] = 0;
            return rgbColor;
        }

        private void SetPixel(Point p, byte[] colorData)//устанавливает пиксель в заданных координатах в заданном цвете
        {
            Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
            wb.WritePixels(rect, colorData, 4, 0);
            MainImage.Source = wb;
        }

        //private String ShowCurPoint(MouseEventArgs e)//показать на экране
        //{
        //        xPosition.Text = Convert.ToString(e.GetPosition(MainImage).X),
        //        yPosition.Text = Convert.ToString(e.GetPosition(MainImage).Y)


        //}

        private Point SetToCurPoint(MouseEventArgs e)//текущая точка
        {
            xPosition.Text = Convert.ToString(e.GetPosition(MainImage).X);
            yPosition.Text = Convert.ToString(e.GetPosition(MainImage).Y);
            return new Point(
                (int)e.GetPosition(MainImage).X,
                (int)e.GetPosition(MainImage).Y
            );
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)//движение мыши
        {
            Point curPoint = SetToCurPoint(e);

            if (isPressed)
            {
                //SetPixel(curPoint, colorData);
                DefineQuater(prevPoint, curPoint);
                //DrawLine(prevPoint, curPoint);

                //for (int i = 0; i <= 649; i++)
                //{
                //    Point p = new Point(i, 0);
                //    DefineQuater(new Point(325, 200), p);
                //}
                //for (int i = 0; i <= 649; i++)
                //{
                //    Point p = new Point(i, 399);
                //    DefineQuater(new Point(325, 200), p);
                //}
                //for (int i = 0; i <= 399; i++)
                //{
                //    Point p = new Point(0, i);
                //    DefineQuater(new Point(325, 200), p);
                //}
                //for (int i = 0; i <= 399; i++)
                //{
                //    Point p = new Point(649, i);
                //    DefineQuater(new Point(325, 200), p);
                //}


               // DefineQuater(new Point(325, 200), curPoint);

            }
            prevPoint = curPoint;
        }

        private void Button_Change_Color(object sender, RoutedEventArgs e) // ------------------тут поправила, это правильно . женя
        {

            string buttonStr = Convert.ToString(((Button)e.OriginalSource).Background);
            colorData = HexToRGBConverter(buttonStr);

            red = colorData[1];
            green = colorData[2];
            blue = colorData[3];

            rColor.Text = Convert.ToString(red);
            gColor.Text = Convert.ToString(green);
            bColor.Text = Convert.ToString(blue);
        }

        private void DrawLine(Point pStart, Point pFinish)//отрисовка линии
        {
            Point newP = new Point();
            double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
            double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;
            //bool XX = pStart.X > pFinish.X;

            if (deltaX > deltaY)
            {
                double k = deltaY / deltaX;
                for (int i = 0; i < deltaX; i++)
                {
                    newP.Y = (k * i + pStart.Y);
                    newP.X = i + pStart.X;
                    SetPixel(newP, colorData);
                }
            }
            else
            {
                double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {
                    newP.X = (k * i + pStart.X);
                    newP.Y = i + pStart.Y;
                    SetPixel(newP, colorData);
                }
            }
        }

        private void DrawLine1(Point pStart, Point pFinish)//отрисовка линии
        {
            Point newP = new Point();
            double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
            double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;
            bool XX = pStart.X > pFinish.X;

            if (deltaX > deltaY)
            {
                double k = deltaY / deltaX;
                for (int i = 0; i < deltaX; i++)
                {
                    newP.Y = (k * i + pStart.Y);
                    newP.X = pStart.X - i;
                    SetPixel(newP, colorData);
                }
            }
            else 
            {
                if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)
                {
                    pStart = pFinish;
                }
               double k = deltaX / deltaY;
                for (int i = 0; i < deltaY; i++)
                {
                  
                    newP.X = (k * i + pStart.X);
                    newP.Y = pStart.Y - i;
                    SetPixel(newP, colorData);
                }
            }
        }

        private void DefineQuater(Point pStart, Point pFinish)//определение четверти
        {
            if (pFinish.X >= pStart.X && pFinish.Y >= pStart.Y)//прямая начинается с права с верху в низ
            {
                DrawLine(pStart, pFinish);
            }
            else if (pFinish.X <= pStart.X && pFinish.Y <= pStart.Y)//прямая начинается с права с низу в верх
            {
                DrawLine(pFinish, pStart);
            }
            else if (pFinish.X > pStart.X && pFinish.Y < pStart.Y)//прямая начинается с лева с низу в верх
            {
                DrawLine1(pFinish, pStart);
            }
            else if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)//прямая начинается с лева с верху в низ
            {
                DrawLine1(pStart, pFinish);
            }
        }

        private void MainImage_MouseDown(object sender, MouseButtonEventArgs e)//клик
        {
            isPressed = true;
            prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
        }

        private void MainImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            wb = new WriteableBitmap(650, 600, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;
        }


    }
}

