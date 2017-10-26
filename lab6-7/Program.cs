using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace lab6
{
    enum State 
    { FIGURES = 1, ELEMENTS };
    struct Owner    
    {
        public static readonly string Name = "Vladislav";
        public static long Id; 
    }
    public class FigureException : Exception
    {
        public FigureException(string message) : base(message)
        { }
    }
    public class CircleException : FigureException
    {
        public CircleException(string message) : base(message)
        { }
    }
    public class RectangleException : FigureException
    {
        public RectangleException(string message) : base(message)
        { }
    }
    public class ManageElementsException : Exception
    {
        public ManageElementsException(string message) : base(message)
        { }
    }
    public class CheckBoxException : ManageElementsException
    {
        public CheckBoxException(string message) : base(message)
        { }
    }
    public class ButtonException : ManageElementsException
    {
        public ButtonException(string message) : base(message)
        { }
    }
    public class RadioButtonException : ManageElementsException
    {
        public RadioButtonException(string message) : base(message)
        { }
    }
    interface IFigure
    {
        void Show();
        void Input();
        void Resize();
    }
    public abstract class Figure
    {
        public abstract double Square();
        public abstract double Perimeter();
    }
    public class Circle : Figure, IFigure
    {
        public double radius;
        public const double pi = 3.14d;
        public Circle()
        {
            Input();
        }
        static Circle()
        {
            Owner.Id = DateTime.Now.Ticks;
        }
        public Circle(double radius)
        {
            this.radius = radius;
            if (radius < 0)
            {
                throw new CircleException("Radius cannot be less then zero.");
            }
        }
        public override double Square()
        {
            return pi * Math.Pow(radius, 2);
        }
        public override double Perimeter()
        {
            return 2 * pi * radius;
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
        public void Input()
        {
            Console.Write("Input radius: ");
            radius = Convert.ToDouble(Console.ReadLine());
        }
        public void Resize()
        {
            Console.Write("Resize: ");
            this.radius = Convert.ToDouble(Console.ReadLine());
        }
        public void Show()
        {
            Console.WriteLine("\nCircle\nRadius: {0}\nPerimeter: {1}\nSquare: {2}\n", radius, Perimeter(), Square());
            Console.WriteLine("Owner's name: {0}\nID: {1}\n", Owner.Name, Owner.Id);
        }
    }
    public class Rectangle : Figure, IFigure 
    {
        public double width;
        public double height;
        public Rectangle()
        {
            Input();
        }
        static Rectangle()
        {
            Owner.Id = DateTime.Now.Ticks;
        }
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
            if (width < 0 || height < 0)
            {
                throw new RectangleException("Width or height cannot be less then zero.");
            }
        }
        public override double Square()
        {
            return height * width;
        }
        public override double Perimeter()
        {
            return (height * 2) + (width * 2);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
        public void Input()
        {
            Console.Write("Input height: ");
            height = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input width: ");
            width = Convert.ToDouble(Console.ReadLine());
        }
        public void Resize()
        {
            Console.Write("Resize height: ");
            this.height = Convert.ToDouble(Console.ReadLine());
            Console.Write("Resize width: ");
            this.width = Convert.ToDouble(Console.ReadLine());
        }
        public void Show()
        {
            Console.WriteLine("\nRectangle\nWidth: {0}\nHeight: {1}\nPerimeter: {2}\nSquare: {3}\n", width, height, Perimeter(), Square());
            Console.WriteLine("Owner's name: {0}\nID: {1}\n", Owner.Name, Owner.Id);
        }
    }
    public interface IElements
    {
        void Show();
    }
    public abstract class ManageElements
    {
        public int count;
        public string title;
    }
    public class CheckBox : ManageElements, IElements
    {
        static CheckBox()
        {
            Owner.Id = DateTime.Now.Ticks;
        }
        public CheckBox(int count, string title)
        {
            this.count = count;
            this.title = title;
            if (title == " ")
            {
                throw new CheckBoxException("Title cannot be empty");
            }
        }
        public void Show()
        {
            Console.WriteLine("\nCheckboxes\nCount: {0}\nTitle: {1}\n", count, title);
            Console.WriteLine("Owner's name: {0}\nID: {1}\n", Owner.Name, Owner.Id);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
    }
    public class RadioButton : ManageElements, IElements
    {
        static RadioButton()
        {
            Owner.Id = DateTime.Now.Ticks;
        }
        public RadioButton(int count, string title)
        {
            this.count = count;
            this.title = title;
            if (title == " ")
            {
                throw new RadioButtonException("Title cannot be empty");
            }
        }
        public void Show()
        {
            Console.WriteLine("\nRadioButtons\nCount: {0}\nTitle: {1}\n", count, title);
            Console.WriteLine("Owner's name: {0}\nID: {1}\n", Owner.Name, Owner.Id);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
    }
    public class Button : ManageElements, IElements
    {
        static Button()
        {
            Owner.Id = DateTime.Now.Ticks;
        }
        public Button(int count, string title)
        {
            this.count = count;
            this.title = title;
            if (title == " ")
            {
                throw new ButtonException("Title cannot be empty");
            }
        }

        public void Show()
        {
            Console.WriteLine("\nButtons\nCount: {0}\nTitle: {1}\n", count, title);
            Console.WriteLine("Owner's name: {0}\nID: {1}\n", Owner.Name, Owner.Id);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
    }
    public class UI<T>
    {
        private List<T> container;
        public UI()
        { 
            container = new List<T>();
        }   
        public T this [int number]
        {
            get { return container[number]; }
            set { container[number] = value; }
        }
        public void Push(T element)
        {
            container.Add(element);
        }
        public void Pop()
        {
            container.RemoveAt(container.Count - 1);
        }
        public int Size
        {
            get { return container.Count; }
        }
        public void Output()
        {
            Console.WriteLine("\nContainer: ");
            for (int i = 0; i < container.Count; i++)
            {
                Console.Write(container[i]);
            }
            Console.WriteLine();
        }
    }
    public class UIController
    {
        public double TotalSquare = 0;
        public int TotalElements = 0;
        private UI<Figure> UIF;
        private UI<ManageElements> UIE;
        public UIController(UI<Figure> ui)
        {
            this.UIF = ui;
        }
        public UIController(UI<ManageElements> ui)
        {
            this.UIE = ui;
        }
        public void GetButtonList()
        {
            Console.WriteLine("Button list");
            for (int i = 0; i < UIE.Size; i++)
            {
                Console.WriteLine(UIE[i].title);
            }
        }
        public int GetCountUiElements()
        {
            for (int i = 0; i < UIE.Size; i++)
            {
                TotalElements += UIE[i].count;
            }
            return TotalElements;
        }
        public double GetTotalSquare() 
        {
            for (int i = 0; i < UIF.Size; i++)
            {
                TotalSquare += UIF[i].Square();
            }
            return TotalSquare;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool isError = false;
            try
            {
                IFigure circle = new Circle(-24);
            }
            catch (CircleException exception)
            {
                Console.WriteLine("Error: " + exception.Message);
                Console.WriteLine("Method: " + exception.TargetSite);
                Console.WriteLine("Source: " + exception.Source);
            }
            try
            {   
                IFigure rectangle = new Rectangle(-27, -13);
            }
            catch (RectangleException exception)
            {
                Console.WriteLine("Error: " + exception.Message);
                Console.WriteLine("Method: " + exception.TargetSite);
                Console.WriteLine("Source: " + exception.Source);
            }
            try 
            {
                IElements checkBox = new CheckBox(3, " ");
                IElements button = new Button(1, "accept");
                IElements radioButton = new RadioButton(4, "question");
            }
            catch
            {
                Console.WriteLine("Unexpected error.");
            }
            finally
            {
                Console.WriteLine("Errors processed.");
            }
            UI<Figure> figures = new UI<Figure>();
            figures.Push(new Circle(24));
            figures.Push(new Rectangle(27, 13));
            UI<ManageElements> elements = new UI<ManageElements>();
            elements.Push(new CheckBox(3, "basket"));
            elements.Push(new Button(1, "accept"));
            elements.Push(new RadioButton(4, "question"));
            // figures.Output();
            Console.Write("Choose the action: \n1. Figures\n2. Elements\n");
            int state = Convert.ToInt32(Console.ReadLine());
            UIController controller;
            if (state == (int)State.FIGURES)
            {
                controller = new UIController(figures);
                Console.WriteLine("Total square: {0}", controller.GetTotalSquare());
            }
            else if (state == (int)State.ELEMENTS)
            {
                controller = new UIController(elements);
                Console.WriteLine("Total elements: {0}", controller.GetCountUiElements());
                controller.GetButtonList();
            }
            else
            {
                Console.WriteLine("Error.");
            }
            Debug.Assert(isError); // проверяет, есть ли логическая ошибка при создании программы. Выбрасывает стек вызовов, если false
            Console.ReadKey();
        }
    }
}
