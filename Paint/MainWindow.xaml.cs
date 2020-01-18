using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WriteableBitmap wb; //создает новый холст Image для рисования 
        WriteableBitmap curState;
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
        Point temp;
        int type = 1;//состояние кнопки переименовать!
        Rastr.Figure figure;
        bool shift = false;


        bool line = false;
        bool square = false;
        bool rectangle = false;
        bool circle = false;
        bool oval = false;
        bool triangle = false;
        bool polygon = false;
        bool tree = false;
        bool lines = false;
        double angle = Math.PI / 2; //Угол поворота на 90 градусов
        double ang1 = Math.PI / 4;  //Угол поворота на 45 градусов
        double ang2 = Math.PI / 6;  //Угол поворота на 30 градусов
        int n = 100;//количество сторон
        double R;//расстояние от центра до стороны
        Point[] p; //массив точек будущего многоугольника

        public MainWindow()
        {
            InitializeComponent();

            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;
            figure = new Rastr.Line();
            //  DrawEllipse(new Point(200, 200), new Point(300, 250));

        }

        //  ОБРАБОТКА СОБЫТИЙ 

        /// <summary>
        /// Метод обрабатывающий кнопки фигур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFigure_Click(object sender, RoutedEventArgs e)
        {
            line = false;
            square = false;
            rectangle = false;
            circle = false;
            oval = false;
            polygon = false;
            triangle = false;
            tree = false;
            lines = false;

            if (sender.Equals(btnLine))
            {
                line = true;
            }
            else if (sender.Equals(btnSquare))
            {
                square = true;
            }
            else if (sender.Equals(btnRectangle))
            {
                rectangle = true;
            }
            else if (sender.Equals(btnCircle))
            {
                circle = true;
            }
            else if (sender.Equals(btnOval))
            {
                oval = true;
            }
            else if (sender.Equals(btnTriangle))
            {
                triangle = true;
            }
            else if (sender.Equals(btnPolygon))
            {
                polygon = true;
            }
            else if (sender.Equals(btnTree))
            {
                tree = true;
            }
            else if (sender.Equals(btnLines))
            {
                lines = true;
            }
            else
            {
                xPosition.Text = "Алярма!";
            }

        }

        /// <summary>
        /// Метод обрабатывает двидение мыши по холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseMove(object sender, MouseEventArgs e)//движение мыши
        {
            shift = Keyboard.IsKeyDown(Key.LeftShift);
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

                if (type == 2)
                {
                    wb = new WriteableBitmap(curState);
                    figure.Draw(wb, pStart, curPoint, shift);
                    MainImage.Source = wb;
                }

                if (line)
                {
                    DrawLine(prevPoint, curPoint);
                }
                if (square)
                {
                    Draw_Squere(curPoint);
                }
                if (rectangle)
                {
                    Draw_Rectangle(curPoint);
                }
                if (circle)
                {
                    DrawCircle(prevPoint, curPoint);
                }
                if (oval)
                {
                    DrawEllipse(prevPoint, curPoint);
                }
                if (triangle)
                {
                    // DrawTree(prevPoint, curPoint, n , angle);                                  
                }
                if (polygon)
                {
                    Draw_Polygon(prevPoint, curPoint);
                }
                if (tree)
                {
                    DrawTree(prevPoint, curPoint, n, angle);
                }
                if (lines)
                {
                    DrawByLines(prevPoint, curPoint, e);
                }



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

            if (line)
            {
                prevPoint = curPoint;
            }



            //  prevPoint = curPoint;
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
        /// Метод обрабатывает клик по кнопке треугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void triangle_Click(object sender, RoutedEventArgs e)
        {
            type = 2;
            figure = new Triangle();
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
            temp = pStart;
            prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
            curState = new WriteableBitmap(wb);
        }

        /// <summary>
        /// Метод обрабатывает MouseUp на холсте
        /// Возвращает isPressed в false
        /// Задает финишную точку координат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
            pFinish = SetToCurPoint(e);
            if (lines)
            {
                isPressed = true;
                pStart = pFinish;
            }
        }

        /// <summary>
        /// Метод обрывает рисование линии при выведении курсора из-за холста и продолжает, когда возвращаешься
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainImage_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isPressed == true)
            {
                prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
            }

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
            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
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
            if (p.X > 0 && p.X < (int)MainImage.Width && p.Y > 0 && p.Y < (int)MainImage.Height)
            {
                Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
                wb.WritePixels(rect, colorData, 4, 0);
                //   curState.WritePixels(rect, colorData, 4, 0);
                MainImage.Source = wb;
            }
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

            wb = new WriteableBitmap(curState);
            int R = (int)Math.Sqrt((Math.Pow((pFinish.X - pStart.X), 2)) + (Math.Pow((pFinish.Y - pStart.Y), 2)));
            double a = Math.Sqrt(2) / 2;

            for (int i = (int)(-a * R); i < (int)(a * R); i++)
            {
                int newY1 = (int)(pStart.Y - Math.Sqrt(R * R - i * i));

                SetPixel(new Point(pStart.X + i, newY1), colorData);
                SetPixel(new Point(pStart.X - i, newY1), colorData);

                int newY2 = (int)(pStart.Y + Math.Sqrt(R * R - i * i));
                SetPixel(new Point(pStart.X + i, newY2), colorData);
                SetPixel(new Point(pStart.X - i, newY2), colorData);
            }
            for (int i = (int)(-a * R); i < (int)(a * R); i++)
            {
                int newX1 = (int)(pStart.X - Math.Sqrt(R * R - i * i));

                SetPixel(new Point(newX1, pStart.Y - i), colorData);
                SetPixel(new Point(newX1, pStart.Y + i), colorData);
                int newX2 = (int)(pStart.X + Math.Sqrt(R * R - i * i));
                SetPixel(new Point(newX2, pStart.Y + i), colorData);
                SetPixel(new Point(newX2, pStart.Y - i), colorData);
            }

        }
        

        /// <summary>
        /// Рисование элипса попиксельно
        /// </summary>
        /// <param name="pStart">Центр элипса</param>
        /// <param name="pFinish">Правый нижний угол прямоугольника описанного вокруг элипса</param>
        private void DrawEllipse(Point pStart, Point pFinish)
        {

            wb = new WriteableBitmap(curState);
            double a = (pFinish.X > pStart.X) ? pFinish.X - pStart.X : pStart.X - pFinish.X;
            double b = (pFinish.Y > pStart.Y) ? pFinish.Y - pStart.Y : pStart.Y - pFinish.Y;
            double aInPowerTwo = a * a;
            double bInPowerTwo = b * b;

            for (int i = 0; i < b; i++)
            {
                int newX1 = (int)(pStart.X + Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                SetPixel(new Point(newX1, pStart.Y + i), colorData);
                SetPixel(new Point(newX1, pStart.Y - i), colorData);

                int newX2 = (int)(pStart.X - Math.Sqrt((aInPowerTwo * (bInPowerTwo - i * i)) / bInPowerTwo));
                SetPixel(new Point(newX2, pStart.Y + i), colorData);
                SetPixel(new Point(newX2, pStart.Y - i), colorData);
            }
            for (int i = 0; i < a; i++)
            {
                int newY1 = (int)(pStart.Y + Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                SetPixel(new Point(pStart.X + i, newY1), colorData);
                SetPixel(new Point(pStart.X - i, newY1), colorData);

                int newY2 = (int)(pStart.Y - Math.Sqrt((bInPowerTwo * (aInPowerTwo - i * i)) / aInPowerTwo));
                SetPixel(new Point(pStart.X + i, newY2), colorData);
                SetPixel(new Point(pStart.X - i, newY2), colorData);
            }

        }


        /// <summary>
        /// Метод рисующий квадрат по двум точкам на одной стороне
        /// </summary>
        /// <param name="pointB"></param>
        private void Draw_Squere(Point pointB)
        {
            wb = new WriteableBitmap(curState);
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
            wb = new WriteableBitmap(curState);
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

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            // Настраиваем параметры диалога
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            dlg.FileName = "Document"; // Имя по-умолчанию
            dlg.DefaultExt = ".jpg"; // Расширение по-умолчанию
            dlg.Filter = "Jpeg Image (.jpg)|*.jpg"; // Фильтр по расширениям

            // Обработка результата работы диалога
            if (result == true)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)MainImage.Source));
                using (FileStream stream = new FileStream(dlg.FileName, FileMode.Create))
                    encoder.Save(stream);
            }
        }

        /// <summary>
        /// Метод создает массив точек многоугольника
        /// </summary>
        /// <param name="angle"></param>
        private void lineAngle(double angle)
        {
            double z = 0; int i = 0;
            while (i < n + 1)
            {
                p[i].X = prevPoint.X + (int)(Math.Round(Math.Cos(z / 180 * Math.PI) * R));
                p[i].Y = prevPoint.Y - (int)(Math.Round(Math.Sin(z / 180 * Math.PI) * R));
                z = z + angle;
                i++;
            }
        }

        /// <summary>
        /// Метод рисует n-угольник
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pFinish"></param>
        private void Draw_Polygon(Point pStart, Point pFinish)
        {

            wb = new WriteableBitmap(curState);
            n = Convert.ToInt32(sides.Text);

            R = Math.Sqrt(Math.Pow((pFinish.X - pStart.X), 2) + Math.Pow((pFinish.Y - pStart.Y), 2));

            p = new Point[n + 1];
            lineAngle((double)(360.0 / (double)n));
            int i = n;

            while (i > 0)
            {
                DrawLine(p[i], p[i - 1]);
                i = i - 1;
            }
        }

        /// <summary>
        /// Метод рисующий Фрактал Дерево Пифагора 
        /// </summary>
        /// <param name="pStart">Стартовая точка</param>
        /// <param name="pFinish">Финишная точка</param>
        /// <param name="a">Параметр, который фиксирует количество итераций в рекурсии</param>
        /// <param name="angle">Угол поворота на каждой итерации</param>
        /// <returns></returns>
        public int DrawTree(Point pStart, Point pFinish, double a, double angle)
        {
            n = Convert.ToInt32(sides.Text);
            double x = pStart.X;
            double y = pStart.Y;

            if (a > 2)
            {
                a *= 0.7; //Меняем параметр a

                //Считаем координаты для вершины-ребенка
                pFinish.X = Math.Round(x + a * Math.Cos(angle));
                pFinish.Y = Math.Round(y - a * Math.Sin(angle));

                //рисуем линию между вершинами           
                DrawLine(pStart, pFinish);

                DrawTree(pFinish, pFinish, a, angle + ang1);
                DrawTree(pFinish, pFinish, a, angle - ang2);
            }
            return 0;
        }

        public void DrawByLines(Point pStart, Point pFinish, MouseEventArgs e)
        {
            wb = new WriteableBitmap(curState);
            Point temp = pStart;
            DrawLine(pStart, pFinish);
            
            if (e.RightButton == MouseButtonState.Pressed)
            {
                pFinish = temp;
            }
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

        //private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    isPressed = false;
        //    pFinish = SetToCurPoint(e);
        //}


        /* ComboBox comboBox = (ComboBox)sender;
         ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
         MessageBox.Show(selectedItem.Content.ToString());*/

        //hgfjknbfm 


    }
}

