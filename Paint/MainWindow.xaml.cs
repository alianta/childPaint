using Paint.Fabric;
using Paint.Rastr;
using Paint.Thickness;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;
using System.Media;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WriteableBitmap wb; //создает новый холст Image для рисования 
        byte[] pixelsCopy = new byte[] { };
        Brush currentBrush;
        MyBitmap myBitmap;
        IDrawer defaultDrawerRealization;
        ColoredFiguresStrategy defaultFillRealization;
        FigureEnum flagFigure = FigureEnum.Pen;
        byte[] colorData = { 0, 0, 0, 255 }; //все для создания цвета
        bool isPressed = false; //передает состаяние мыши
        Point prevPoint; //точка начала коордиат
        Point pStart;// Начальная точка
        Point pFinish;// Конечная точка
        int numSides;//количество сторон
        bool shiftPressed = false;
        bool isBucket = false;
        bool isDoubleClicked = false;
        int clickCount = 0;
        Fill fill = new Fill();
        Stack stackBack = new Stack();
        Stack stackForward = new Stack();

        public MainWindow()
        {
            Scream();
            defaultDrawerRealization = new DrawByLine();
            defaultDrawerRealization.CurrentBrush = new Brush();
            currentBrush = defaultDrawerRealization.CurrentBrush;
            defaultFillRealization = new NoFill();
            InitializeComponent();
            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            myBitmap = MyBitmap.GetBitmap();
            myBitmap.btm = wb;
            stackBack.AddMyBitmap(myBitmap.btm);
            MainImage.Source = myBitmap.btm;

            ShowCurColorRGB(colorData);
            FillBitmap();

        }

        private void Scream()
        {
            SoundPlayer player = new SoundPlayer();
            string path = Directory.GetCurrentDirectory();
            player.SoundLocation = path + "\\white\\female_scream.wav";

            try
            {
                player.Load();
                player.Play();
            }
            catch (Exception E) { }
        }

        private void FillBitmap()
        {
            currentBrush = new Brush(currentBrush.BrushThickness, new Color("FFFFFFFF"));
            for (int j = 0; j < (int)MainImage.Height; j++)
            {
                for (int i = 0; i < (int)MainImage.Width; i++)
                {
                    Pixel.Draw(new Point(i, j), currentBrush.BrushColor.HexToRGBConverter());
                }
            }
        }

        //  ОБРАБОТКА СОБЫТИЙ


        /// <summary>
        /// Метод обрабатывающий кнопки фигур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFigure_Click(object sender, RoutedEventArgs e)
        {
            isBucket = false;

            if (sender.Equals(btnLine))
            {
                flagFigure = FigureEnum.Pen;
            }
            else if (sender.Equals(btnRectangle))
            {
                flagFigure = FigureEnum.Rectangle;
            }
            else if (sender.Equals(btnCircle))
            {
                flagFigure = FigureEnum.Circle;
            }
            else if (sender.Equals(btnTriangle))
            {
                flagFigure = FigureEnum.Triangle;
            }
            else if (sender.Equals(btnPolygon))
            {
                flagFigure = FigureEnum.Polygon;
            }
            else if (sender.Equals(btnTree))
            {
                flagFigure = FigureEnum.Tree;
            }
            else if (sender.Equals(btnClosingLines))
            {
                flagFigure = FigureEnum.ClosingLines;
            }
            else if (sender.Equals(btnStraightLine))
            {
                flagFigure = FigureEnum.StraightLine;
            }
            else if (sender.Equals(btnEraser))
            {
                flagFigure = FigureEnum.Eraser;
            }
            else
            {
                xPosition.Text = "Алярма!";
            }
        }

        FigureCreator concreteCreator = null;
        Figure concreteFigure = null;

        /// <summary>
        /// Метод обрабатывает двидение мыши по холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            shiftPressed = Keyboard.IsKeyDown(Key.LeftShift);
            ShowCurPoint(e);
            Point curPoint = SetToCurPoint(e);

            if (isPressed)
            {
                MainImage.Source = myBitmap.btm;

                switch (flagFigure)
                {
                    case FigureEnum.Pen:
                        concreteCreator = new PenCreator();
                        break;
                    case FigureEnum.Rectangle:
                        concreteCreator = new RectangleCreator();
                        break;
                    case FigureEnum.Circle:
                        concreteCreator = new EllipceCreator();
                        break;
                    case FigureEnum.Triangle:
                        concreteCreator = new TriangleCreator();
                        break;
                    case FigureEnum.Polygon:
                        numSides = Convert.ToInt32(sides.Text);
                        concreteCreator = new PolygonCreator(numSides);
                        break;
                    case FigureEnum.Tree:
                        numSides = Convert.ToInt32(sides.Text);
                        concreteCreator = new FractalTreeCreator(numSides);
                        break;
                    case FigureEnum.StraightLine:
                        concreteCreator = new StraightLineCreator();
                        break;
                    case FigureEnum.Eraser:
                        concreteCreator = new EraserCreator();
                        break;
                    case FigureEnum.ClosingLines:
                        break;
                }

                if (concreteCreator == null)
                    return;

                if (flagFigure == FigureEnum.ClosingLines && clickCount < 2)
                {
                    concreteFigure = concreteCreator.CreateFigure(prevPoint, curPoint, isDoubleClicked);
                }

                concreteFigure = concreteCreator.CreateFigure(prevPoint, curPoint, shiftPressed);
                concreteFigure.DrawerRealisation = defaultDrawerRealization;

                if (flagFigure == FigureEnum.Pen || flagFigure == FigureEnum.Eraser)
                {
                    prevPoint = curPoint;
                }
                else
                {
                    myBitmap.SetBitmapToCopy();
                    
                }

                concreteFigure.DoDraw();
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
            defaultDrawerRealization.CurrentBrush.BrushColor = new Color(buttonStr);
            colorData = HexToRGBConverter(buttonStr);
            ShowCurColorRGB(colorData);
        }

        Point tmpPoint;
        /// <summary>
        /// Метод обрабатывает нажатие левой кнопки мыши на холсте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (clickCount < 2 && flagFigure == FigureEnum.ClosingLines)
            {
                clickCount++;
            }

            pStart = SetToCurPoint(e);
            isPressed = true;

            if (clickCount == 1 && flagFigure == FigureEnum.ClosingLines)
            {
                concreteCreator = new ClosingLinesCreator();
            }

            if (isBucket)
            {
                tmpPoint = pStart;
                fill.FillFigure(colorData, pStart);
                MainImage.Source = myBitmap.btm;
            }
            else
            {
                prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
                myBitmap.CreateCopy();
            }
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
            

            if (!isDoubleClicked)
            {
                stackBack.AddMyBitmap(myBitmap.btm);

                if (flagFigure == FigureEnum.ClosingLines)
                {
                    isPressed = true;
                }
                
            }
            if (clickCount > 1 && flagFigure == FigureEnum.ClosingLines)
            {
                concreteFigure = concreteCreator.CreateFigure(tmpPoint, pFinish, isDoubleClicked);
                concreteFigure.DrawerRealisation = defaultDrawerRealization;
                concreteFigure.DoDraw();
                MainImage.Source = myBitmap.btm;

                if (isDoubleClicked)
                {
                    isDoubleClicked = false;
                    isPressed = false;
                    clickCount = 0;
                }
            }

            if (flagFigure == FigureEnum.ClosingLines)
            {
                tmpPoint = pFinish;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            isDoubleClicked = true;
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
            FillBitmap();
            myBitmap.btm = wb;
            MainImage.Source = myBitmap.btm;
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
            xPosition.Text = Convert.ToString(Convert.ToInt32(e.GetPosition(MainImage).X));
            yPosition.Text = Convert.ToString(Convert.ToInt32(e.GetPosition(MainImage).Y));
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
            rColor.Text = Convert.ToString(colorData[2]);
            gColor.Text = Convert.ToString(colorData[1]);
            bColor.Text = Convert.ToString(colorData[0]);
        }

        /// <summary>
        /// Метод обрабатывает кнопку сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            ComboBoxItem selectedItem = (ComboBoxItem)fileTypesList.SelectedValue;
            StackPanel selectedStackPanel = (StackPanel)selectedItem.Content;
            UIElementCollection UIElCollection = selectedStackPanel.Children;
            TextBlock tb = (TextBlock)UIElCollection[0];
            string selectedFileType = Convert.ToString(tb.Text);

            // Настраиваем параметры диалога
            dlg.FileName = "Document"; // Имя по-умолчанию
            dlg.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|BMP Image (*.bmp)|*.bmp|All files (*.*)|*.*";// Фильтр по расширениям
            if (selectedFileType == ".jpg")
            {
                dlg.FilterIndex = 2;

            }
            if (selectedFileType == ".png")
            {
                dlg.FilterIndex = 1;

            }
            if (selectedFileType == ".bmp")
            {
                dlg.FilterIndex = 3;

            }

            Nullable<bool> result = dlg.ShowDialog();
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
                defaultDrawerRealization.CurrentBrush.BrushThickness = new DefaultThickness();
            }
            else if (selectedItem.Equals(thick2))
            {
                defaultDrawerRealization.CurrentBrush.BrushThickness = new MediumThickness();
            }
            else if (selectedItem.Equals(thick3))
            {
                defaultDrawerRealization.CurrentBrush.BrushThickness = new BoldThickness();
            }
            else if (selectedItem.Equals(thick4))
            {
                defaultDrawerRealization.CurrentBrush.BrushThickness = new ExtraboldThickness();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bntFillBucket_Click(object sender, RoutedEventArgs e)
        {
            isBucket = true;
        }

        private void BtnBackForward_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender.Equals(btnBack))
            {
                if (stackBack.GetSize() > 1)
                {
                    stackForward.AddMyBitmap(stackBack.GetMyBitmap());
                }

                myBitmap.btm = stackBack.GetMyBitmap();
                MainImage.Source = myBitmap.btm;
                stackBack.AddMyBitmap(myBitmap.btm);
            }
            if (sender.Equals(btnForward) && stackForward.GetSize() > 0)
            {
                myBitmap.btm = stackForward.GetMyBitmap();
                MainImage.Source = myBitmap.btm;
            }
        }
        private void clrdFigure_Click(object sender, RoutedEventArgs e)
        {
            defaultFillRealization = new SolidFill();
        }
    }
}
