using Paint.Rastr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint
{
    public class MyCanvas
    {
        //private static List<Canvas> copies;
        private static Canvas instance;
        private static Canvas instanceCopy;

        public static VectorFigure CurrentFigure;
        private static List<VectorFigure> listVectorFigures;

        public static Canvas Instance
        {
            get => instance;
            set
            {
                instance = value;
                instanceCopy = instance;
            }
        }

        public static List<VectorFigure> ListVectorFigures
        {
            get
            {
                if (listVectorFigures == null)
                {
                    listVectorFigures = new List<VectorFigure>();
                }

                return listVectorFigures;
            }
            set => listVectorFigures = value;
        }

        /*public static List<Canvas> Copies
        {
            get
            {
                if (copies == null)
                {
                    copies = new List<Canvas>();
                }
                return copies;
            }
            set => copies = value;
        }*/

        public static void CopyInstance()
        {
            if (instance != null)
                instanceCopy = instance;
        }

        public static Canvas GetInstanceCopy()
        {
            if (instanceCopy == null && instance != null)
                instanceCopy = instance;
            return instanceCopy;
        }

        public static void AddLine(Line l)
        {
            instance.Children.Add(l);
        }

        public static void RemoveChildrenByTag()
        {
            for (int i = 0; i < instance.Children.Count; i++)
            {
                FrameworkElement child = (FrameworkElement)instance.Children[i];
                if (child.Tag == CurrentFigure)
                    instance.Children.Remove(child);
            }
        }


        /*
        /// <summary>
        /// Основной объект, в котором будет храниться уникальный экземпляр класса. 
        /// </summary>
        private static MyCanvas _instance;


        /// <summary>
        /// Данные, хранимые в классе.
        /// </summary>
        public Canvas Canv { get; set; }
        public Canvas CanvCopy { get; set; }

        /// <summary>
        /// Защищенный конструктор для инициализации единственного экземпляра класса.
        /// </summary>
        /// <param name="wb">Данные, используемые в классе.</param>
        public MyCanvas()
        {
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <param name="wb">Инициализирующие данные класса.</param>
        /// <returns>Уникальный экземпляр класса.</returns>
        public static MyCanvas GetCanvas(Canvas setCanv)
        {
            if (_instance == null)
            {
                _instance = new MyCanvas();
                _instance.Canv = setCanv;
            }
            return _instance;
        }
        public static MyCanvas GetCanvas2()
        {
            if (_instance == null)
            {
                _instance = new MyCanvas();
               // _instance.Canv = setCanv;
            }
            return _instance;
        }
        /// <summary>
        /// Сохранили текщий в копию.
        /// </summary>
       /* public void CreateCopy()
        {
            CanvCopy = new Canvas();
            Canv
        }*/

        /// <summary>
        /// Поменяли местами.
        /// </summary>
      /*  public void SetBitmapToCopy()
        {
            _instance.Canv = new MyCanvas(_instance.CanvCopy);
        }*/
        
    }
}
