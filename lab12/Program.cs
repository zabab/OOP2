using System;
using System.IO;
using System.Reflection;
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
    public class Rectangle : Figure, IFigure 
    {
        public double width;
        public double height;
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
}

namespace lab12
{
    public class Reflector
    {
        public Type type; 
        public Reflector(string type)
        {
            this.type = Type.GetType(type, false, true);
        }
        public void AboutClass()
        {
            using (FileStream fstream = new FileStream("class.txt", FileMode.OpenOrCreate))
            {
                foreach (MemberInfo info in type.GetMembers())
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(info.DeclaringType + " - " + info.MemberType + " - " + info.Name + "\n");
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
        public void PublicMethods()
        {
            using (FileStream fstream = new FileStream("methods.txt", FileMode.OpenOrCreate))
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    if (method.IsPublic)
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(method.Name + "\n");
                        fstream.Write(array, 0, array.Length);
                    }
                }
            }
        }
        public void SpecifiedMethods(string arg)
        {
            using (FileStream fstream = new FileStream("specified_methods.txt", FileMode.OpenOrCreate))
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (arg.Contains(parameters[i].ParameterType.Name))
                        {
                            byte[] array1 = System.Text.Encoding.Default.GetBytes(method.ReturnType.Name + " - " + method.Name + " (");
                            fstream.Write(array1, 0, array1.Length);
                            byte[] array2 = System.Text.Encoding.Default.GetBytes(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                            fstream.Write(array2, 0, array2.Length);
                            byte[] array3 = System.Text.Encoding.Default.GetBytes(", ");
                            if (i + 1 < parameters.Length) 
                            {
                                fstream.Write(array3, 0, array3.Length);
                            }
                            fstream.Write(System.Text.Encoding.Default.GetBytes(")\n"), 0, System.Text.Encoding.Default.GetBytes(")\n").Length);
                        }
                    }
                }
            }
        }
        public void Fields()
        {
            using (FileStream fstream = new FileStream("fields.txt", FileMode.OpenOrCreate))
            {
                foreach (FieldInfo field in type.GetFields())
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(field.FieldType + " - " + field.Name + "\n");
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
        public void Properties()
        {
            using (FileStream fstream = new FileStream("properties.txt", FileMode.OpenOrCreate))
            {
                foreach (PropertyInfo prorertie in type.GetProperties())
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(prorertie.PropertyType + " - " +  prorertie.Name + "\n");
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
        public void Interfaces()
        {
            using (FileStream fstream = new FileStream("interfaces.txt", FileMode.OpenOrCreate))
            {
                foreach (Type interfaces in type.GetInterfaces())
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(interfaces.Name + "\n");
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
        public void RunTimeMethod(string type, string method)
        {
            Assembly asm = Assembly.LoadFrom(@"C:\PWL\study\OOP\lab11\bin\Debug\netcoreapp2.0\lab11.dll");
            Type newType = asm.GetType(type);
            List<int> nums = new List<int> {1488, 0, 27, 54, 42};
            object programm = Activator.CreateInstance(newType, new object[] {nums});
            MethodInfo newMethod = newType.GetMethod(method);
            object result = newMethod.Invoke(programm, new object[] {});
            Console.WriteLine("Method of another instanse: {0}\nMethod's value: {1}", method, result);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Reflector reflector = new Reflector("lab5.Circle");
            reflector.AboutClass();
            reflector.PublicMethods();
            reflector.Fields();
            reflector.Properties();
            reflector.Interfaces();
            reflector.SpecifiedMethods("Object");
            reflector.RunTimeMethod("lab11.Stack", "GetMax");
        }
    }
}
