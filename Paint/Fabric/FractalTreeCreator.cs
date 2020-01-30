using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint.Fabric
{
    class FractalTreeCreator : FigureCreator
    {

        private int numInerations;
        double angle = Math.PI / 2; //Угол поворота на 90 градусов
        public FractalTreeCreator(int numInerations)
        {
            this.numInerations = Convert.ToInt32(numInerations);
        }

        public override Figure CreateFigure(Point pStart, Point pFinish, bool shiftPressed)
        {
            return CreateTree(pStart, pFinish, numInerations, angle);
        }

        /// <summary>
        /// Метод рисующий Фрактал Дерево Пифагора 
        /// </summary>
        /// <param name="pStart">Стартовая точка</param>
        /// <param name="pFinish">Финишная точка</param>
        /// <param name="n">Параметр, который фиксирует количество итераций в рекурсии</param>
        /// <param name="angle">Угол поворота на каждой итерации</param>
        /// <returns></returns>
        public Figure CreateTree(Point pStart, Point pFinish, double numInerations, double angle)
        {
            List<Point> figurePoints = new List<Point>();
            double ang1 = Math.PI / 4;  //Угол поворота на 45 градусов
            double ang2 = Math.PI / 6;  //Угол поворота на 30 градусов

            double x = pStart.X;
            double y = pStart.Y;

            if (numInerations > 2)
            {
                numInerations *= 0.7; //Меняем параметр a
                                      //Считаем координаты для вершины-ребенка
                pFinish.X = (int)Math.Round(x + numInerations * Math.Cos(angle));
                pFinish.Y = (int)Math.Round(y - numInerations * Math.Sin(angle));
                //рисуем линию между вершинами          
                figurePoints.Add(pStart);
                figurePoints.Add(pFinish);
                CreateTree(pFinish, pFinish, numInerations, angle + ang1);
                CreateTree(pFinish, pFinish, numInerations, angle - ang2);
            }
            return new FractalTree(figurePoints);
            // для дерева надо что б в DrawByLine было  i < figurePoints.Count - 1 ------------ !!! =>
            //for (int i = 0; i < figurePoints.Count - 1; i++)
            //{
            //    line.Draw(wb, figurePoints[i], figurePoints[i + 1], false);
            //}
        }
    }
}
