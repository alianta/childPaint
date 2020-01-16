﻿using System;
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
        int thicknessLine = 1;//толщина линии
        Point pStart;// Начальная точка
        Point pFinish;// Конечная точка


        public MainWindow()
        {
            InitializeComponent();

            wb = new WriteableBitmap(650, 600, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;

        }
        
        //  ОБРАБОТКА СОБЫТИЙ 

        /// <summary>
        /// Метод обрабатывает двидение мыши по холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseMove(object sender, MouseEventArgs e)//движение мыши
        {

            ShowCurPoint(e);
            Point curPoint = SetToCurPoint(e);

            if (isPressed)
            {
                //SetPixel(curPoint, colorData);

                if (thicknessLine == 2)
                {
                    DrawLine(prevPoint, curPoint);
                    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                    DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));
                    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y - 1), new Point(curPoint.X - 1, curPoint.Y - 1));


                }
                else if (thicknessLine == 3)
                {

                    DrawLine(prevPoint, curPoint);
                    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y), new Point(curPoint.X + 1, curPoint.Y));
                    DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));
                    DrawLine(new Point(prevPoint.X, prevPoint.Y + 1), new Point(curPoint.X, curPoint.Y + 1));
                }
                else if (thicknessLine == 4)
                {

                    DrawLine(prevPoint, curPoint);
                    DrawLine(new Point(prevPoint.X, prevPoint.Y), new Point(curPoint.X, curPoint.Y));
                    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y), new Point(curPoint.X + 1, curPoint.Y));
                    DrawLine(new Point(prevPoint.X + 2, prevPoint.Y), new Point(curPoint.X + 2, curPoint.Y));
                    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y - 1), new Point(curPoint.X - 1, curPoint.Y - 1));
                    DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));
                    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y - 1), new Point(curPoint.X + 1, curPoint.Y - 1));
                    DrawLine(new Point(prevPoint.X + 2, prevPoint.Y - 1), new Point(curPoint.X + 2, curPoint.Y - 1));

                    DrawLine(new Point(prevPoint.X, prevPoint.Y + 1), new Point(curPoint.X, curPoint.Y + 1));
                    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y + 1), new Point(curPoint.X + 1, curPoint.Y + 1));
                    DrawLine(new Point(prevPoint.X, prevPoint.Y + 2), new Point(curPoint.X, curPoint.Y + 2));
                    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y + 2), new Point(curPoint.X + 1, curPoint.Y + 2));
                }
                {
                    DrawLine(prevPoint, curPoint);
                }

                //DrawLine(prevPoint, curPoint);

                //for (int i = 0; i <= 649; i++)
                //{
                //    Point p = new Point(i, 0);
                //    DrawLine(new Point(325, 200), p);
                //}
                //for (int i = 0; i <= 649; i++)
                //{
                //    Point p = new Point(i, 399);
                //    DrawLine(new Point(325, 200), p);
                //}
                //for (int i = 0; i <= 399; i++)
                //{
                //    Point p = new Point(0, i);
                //    DrawLine(new Point(325, 200), p);
                //}
                //for (int i = 0; i <= 399; i++)
                //{
                //    Point p = new Point(649, i);
                //    DrawLine(new Point(325, 200), p);
                //}


                // DrawLine(new Point(325, 200), curPoint);


            }
            prevPoint = curPoint;
        }

        /// <summary>
        /// Метод обрабатывает клик по иконке с цветами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Change_Color(object sender, RoutedEventArgs e)
        {
            string buttonStr = Convert.ToString(((Button)e.OriginalSource).Background);
            colorData = HexToRGBConverter(buttonStr);
            ShowCurColorRGB(colorData);
        }

        /// <summary>
        /// Метод обрабатывает MouseDown на холсте
        /// Ставит isPressed в true
        /// Задает стартовую точку координат
        /// Задает предыдущую точку координат  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            pStart = SetToCurPoint(e);
            prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
        }

        /// <summary>
        /// Метод обрабатывает MouseUp на холсте
        /// Возвращает isPressed в false
        /// Задает финишную точку координат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
            pFinish = SetToCurPoint(e);
        }

        /// <summary>
        /// Метод обрабатывает нажатие на кнопку очищения холста
        /// Задает новый битмап
        /// Ставит битмап в sourse холста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            wb = new WriteableBitmap(650, 600, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;
        }

        /// <summary>
        /// Метод обрабатывающй нажатие на кнопки изменения торщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Change_Thickness(object sender, RoutedEventArgs e)
        {
            string thickness = Convert.ToString(((Button)e.OriginalSource).Content);
            if (thickness == "x1")
            {
                thicknessLine = 1;
            }
            else if (thickness == "x2")
            {
                thicknessLine = 2;
            }
            else if (thickness == "x3")
            {
                thicknessLine = 3;
            }
            else if (thickness == "x4")
            {
                thicknessLine = 4;
            }
        }


        //  ВНУТРЕННИЕ МЕТОДЫ


        /// <summary>
        /// Метод конвертирует цвета из hex в rgb
        /// </summary>
        /// <param name="s">Строка. Цвет в формате hex </param>
        /// <returns>Возвращает массив byte[] {alpha, red, green, blue}</returns>
        private byte[] HexToRGBConverter(String s)
        {
            if (s.IndexOf('#') != -1)
                s = s.Replace("#", "");
            byte[] rgbColor = new byte[4]; // sARGB
            rgbColor[3] = Convert.ToByte(int.Parse(s.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            rgbColor[2] = Convert.ToByte(int.Parse(s.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            rgbColor[1] = Convert.ToByte(int.Parse(s.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            rgbColor[0] = Convert.ToByte(int.Parse(s.Substring(6, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            return rgbColor;
        }

        /// <summary>
        /// Метод устанавливает пиксель в заданных координатах в заданном цвете
        /// </summary>
        /// <param name="p">Point. Координаты точки</param>
        /// <param name="colorData">Массив. Цвет в aRGB</param>
        private void SetPixel(Point p, byte[] colorData)
        {
            Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
            wb.WritePixels(rect, colorData, 4, 0);
            MainImage.Source = wb;
        }

        /// <summary>
        /// Метод выводит в 2 текстбокса координаты позиции мыши
        /// </summary>
        /// <param name="e"></param>
        private void ShowCurPoint(MouseEventArgs e)
        {
            xPosition.Text = Convert.ToString(e.GetPosition(MainImage).X);
            yPosition.Text = Convert.ToString(e.GetPosition(MainImage).Y);
        }

        /// <summary>
        /// Метод задает текущую точку
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Возвращает Point с текущими координатами</returns>
        private Point SetToCurPoint(MouseEventArgs e)
        {
            return new Point(
                (int)e.GetPosition(MainImage).X,
                (int)e.GetPosition(MainImage).Y
            );
        }

        /// <summary>
        /// Метод выводит в 3 текстбокса текущий RGB цвет 
        /// </summary>
        /// <param name="colorData">byte[] {alpha, red, green, blue}</param>
        private void ShowCurColorRGB(byte[] colorData)
        {
            red = colorData[1];
            green = colorData[2];
            blue = colorData[3];

            rColor.Text = Convert.ToString(red);
            gColor.Text = Convert.ToString(green);
            bColor.Text = Convert.ToString(blue);
        }

        /// <summary>
        /// Метод риcует линию по двум координатам дял 3 и 4 четвертей 
        /// </summary>
        /// <param name="pStart">Point точка старта</param>
        /// <param name="pFinish">Point точка финиша</param>
        private void DrawLineInThirdTourthQuarters(Point pStart, Point pFinish)
        {
            Point newP = new Point();
            double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
            double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;

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

        /// <summary>
        /// Метод римует линию по по двум координатам для 1 и 2 четвертей
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        private void DrawLineInFirstSecondQuarters(Point pStart, Point pFinish)
        {
            Point newP = new Point();
            double deltaX = Math.Abs(pFinish.X - pStart.X) + 1;
            double deltaY = Math.Abs(pFinish.Y - pStart.Y) + 1;

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

        /// <summary>
        /// Рисование линии попиксельно
        /// </summary>
        /// <param name="pStart">Начальная точка</param>
        /// <param name="pFinish">конечная точка</param>
        private void DrawLine(Point pStart, Point pFinish)//определение четверти
        {
            if (pFinish.X >= pStart.X && pFinish.Y >= pStart.Y)//прямая начинается с права с верху в низ
            {
                DrawLineInThirdTourthQuarters(pStart, pFinish);
            }
            else if (pFinish.X <= pStart.X && pFinish.Y <= pStart.Y)//прямая начинается с права с низу в верх
            {
                DrawLineInThirdTourthQuarters(pFinish, pStart);
            }
            else if (pFinish.X > pStart.X && pFinish.Y < pStart.Y)//прямая начинается с лева с низу в верх
            {
                DrawLineInFirstSecondQuarters(pFinish, pStart);
            }
            else if (pFinish.X < pStart.X && pFinish.Y > pStart.Y)//прямая начинается с лева с верху в низ
            {
                DrawLineInFirstSecondQuarters(pStart, pFinish);
            }
        }

        /// <summary>
        /// Рисование окружности попиксельно
        /// </summary>
        /// <param name="pStart">Начальная точка по клику</param>
        /// <param name="pFinish">Конечная точка по клику</param>
        private void DrawCircle(Point pStart, Point pFinish)
        {
            double R = Math.Sqrt(Math.Pow((pFinish.X - pStart.X), 2) + Math.Pow((pFinish.Y - pStart.Y), 2));
            //double x = 0;
            //double y = R;
            //double delta = 1 - 2 * R;
            //double error = 0;
            //while (y >= 0)
            //{
            //    SetPixel(new Point(pStart.X + x, pStart.Y + y), colorData);
            //    SetPixel(new Point(pStart.X + x, pStart.Y - y), colorData);
            //    SetPixel(new Point(pStart.X - x, pStart.Y + y), colorData);
            //    SetPixel(new Point(pStart.X - x, pStart.Y - y), colorData);
            //    error = 2 * (delta + y) - 1;
            //    if ((delta < 0) && (error <= 0))
            //        delta += 2 * (++x) + 1;
            //    if ((delta > 0) && (error > 0))
            //        delta -= 2 * (--y) + 1;
            //    delta += 2 * (++x - y--);

            for (double i = -R; i < R; i++)
            {
                double v = Math.Round(Math.Sqrt(R * R - i * i));
                for (double j = -v; j < v; j++)
                {
                    SetPixel(new Point(i + pStart.X, j + pStart.Y), colorData);
                }
            }
        }

        /// <summary>
        /// Метод рисующий квадрат по двум точкам на одной стороне
        /// </summary>
        /// <param name="pointB"></param>
        private void Draw_Squere(Point pointB)
        {
            DrawLine(prevPoint, pointB); 

            double katet1, katet2;
            katet1 = prevPoint.Y - pointB.Y;
            katet2 = pointB.X - prevPoint.X;

            Point pointD = new Point();
            pointD.X = prevPoint.X + katet1;
            pointD.Y = prevPoint.Y + katet2;

            Point pointC = new Point();
            pointC.X = pointB.X + katet1;
            pointC.Y = pointB.Y + katet2;

            DrawLine(pointB, pointC);
            DrawLine(pointC, pointD);
            DrawLine(pointD, prevPoint);
        }

        /// <summary>
        /// Метод рисует прямоугольник по двум точкам на противоположных углах
        /// </summary>
        /// <param name="pointC"></param>
        private void Draw_Rectangle(Point pointC)
        {
            Point pointD = new Point();
            pointD.X = prevPoint.X;
            pointD.Y = pointC.Y;

            Point pointB = new Point();
            pointB.X = pointC.X;
            pointB.Y = prevPoint.Y;
            DrawLine(prevPoint, pointB);
            DrawLine(pointB, pointC);
            DrawLine(pointC, pointD);
            DrawLine(pointD, prevPoint);
        }

        //ПРИВЯЗКА К КОНПКАМ: КВАДРАТ, ПРЯМОУГОЛЬНИК
        //private void Button_Square(object sender, RoutedEventArgs e)
        //{
        //    type = "square";
        //}

        //private void Button_Rectangle(object sender, RoutedEventArgs e)
        //{
        //    type = "rectangle";
        //}




        /* ComboBox comboBox = (ComboBox)sender;
         ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
         MessageBox.Show(selectedItem.Content.ToString());*/

        //hgfjknbfm 


    }
}

