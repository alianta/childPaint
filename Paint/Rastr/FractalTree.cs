using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Paint.Rastr
{
    class FractalTree : Figure
    {
        private byte[] colorData;
        private Figure line;
        double angle = Math.PI / 2; //Угол поворота на 90 градусов
        double ang1 = Math.PI / 4;  //Угол поворота на 45 градусов
        double ang2 = Math.PI / 6;  //Угол поворота на 30 градусов

        double iter = 100;
        int thickness;

        //public FractalTree(byte[] colorData, int thickness, int n)
        //{
        //    this.thickness = thickness;
        //    this.colorData = colorData;
        //    line = new Line(colorData, thickness);
        //    iter = n;
        //}
        public FractalTree()
        { }

        public FractalTree(List<Point> figurePoints) : base(figurePoints)
        { }
        public override void Draw(WriteableBitmap wb, Point pStart, Point pFinish, bool shift)
        {
            
            DrawTree(wb, pStart, pFinish, shift, iter, angle);

        }     


        /// <summary>
        /// Метод рисующий Фрактал Дерево Пифагора 
        /// </summary>
        /// <param name="pStart">Стартовая точка</param>
        /// <param name="pFinish">Финишная точка</param>
        /// <param name="n">Параметр, который фиксирует количество итераций в рекурсии</param>
        /// <param name="angle">Угол поворота на каждой итерации</param>
        /// <returns></returns>
        public int DrawTree(WriteableBitmap wb, Point pStart, Point pFinish, bool shift, double n, double angle)
        {
            double x = pStart.X;
            double y = pStart.Y;

            if (n > 2)
            {
                n *= 0.7; //Меняем параметр a
                //Считаем координаты для вершины-ребенка
                pFinish.X = Math.Round(x + n * Math.Cos(angle));
                pFinish.Y = Math.Round(y - n * Math.Sin(angle));
                //рисуем линию между вершинами          
                line.Draw(wb, pStart, pFinish, shift);
                DrawTree(wb, pFinish, pFinish, shift, n, angle + ang1);
                DrawTree(wb, pFinish, pFinish, shift, n, angle - ang2);
            }
            return 0;
        }
    }
}
