using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public abstract class Figure
    {
        public abstract double Square();
        public abstract double Perimeter();
    }
    interface IFigure
    {
        void Show();
        void Input();
        void Resize();
    }
    interface IElements
    {
        void Show();
    }
    public class Circle : Figure, IFigure
    {
        public double radius;
        public const double pi = 3.14d;
        public Circle()
        {
            Input();
        }
        public Circle(double radius)
        {
            this.radius = radius;
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
        }
    }
    public partial class Rectangle : Figure, IFigure 
    {
        public Rectangle()
        {
            Input();
        }
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
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
        }
    }
    public class ManageElements
    {
        public int count;
        public string title;
    }
    public class CheckBox : ManageElements, IElements
    {
        public CheckBox(int count, string title)
        {
            this.count = count;
            this.title = title;
        }
        public void Show()
        {
            Console.WriteLine("\nCheckboxes\nCount: {0}\nTitle: {1}\n", count, title);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
    }
    public class RadioButton : ManageElements, IElements
    {
        public RadioButton(int count, string title)
        {
            this.count = count;
            this.title = title;
        }
        public void Show()
        {
            Console.WriteLine("\nRadioButtons\nCount: {0}\nTitle: {1}\n", count, title);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
    }
    public class Button : ManageElements, IElements
    {
        public Button(int count, string title)
        {
            this.count = count;
            this.title = title;
        }
        public void Show()
        {
            Console.WriteLine("\nButtons\nCount: {0}\nTitle: {1}\n", count, title);
        }
        public override string ToString()
        {
            Show();
            return base.ToString();
        }
    }
    sealed public class Printer
    {
        public Printer() { }
        public string Print(IFigure obj)
        {
            return obj.ToString();
        }
        public string Print(IElements obj)
        {
            return obj.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IFigure circle = new Circle(24);
            IFigure rectangle = new Rectangle(27, 13);
            IElements checkBox = new CheckBox(3, "basket");
            IElements button = new Button(1, "accept");
            IElements radioButton = new RadioButton(4, "question");
            Printer printer = new Printer();
            Console.WriteLine("IS-operator: {0}, {1}", circle is Circle, circle is Rectangle);
            // обычное приведение, но с использование оператора as(выбрасывает не исключение, а null)
            IFigure circle1 = circle as Rectangle; 
            printer.Print(circle);
            printer.Print(rectangle);
            printer.Print(checkBox);
            printer.Print(button);
            printer.Print(radioButton);
            Console.ReadKey();
        }
    }
}
