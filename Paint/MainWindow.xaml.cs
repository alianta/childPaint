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
        byte[] colorData = { 0, 0, 0, 255 }; //все для создания цвета
        bool isPressed = false; //передает состаяние мыши
        Point prevPoint; //точка начала коордиат
        int thicknessLine = 1;//толщина линии
        Point pStart;// Начальная точка
        Point pFinish;// Конечная точка
        Point temp;
        Rastr.Figure figure;
        int n;//количество сторон

        bool shift = false;
        bool line = true;
        bool rectangle = false;
        bool circle = false;
        bool triangle = false;
        bool polygon = false;
        bool tree = false;
        bool lines = false;
        //double R;//расстояние от центра до стороны
        //Point[] p; //массив точек будущего многоугольника

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
            tree = false;
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
                MainImage.Source = wb;

                if (circle || triangle || polygon || tree || rectangle)
                {
                    wb = new WriteableBitmap(curState);
                }
                 if (circle ||  polygon || tree || rectangle)
                {
                    figure.Draw(wb, pStart, curPoint, shift);
                }

                if (line)
                {
                    figure.Draw(wb, prevPoint, curPoint, thicknessLine, false);
                    figure = new Line(colorData, thicknessLine);
                    prevPoint = curPoint;
                }
                if (circle)
                {
                    figure = new Ellipce(colorData, thicknessLine);
                }
                if (triangle)
                {
                    figure.Draw(wb, pStart, curPoint, thicknessLine, shift);
                    figure = new Triangle(colorData, thicknessLine);
                }
                if (rectangle)
                {
                    figure = new Rectangle(colorData, thicknessLine);
                }
                if (polygon)
                {
                    //if (sides.Text != "")
                    //{
                    //    n = Convert.ToInt32(sides.Text);
                    //}
                    //else
                    //{
                    //    n = 5;
                    //}
                    n = Convert.ToInt32(sides.Text);
                    figure = new Poligon(colorData, thicknessLine, n);
                }
                if (tree)
                {
                    //if (sides.Text != "")
                    //{
                    //    n = Convert.ToInt32(sides.Text);
                    //}
                    //else
                    //{
                    //    n = 100;
                    //}
                    n = Convert.ToInt32(sides.Text);
                    figure = new FractalTree(colorData, thicknessLine, n);
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
        /// Метод обрабатывает нажатие левой кнопки мыши на холсте
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
            //figure.Draw(wb, pStart, colorData);
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
        /// Метод обрабатывает нажатие на кнопку очищения холста     
        /// /// Задает новый битмап
        /// Ставит битмап в sourse холста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            MainImage.Source = wb;
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
        /// Метод выводит в 2 текстбокса координаты позиции мыши
        /// </summary>
        /// <param name="e"></param>
        /// 
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
        /// Метод выводит в 3 текстбокса текущий RGB цвет (отображает пользователю текущий цвет в формате RGB)
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
        /// Метод обрабатывает кнопку изменения толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Change_Thickness(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)ThicknessList.SelectedValue;
            if (selectedItem.Equals(thick1))
            {
                thicknessLine = 1;
            }
            else if (selectedItem.Equals(thick2))
            {
                thicknessLine = 2;
            }
            else if (selectedItem.Equals(thick3))
            {
                thicknessLine = 3;
            }

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /* ComboBox comboBox = (ComboBox)sender;
ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
MessageBox.Show(selectedItem.Content.ToString());*/
        //hgfjknbfm 
    }
}
