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
        FigureEnum flagFigure = FigureEnum.Pen;
        string flagFigure1 = "Pen";
        byte[] colorData = { 0, 0, 0, 255 }; //все для создания цвета
        bool isPressed = false; //передает состаяние мыши
        Point prevPoint; //точка начала коордиат
        Point pStart;// Начальная точка
        Point pStaticStart;// Неизменная начальная точка для ломаной линии
        Point pFinish;// Конечная точка
        int numSides;//количество сторон
        bool shiftPressed = false;
        bool isBucket = false;
        bool isDoubleClicked = false;
        bool isFirstClicked = true;
        Pen pen;


        public MainWindow()
        {
            defaultDrawerRealization = new DrawByLine();
            defaultDrawerRealization.CurrentBrush = new Brush();
            currentBrush = defaultDrawerRealization.CurrentBrush;
            pen = new Pen();
            pen.CurrentBrush = new Brush();

            InitializeComponent();
            wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            myBitmap = MyBitmap.GetBitmap();
            myBitmap.btm = wb;
            MainImage.Source = myBitmap.btm;
            
            ShowCurColorRGB(colorData);

            ////defaultDrawerRealization.CurrentBrush = currentBrush;

            FillBitmap();
        }

        //  ОБРАБОТКА СОБЫТИЙ
        private void FillBitmap()
        {
            currentBrush = new Brush(currentBrush.BrushThickness, new Color("FFFFFFFF"));
            for (int j = 0; j < (int)MainImage.Height; j++)
            {
                for (int i = 0; i < (int)MainImage.Width; i++)
                {
                    Pixel.Draw(new Point (i, j), currentBrush.BrushColor.HexToRGBConverter());
                }
            }
        }
             

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
                flagFigure1 = "Pen";
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
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            shiftPressed = Keyboard.IsKeyDown(Key.LeftShift);
            ShowCurPoint(e);
            Point curPoint = SetToCurPoint(e);

            FigureCreator concreteCreator = null;

            if (isPressed)
            {
                MainImage.Source = wb;
                
                switch (flagFigure)
                {
                    //case FigureEnum.Pen:
                    //    //concreteCreator = new Pen();
                    //    Pen pen = new Pen();
                    //    pen.DrawLine(prevPoint, curPoint);
                    //    prevPoint = curPoint;
                    //    break;
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
                    case FigureEnum.ClosingLines:
                        concreteCreator = new StraightLineCreator();
                        break;
                }

                if (flagFigure1 == "Pen")
                {
                    pen.CurrentBrush = new Brush();
                    pen.DrawLine(prevPoint, curPoint);
                    prevPoint = curPoint;
                }

                if (concreteCreator == null)
                    return;
                currentBrush.BrushColor =new Color("#FFFF0000");
                Figure concreteFigure = concreteCreator.CreateFigure(prevPoint, curPoint, shiftPressed);
                concreteFigure.DrawerRealisation = defaultDrawerRealization;
                myBitmap.SetBitmapToCopy();
                concreteFigure.DoDraw();
                MainImage.Source = myBitmap.btm;
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
            defaultDrawerRealization.CurrentBrush.BrushColor= new Color(buttonStr);
            pen.CurrentBrush.BrushColor = new Color(buttonStr);
            colorData = HexToRGBConverter(buttonStr);
            ShowCurColorRGB(colorData);
        }

        /// <summary>
        /// Метод обрабатывает нажатие на кнопку ластика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntEraser_Click(object sender, RoutedEventArgs e)
        {
            colorData = new byte[] { 255, 255, 255, 255 };
            flagFigure = FigureEnum.Pen;
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
            //isPressed = true;
            //pStart = SetToCurPoint(e);
            //pStaticStart = pStart;
            pStart = SetToCurPoint(e);
            isPressed = true;
            if (isFirstClicked)
            {
                pStaticStart = pStart;
                isFirstClicked = false;
            }
            if (isBucket)
            {
                tmpPoint = pStart;
                FillFigure(wb, colorData, pStart);
                MainImage.Source = myBitmap.btm;
            }
            else
            {
                //  isPressed = true;
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

            if (!isDoubleClicked && flagFigure == FigureEnum.ClosingLines)
            {
                isPressed = true;
                pStart = pFinish;
            }
            if (isDoubleClicked && flagFigure == FigureEnum.ClosingLines)
            {
                FigureCreator concreteCreator = new StraightLineCreator();
                Figure concreteFigure = concreteCreator.CreateFigure(pFinish, pStaticStart, shiftPressed);
                concreteFigure.DrawerRealisation = defaultDrawerRealization;
                myBitmap.SetBitmapToCopy();
                concreteFigure.DoDraw();
                MainImage.Source = myBitmap.btm;
                isDoubleClicked = false;
                isFirstClicked = true;
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
            //wb = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            myBitmap = MyBitmap.GetBitmap();
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
                pen.CurrentBrush.BrushThickness = new DefaultThickness();
            }
            else if (selectedItem.Equals(thick2))
            {
                defaultDrawerRealization.CurrentBrush.BrushThickness = new MediumThickness();
                pen.CurrentBrush.BrushThickness = new MediumThickness();
            }
            else if (selectedItem.Equals(thick3))
            {
                defaultDrawerRealization.CurrentBrush.BrushThickness = new BoldThickness();
                pen.CurrentBrush.BrushThickness = new BoldThickness();
            }
            else if (selectedItem.Equals(thick4))
            {
                defaultDrawerRealization.CurrentBrush.BrushThickness = new ExtraboldThickness();
                pen.CurrentBrush.BrushThickness = new ExtraboldThickness();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainImage_MouseEnter(object sender, MouseButtonEventArgs e)
        {

        }
       

        //---------------------------------------------
        private byte[] GetPixelColorData(WriteableBitmap wb, Point currentPoint)//возвращает цвет пикселя на битмапе
        {
            int bytePerPixel = 4;//?????????????????????????????????????????
            int stride = 4 * Convert.ToInt32(wb.Width);
            byte[] pixels = new byte[wb.PixelWidth * wb.PixelHeight * 4];
            wb.CopyPixels(pixels, stride, 0);
            int currentPixel = (int)currentPoint.X * bytePerPixel + (stride * (int)currentPoint.Y);
            byte[] color = new byte[] { pixels[currentPixel], pixels[currentPixel + 1], pixels[currentPixel + 2], 0 };

            return color;
        }

        private bool IsColorsEqual(byte[] colorData1, byte[] colorData2)
        {

            if (colorData1[0] == colorData2[0] && colorData1[1] == colorData2[1] && colorData1[2] == colorData2[2] && colorData1[3] == colorData2[3])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FillFigure(WriteableBitmap wb, byte[] colorData, Point startPoint)//битмап, цветзаливки, кудаткнули
        {
            int bytePerPixel = 4;//?????????????????????????????????????????
            int stride = bytePerPixel * Convert.ToInt32(wb.Width);
            //  int stride = 4;//???????????????????????????????????????????
            pixelsCopy = new byte[wb.PixelWidth * wb.PixelHeight * bytePerPixel];
            wb.CopyPixels(pixelsCopy, stride, 0);
            byte[] colorStart = GetPixelColorData(wb, startPoint);
                FillFigureStep(wb, colorData, colorStart, startPoint);
            //try
            //{
            //    myBitmap.btm.Lock();
            //}
            //finally
            //{
            //    myBitmap.btm.Unlock();
            //}
        }

        private void FillFigureStep(WriteableBitmap wb, byte[] colorData, byte[] startColorData, Point currentPoint)//битмап, цветзаливки, цветстартовогопикселя, кудаткнули
        {
            Pixel pixel = new Pixel();
            Point tmpPoint = currentPoint;

            while (IsColorsEqual(GetPixelColorData(myBitmap.btm, tmpPoint), startColorData) && tmpPoint.X > 0)
            {
                Pixel.Draw(tmpPoint, colorData);
                tmpPoint.X--;
            }
            if (!IsColorsEqual(GetPixelColorData(myBitmap.btm, tmpPoint), startColorData))
            {
                tmpPoint.X++;
            }

            Point left = tmpPoint;
            tmpPoint.X = currentPoint.X + 1;

            while (IsColorsEqual(GetPixelColorData(myBitmap.btm, tmpPoint), startColorData) && tmpPoint.X < wb.Width - 1)
            {
                Pixel.Draw(tmpPoint, colorData);
                tmpPoint.X++;
            }
            if (!IsColorsEqual(GetPixelColorData(myBitmap.btm, tmpPoint), startColorData))
            {
                tmpPoint.X--;
            }
            else
            {
                Pixel.Draw(tmpPoint, colorData);
            }
            Point right = tmpPoint;


            for (int i = (int)left.X; i <= (int)right.X; i++)
            {
                Point point1 = new Point(i, currentPoint.Y + 1);

                if ((IsColorsEqual(GetPixelColorData(myBitmap.btm, point1), startColorData) && point1.Y < wb.Height - 1))
                {
                    FillFigureStep(myBitmap.btm, colorData, startColorData, point1);

                }

                //if ((IsColorsEqual(GetPixelColorData(wb, point2), startColorData) && point2.Y < wb.Height - 1))
                //{
                //    FillFigureStep(wb, colorData, startColorData, point2);
                //}
            }

            for (int i = (int)left.X; i <= (int)right.X; i++)
            {
                //Point point1 = new Point(i, currentPoint.Y + 1);
                Point point2 = new Point(i, currentPoint.Y - 1);

                //if ((IsColorsEqual(GetPixelColorData(wb, point1), startColorData) && point1.Y > 0))
                //{
                //    FillFigureStep(wb, colorData, startColorData, point1);

                //}

                if ((IsColorsEqual(GetPixelColorData(myBitmap.btm, point2), startColorData) && point2.Y > 0))
                {
                    FillFigureStep(myBitmap.btm, colorData, startColorData, point2);
                }
            }
            isPressed = false;
        }

        private void bntFillBucket_Click(object sender, RoutedEventArgs e)
        {
            isBucket = true;

        }

    }
}
