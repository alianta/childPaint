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
using System.Windows.Shapes;
using Paint.SurfaceStrategy;
using Line = System.Windows.Shapes.Line;
using System.Collections.Generic;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region переменные
        private Point prev = new Point(0, 0);
        private Point position = new Point(0, 0);
        private Brush currentBrush;
        private MyBitmap myBitmap;

        private IDrawer defaultDrawerRealization;
        private ISurfaceStrategy currentSurfaceStrategy;
        private ColoredFiguresStrategy defaultFillRealization;
        private FigureEnum flagFigure = FigureEnum.Pen;
        private Point prevPoint, pStart, pFinish, tmpPoint;
        private FigureCreator concreteCreator = null;
        private Figure concreteFigure = null;
        private SolidColorBrush stroke1 = Brushes.Black;
        private bool isPressed = false;
        private bool shiftPressed = false;
        private bool isBucket = false;
        private bool isDoubleClicked = false;
        private int numSides;
        private int clickCount = 0;
        private int vectorThickness;
        private Stack stackBack = new Stack();
        private System.Windows.Point A;
        private Stack stackForward = new Stack();

        bool canBeDragged = false;
        bool canDraw = true;
        List<List<Point>> listOfFigures = new List<List<Point>>();
        List<Point> fogureSpacePoints = new List<Point>();
        int[] figureMinMaxXY;
        bool pointIsInFigureSpace;
        #endregion

        public MainWindow()
        {
            //Scream();
            defaultDrawerRealization = new DrawByLine();
            currentBrush = new Brush();
            defaultFillRealization = new NoFill();
            currentSurfaceStrategy = new DrawOnBitmap();
            // Canvas c = new Canvas();

            InitializeComponent();

            MyCanvas.Instance = myCanvas;
            myBitmap = MyBitmap.GetBitmap();
            myBitmap.Btm = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
            stackBack.AddMyBitmap(myBitmap.Btm);
            MainImage.Source = myBitmap.Btm;


            FillBitmap();
        }

        //  ОБРАБОТКА СОБЫТИЙ

        #region btns
        /// <summary>
        /// Метод обрабатывающий кнопки фигур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFigure_Click(object sender, RoutedEventArgs e)
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
            else if (sender.Equals(btnRhombus))
            {
                flagFigure = FigureEnum.Parallelogram;
            }
            else
            {
                xPosition.Text = "Алярма!";
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
            currentBrush.BrushColor = new Color(buttonStr);
            stroke1 = new SolidColorBrush((System.Windows.Media.Color)ColorConverter.ConvertFromString(buttonStr));
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
            Type canvaType = currentSurfaceStrategy.GetType();

            if (canvaType.Name == "DrawOnCanvas")
            {
                myCanvas.Children.Clear();
            }
            else
            {
                FillBitmap();
                myBitmap.Btm = new WriteableBitmap((int)MainImage.Width, (int)MainImage.Height, 96, 96, PixelFormats.Bgra32, null);
                MainImage.Source = myBitmap.Btm;
            }
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
            switch (selectedFileType)
            {
                case (".jpg"):
                    dlg.FilterIndex = 2;
                    break;
                case (".png"):
                    dlg.FilterIndex = 1;
                    break;
                case (".bmp"):
                    dlg.FilterIndex = 3;
                    break;
            }

            Type str = currentSurfaceStrategy.GetType();
            if (str.Name == "DrawOnCanvas")
            {
                //заглушка для вектора (пока не ясно как сохранять в SVG)
                MessageBox.Show("This functionality will be in the next version of programm!");
            }
            else
            {
                Nullable<bool> result = dlg.ShowDialog();
                // Обработка результата работы диалога
                if (result == true)
                {


                    if (str.Name == "DrawOnCanvas")
                    {
                        FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                        RenderTargetBitmap bmp = new RenderTargetBitmap((int)myCanvas.ActualWidth,
                            (int)myCanvas.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                        bmp.Render(myCanvas);
                        BitmapEncoder encoder = new TiffBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bmp));
                        encoder.Save(fs);
                        fs.Close();
                    }
                    else if (str.Name == "DrawOnBitmap")
                    {
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)MainImage.Source));
                        using (FileStream stream = new FileStream(dlg.FileName, FileMode.Create))
                            encoder.Save(stream);
                    }

                }
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
                currentBrush.BrushThickness = new DefaultThickness();
                vectorThickness = 1;
            }
            else if (selectedItem.Equals(thick2))
            {
                currentBrush.BrushThickness = new MediumThickness();
                vectorThickness = 2;
            }
            else if (selectedItem.Equals(thick3))
            {
                currentBrush.BrushThickness = new BoldThickness();
                vectorThickness = 3;
            }
            else if (selectedItem.Equals(thick4))
            {
                currentBrush.BrushThickness = new ExtraboldThickness();
                vectorThickness = 4;
            }
        }


        /// <summary>
        /// Метод обрабатывает нажатие на кнопку заливки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BntFillBucket_Click(object sender, RoutedEventArgs e)
        {
            isBucket = true;
        }

        /// <summary>
        /// Метод обрабатывает нажатие на кнопки вперед / назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackForward_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btnBack))
            {
                if (stackBack.GetSize() > 1)
                {
                    stackForward.AddMyBitmap(stackBack.GetMyBitmap());
                }

                myBitmap.Btm = stackBack.GetMyBitmap();
                MainImage.Source = myBitmap.Btm;
                stackBack.AddMyBitmap(myBitmap.Btm);
            }
            if (sender.Equals(btnForward) && stackForward.GetSize() > 0)
            {
                myBitmap.Btm = stackForward.GetMyBitmap();
                MainImage.Source = myBitmap.Btm;
            }
        }

        #endregion

        #region bitmap

        /// <summary>
        /// Метод обрабатывает нажатие левой кнопки мыши на холсте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pStart = SetToCurPoint(e);
            isPressed = true;

            if (clickCount < 2 && flagFigure == FigureEnum.ClosingLines)
            {
                clickCount++;
            }

            else if (canBeDragged && !IsPointIsInFigureSpace(figureMinMaxXY, pStart))
            {
                canBeDragged = false;
                fogureSpacePoints = null;
                figureMinMaxXY = null;
                canDraw = true;
            }

            if (clickCount == 1 && flagFigure == FigureEnum.ClosingLines)
            {
                concreteCreator = new ClosingLinesCreator();
            }

            if (isBucket)
            {
                tmpPoint = pStart;
                Filling fill = new Filling(currentBrush.BrushColor);

                fill.PixelFill(pStart.X, pStart.Y);
                MainImage.Source = myBitmap.Btm;
            }
            else
            {
                prevPoint = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y);
                myBitmap.CreateCopy();
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


            if (isPressed)
            {
                MainImage.Source = myBitmap.Btm;
                if (canDraw)
                {
                    if (!isBucket)
                    {
                        SelectConcreteCreator(flagFigure);

                        if (concreteCreator == null)
                            return;

                        if (flagFigure == FigureEnum.ClosingLines && clickCount < 2)
                        {
                            concreteFigure = concreteCreator.CreateFigure(prevPoint, curPoint, isDoubleClicked);
                        }

                        concreteFigure = concreteCreator.CreateFigure(prevPoint, curPoint, shiftPressed);
                        InitFigure(concreteFigure);

                        if (flagFigure == FigureEnum.Pen || flagFigure == FigureEnum.Eraser)
                        {
                            prevPoint = curPoint;
                        }
                        else
                        {
                            myBitmap.SetBitmapToCopy();
                        }
                    }
                }
                else
                {
                    if (canBeDragged && pointIsInFigureSpace)
                    {
                        List<Point> newList = new List<Point>();
                        int[] distance = GetDistance(pStart, curPoint);
                        newList = SetNewFigurePoints(listOfFigures[listOfFigures.Count - 1], distance);
                        concreteFigure.Points = null;
                        concreteFigure.Points = newList;
                        myBitmap.SetBitmapToCopy();
                    }
                }
                concreteFigure.DoDraw();
            }
        }
        #endregion

        #region window
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

            if (flagFigure != FigureEnum.ClosingLines && flagFigure != FigureEnum.Pen)
            {
                canBeDragged = true;
                canDraw = false;
                listOfFigures.Add(concreteFigure.Points);
            }

            if (!isDoubleClicked)
            {
                stackBack.AddMyBitmap(myBitmap.Btm);

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
                MainImage.Source = myBitmap.Btm;

                if (isDoubleClicked)
                {
                    canBeDragged = true;
                    canDraw = false;
                    listOfFigures.Add(concreteFigure.Points);
                    isDoubleClicked = false;
                    isPressed = false;
                    clickCount = 0;
                }
            }

            if (flagFigure == FigureEnum.ClosingLines)
            {
                tmpPoint = pFinish;
            }

            if (canBeDragged)
            {
                figureMinMaxXY = GetMinMaxXY(concreteFigure.Points);
                fogureSpacePoints = GetfigureSpacePoints(figureMinMaxXY);
            }

        }

        /// <summary>
        /// Метод обрабатывает DoubleClick на холсте
        /// Возвращает isDoubleClick в false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Метод обрабатывает нажатие на кнопки F1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                InfoPage info = new InfoPage();
                info.ShowDialog();
            }
        }

        #endregion

        /// <summary>
        /// Выбор растр или вектор
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((TabItem)e.AddedItems[0]).Header)
            {
                case "BITMAP":
                    currentSurfaceStrategy = new DrawOnBitmap();
                    MainImage.Source = myBitmap.Btm;
                    break;
                case "VECTOR":
                    currentSurfaceStrategy = new DrawOnCanvas();
                    /*MyCanvas mc = new MyCanvas();
                    mc.Canv = myCanvas;*/
                    //mc.GetCanvas(myCanvas);
                    break;
            }
        }

        #region canvas

        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;
        UIElement source = null;
        List<Figure> listOfVectorFigures = new List<Figure>();
        // The points selected by the user.
        private List<System.Windows.Point> vectorPoints = new List<System.Windows.Point>();

        /// <summary>
        /// Метод обрабатывает наведение курсора на векторный холст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                //A = e.GetPosition(myCanvas);
                //prev = new Point((int)A.X, (int)A.Y);
            }
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tmp = e.GetPosition(sender as Canvas);
            position = new Point((int)tmp.X, (int)tmp.Y);
            prev = position;
        }

        private void MyCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (MyCanvas.CurrentFigure != null)
            {
                MyCanvas.ListVectorFigures.Add(MyCanvas.CurrentFigure);
                foreach (Line line in myCanvas.Children)
                {
                    line.MouseLeftButtonDown += Line_MouseDown;
                }
            }
            MyCanvas.CurrentFigure = null;
        }

        public void Line_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string bcolor = currentBrush.BrushColor.getHexColor();
            (sender as Line).Stroke = new BrushConverter().ConvertFromString(bcolor) as SolidColorBrush;


            /* foreach (VectorFigure figure in MyCanvas.ListVectorFigures)
             {
                 foreach (var line in figure.ListOfLines)
                 {
                     if (sender == line)
                     {
                         MyCanvas.CurrentFigure = figure;
                         break;
                     }
                 }
             }*/

            //foreach (var line in MyCanvas.CurrentFigure.ListOfLines)
            //{
            //    line.X1 += 20;
            //    line.X2 += 20;
            //    line.Y1 += 20;
            //    line.Y2 += 20;
            //}
        }


        /// <summary>
        /// Метод обрабатывает движение по векторному холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            ShowCurPoint(e, sender);

            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                var temp = e.GetPosition(sender as Canvas);
                // position = new Point((int)temp.X, (int)temp.Y);
                position = new Point((int)temp.X, (int)temp.Y);
                SelectConcreteCreator(flagFigure);
                concreteFigure = concreteCreator.CreateFigure(prev, position, false);
                VectorFigure vectorFigure = new VectorFigure(concreteFigure.Points);
                if (MyCanvas.CurrentFigure == null)
                {
                    MyCanvas.CurrentFigure = vectorFigure;
                }
                else
                {
                    MyCanvas.RemoveChildrenByTag();
                }
                InitFigure(concreteFigure);
                concreteFigure.DoDraw();
                if (MyCanvas.CurrentFigure == null)
                    MyCanvas.ListVectorFigures.Add(new VectorFigure(concreteFigure.Points));
            }
            else
            {
                // MyCanvas.CurrentFigure = null;
                MyCanvas_MouseUp(sender, e);
            }
        }
        #endregion


        //  ВНУТРЕННИЕ МЕТОДЫ

        #region ВНУТРЕННИЕ МЕТОДЫ
        /// <summary>
        /// Создание креейтера выбраной фигуры 
        /// </summary>
        /// <param name="flagFigure"></param>
        private void SelectConcreteCreator(FigureEnum flagFigure)
        {
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
                case FigureEnum.Parallelogram:
                    concreteCreator = new ParallelogramCreator();
                    break;
            }
        }

        /// <summary>
        /// Метод выводит в 2 текстбокса координаты позиции мыши
        /// </summary>
        /// <param name="e"></param>
        /// 
        private void ShowCurPoint(MouseEventArgs e, object sender = null)
        {
            Type str = currentSurfaceStrategy.GetType();

            if (str.Name == "DrawOnCanvas")
            {
                xPosition.Text = Convert.ToString(Convert.ToInt32(e.GetPosition(sender as Canvas).X));
                yPosition.Text = Convert.ToString(Convert.ToInt32(e.GetPosition(sender as Canvas).Y));
            }

            else
            {
                xPosition.Text = Convert.ToString(Convert.ToInt32(e.GetPosition(MainImage).X));
                yPosition.Text = Convert.ToString(Convert.ToInt32(e.GetPosition(MainImage).Y));
            }
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
        /// Метод инициализирует свойства, необходимые для рисования
        /// </summary>
        /// <param name="figure"></param>
        private void InitFigure(Figure figure)
        {
            figure.DrawerRealisation = defaultDrawerRealization;
            figure.DrawerRealisation.SurfaceStrategy = currentSurfaceStrategy;
            figure.DrawerRealisation.SurfaceStrategy.CurrentBrush = currentBrush;
        }

        /// <summary>
        /// Метод ограничивает ввод в текстовые поля всего, кроме цифр
        /// </summary>
        /// <param name="e"></param>
        private void Sides_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        /// <summary>
        /// Метод заливает белым цветом весь битмап
        /// </summary>
        private void FillBitmap()
        {
            for (int j = 0; j < (int)MainImage.Height; j++)
            {
                for (int i = 0; i < (int)MainImage.Width; i++)
                {
                    Pixel.Draw(new Point(i, j), new Color("FFFFFFFF").HexToRGBConverter());
                }
            }
        }

        /// <summary>
        /// Метод описывает звуковое сопровождение при разработке
        /// </summary>
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
            catch (Exception) { }

        }

        private int[] GetMinMaxXY(List<Point> figurePoints)
        {
            int minX = figurePoints[0].X;
            int maxX = figurePoints[0].X;
            int minY = figurePoints[0].Y;
            int maxY = figurePoints[0].Y;
            foreach (Point item in figurePoints)
            {
                if (item.X > maxX)
                {
                    maxX = item.X;
                }
                if (item.Y > maxY)
                {
                    maxY = item.Y;
                }

                if (item.X < minX)
                {
                    minX = item.X;
                }
                if (item.Y < minY)
                {
                    minY = item.Y;
                }
            }
            int[] arr = new int[] { minX, maxX, minY, maxY };
            return arr;
        }

        private List<Point> GetfigureSpacePoints(int[] minMaxXY)
        {
            List<Point> figureSpace = new List<Point>();
            figureSpace.Add(new Point(minMaxXY[0], minMaxXY[2]));
            figureSpace.Add(new Point(minMaxXY[0], minMaxXY[3]));
            figureSpace.Add(new Point(minMaxXY[1], minMaxXY[3]));
            figureSpace.Add(new Point(minMaxXY[1], minMaxXY[2]));

            return figureSpace;
        }


        private bool IsPointIsInFigureSpace(int[] minMaxXY, Point curPoint)
        {
            if (curPoint.X >= minMaxXY[0] && curPoint.X <= minMaxXY[1] && curPoint.Y >= minMaxXY[2] && curPoint.Y <= minMaxXY[3])
            {
                return pointIsInFigureSpace = true;
            }

            return pointIsInFigureSpace = false;
        }

        private int[] GetDistance(Point pStart, Point curPoint)
        {
            int[] arr = new int[2];
            arr[0] = curPoint.X - pStart.X;
            arr[1] = curPoint.Y - pStart.Y;

            return arr;
        }


        private Point GetNewPoint(Point point, int[] distance)
        {
            Point newP = new Point();
            newP.X = point.X + distance[0];
            newP.Y = point.Y + distance[1];
            return newP;
        }



        private List<Point> SetNewFigurePoints(List<Point> concreteFigurePoints, int[] distance)
        {
            List<Point> newList = new List<Point>();
            foreach (Point point in concreteFigurePoints)
            {
                Point p = GetNewPoint(point, distance);
                newList.Add(p);
            }
            return newList;
        }

        private System.Drawing.Point ConverterOfPointsToSD(System.Windows.Point p)
        {
            System.Drawing.Point temp = new System.Drawing.Point();
            temp.X = (int)p.X;
            temp.Y = (int)p.Y;
            return temp;
        }

        private System.Windows.Point ConverterOfPointsToSW(System.Drawing.Point p)
        {
            System.Windows.Point temp = new System.Windows.Point();
            temp.X = p.X;
            temp.Y = p.Y;
            return temp;
        }
        #endregion
    }
}

