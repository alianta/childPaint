using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint
{
    public class MyCanvas
    {
        private static List<Canvas> copies;
        private static Canvas instance;
        private static Canvas instanceCopy;

        public static Canvas Instance
        {
            get => instance;
            set
            {
                instance = value;
                instanceCopy = instance;
            }
        }

        public static List<Canvas> Copies
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
        }

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
    }
}
