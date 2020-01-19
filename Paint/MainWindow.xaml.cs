using Paint.Rastr;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        bool rectangle = false;
        bool circle = false;
        bool oval = false;
        bool triangle = false;
        bool polygon = false;
        // bool tree = false;
        bool lines = false;
        int n = 100;//количество сторон
        double R;//расстояние от центра до стороны
        Point[] p; //массив точек будущего многоугольника
        public MainWindow()
        {
            InitializeComponent();
            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;
            figure = new Rastr.Pixel();
            ShowCurColorRGB(colorData);
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
            rectangle = false;
            circle = false;
            polygon = false;
            triangle = false;
            //  tree = false;
            lines = false;
            if (sender.Equals(btnLine))
            {
                line = true;
            }
            else if (sender.Equals(btnRectangle))
            {
                rectangle = true;
            }
            else if (sender.Equals(btnCircle))
            {
                circle = true;
            }
            else if (sender.Equals(btnTriangle))
            {
                triangle = true;
            }
            else if (sender.Equals(btnPolygon))
            {
                polygon = true;
            }
            //else if (sender.Equals(btnTree))
            //{
            //    tree = true;
            //}
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
                //if (thicknessLine == 2)
                //{
                //    DrawLine(prevPoint, curPoint);
                //    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));
                //    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y - 1), new Point(curPoint.X - 1, curPoint.Y - 1));
                //}
                //else if (thicknessLine == 3)
                //{
                //    DrawLine(prevPoint, curPoint);
                //    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y), new Point(curPoint.X + 1, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y + 1), new Point(curPoint.X, curPoint.Y + 1));
                //}
                //else if (thicknessLine == 4)
                //{
                //    DrawLine(prevPoint, curPoint);
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y), new Point(curPoint.X, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y), new Point(curPoint.X - 1, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y), new Point(curPoint.X + 1, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X + 2, prevPoint.Y), new Point(curPoint.X + 2, curPoint.Y));
                //    DrawLine(new Point(prevPoint.X - 1, prevPoint.Y - 1), new Point(curPoint.X - 1, curPoint.Y - 1));
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y - 1), new Point(curPoint.X, curPoint.Y - 1));
                //    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y - 1), new Point(curPoint.X + 1, curPoint.Y - 1));
                //    DrawLine(new Point(prevPoint.X + 2, prevPoint.Y - 1), new Point(curPoint.X + 2, curPoint.Y - 1));
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y + 1), new Point(curPoint.X, curPoint.Y + 1));
                //    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y + 1), new Point(curPoint.X + 1, curPoint.Y + 1));
                //    DrawLine(new Point(prevPoint.X, prevPoint.Y + 2), new Point(curPoint.X, curPoint.Y + 2));
                //    DrawLine(new Point(prevPoint.X + 1, prevPoint.Y + 2), new Point(curPoint.X + 1, curPoint.Y + 2));
                //}
                if (type == 7)
                {
                    figure.Draw(wb, prevPoint, curPoint, shift);
                    MainImage.Source = wb;
                }
                if (type == 2)
                {
                    wb = new WriteableBitmap(curState);
                    figure.Draw(wb, pStart, curPoint, shift);
                    MainImage.Source = wb;
                }
                if (type == 3)
                {
                    wb = new WriteableBitmap(curState);
                    figure.Draw(wb, pStart, curPoint, shift);
                    MainImage.Source = wb;
                }
                if (type == 4)
                {
                    wb = new WriteableBitmap(curState);
                    figure.Draw(wb, pStart, curPoint, shift);
                    MainImage.Source = wb;
                }
                if (type == 6)
                {
                    wb = new WriteableBitmap(curState);
                    figure.Draw(wb, pStart, curPoint, sides.Text);
                    MainImage.Source = wb;
                }
                if (type == 11)
                {
                    wb = new WriteableBitmap(curState);
                    figure.Draw(wb, pStart, curPoint, shift);
                    MainImage.Source = wb;
                    // DrawTree(prevPoint, curPoint, n, angle);
                }

                if (line)
                {
                    type = 7;
                    figure = new Line();
                    prevPoint = curPoint;
                }
                if (circle)
                {
                    type = 3;
                    figure = new Ellipce();
                }
                if (triangle)
                {
                    type = 2;
                    figure = new Triangle();                                 
                }
                if (rectangle)
                {
                    type = 4;
                    figure = new Rectangle();
                }
                if (polygon)
                {
                    type = 6;
                    figure = new Poligon();
                    //Draw_Polygon(prevPoint, curPoint);
                }
                
                //if (lines)
                //{
                //    DrawByLines(prevPoint, curPoint, e);
                //}
            }
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
        /// </summary>        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void tree_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sides.Text != "")
        //    {
        //        n = Convert.ToInt32(sides.Text);
        //    }
        //    else
        //    {
        //        n = 100;
        //    }
        //    type = 11;
        //    figure = new FractalTree(n);
        //}

        /// <summary>
        /// Метод обрабатывает MouseDown на холсте
        /// Ставит isPressed в true
        /// Задает стартовую точку координат
        /// Задает предыдущую точку координат  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        private void MainImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            pStart = SetToCurPoint(e);
            temp = pStart;
            prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
            curState = new WriteableBitmap(wb);
            //figure.Draw(wb, pStart, colorData);
        }

        /// <summary>
        /// Метод обрабатывает MouseUp на холсте
        /// Возвращает isPressed в false        /// Задает финишную точку координат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
            pFinish = SetToCurPoint(e);
            //if (lines)
            //{
            //    isPressed = true;
            //    pStart = pFinish;
            //}
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
        /// Метод обрабатывает нажатие на кнопку очищения холста             /// /// Задает новый битмап
        /// Ставит битмап в sourse холста
        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;
        }

        /// <summary>
        /// Метод обрабатывающй нажатие на кнопки изменения торщины линии        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>
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
        /// <returns>Возвращает массив byte[] {alpha, red, green, blue}</returns>        private byte[] HexToRGBConverter(String s)
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
        //private void SetPixel(Point p, byte[] colorData)
        //{
        //    if (p.X > 0 && p.X < (int)MainImage.Width && p.Y > 0 && p.Y < (int)MainImage.Height)
        //    {
        //        Int32Rect rect = new Int32Rect((int)p.X, (int)p.Y, 1, 1);
        //        wb.WritePixels(rect, colorData, 4, 0);
        //        //   curState.WritePixels(rect, colorData, 4, 0);
        //        MainImage.Source = wb;
        //    }
        //}

        /// <summary>
        /// Метод выводит в 2 текстбокса координаты позиции мыши        /// </summary>
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

        /// <summary>        /// Метод выводит в 3 текстбокса текущий RGB цвет (отображает пользователю текущий цвет в формате RGB)
        /// </summary>
        /// <param name="colorData">byte[] {alpha, red, green, blue}</param>
        private void ShowCurColorRGB(byte[] colorData)
        {
           /* red = colorData[1];
            green = colorData[2];
            blue = colorData[3];*/
            rColor.Text = Convert.ToString(colorData[2]);
            gColor.Text = Convert.ToString(colorData[1]);
            bColor.Text = Convert.ToString(colorData[0]);
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
        //private void lineAngle(double angle)
        //{
        //    double z = 0; int i = 0;
        //    while (i < n + 1)
        //    {
        //        p[i].X = prevPoint.X + (int)(Math.Round(Math.Cos(z / 180 * Math.PI) * R));
        //        p[i].Y = prevPoint.Y - (int)(Math.Round(Math.Sin(z / 180 * Math.PI) * R));
        //        z = z + angle;
        //        i++;
        //    }
        //}
        ///// <summary>
        ///// Метод рисует n-угольник
        ///// </summary>
        ///// <param name="pStart"></param>
        ///// <param name="pFinish"></param>
        //private void Draw_Polygon(Point pStart, Point pFinish)
        //{
        //    wb = new WriteableBitmap(curState);
        //    n = Convert.ToInt32(sides.Text);
        //    R = Math.Sqrt(Math.Pow((pFinish.X - pStart.X), 2) + Math.Pow((pFinish.Y - pStart.Y), 2));
        //    p = new Point[n + 1];
        //    lineAngle((double)(360.0 / (double)n));
        //    int i = n;
        //    while (i > 0)
        //    {
        //        DrawLine(p[i], p[i - 1]);
        //        i = i - 1;
        //    }
        //}
        //public void DrawByLines(Point pStart, Point pFinish, MouseEventArgs e)
        //{
        //    wb = new WriteableBitmap(curState);
        //    Point temp = pStart;
        //    DrawLine(pStart, pFinish);
        //    if (e.RightButton == MouseButtonState.Pressed)
        //    {
        //        pFinish = temp;
        //    }
        //}
        /* ComboBox comboBox = (ComboBox)sender;
         ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
         MessageBox.Show(selectedItem.Content.ToString());*/
        //hgfjknbfm 
    }
}
