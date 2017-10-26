using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace lab8
{
    interface IVector<T>
    {
        void Push(T number);
        void Pop();
        void Print();
    }
    public class Button
    {
        public int count;
        public string title;
        public Button(string title)
        {
            this.title = title;
        }
        public void Print()
        {
            Console.WriteLine("\nButtons\nCount: {0}\nTitle: {1}\n", count, title);
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    class Vector<T> : IVector<T>
    {
        class Date
        {
            public string createDate;
            public Date()
            { 
                createDate = DateTime.Now.ToShortDateString();
            }
        } 
        private List<T> vector;
        private string strings;
        private int size;
        public string vectorCreatingDate;
        public Vector(string str)
        {
            Date time = new Date();
            vectorCreatingDate = time.createDate;
            strings = str;
            size = strings.Length;
        }
        public Vector(List<T> numbers)
        {
            Date time = new Date();
            vectorCreatingDate = time.createDate;
            vector = numbers;
            size = vector.Count;
        }
        public T this [int number]
        {
            get { return vector[number]; }
            set { vector[number] = value; }
        }
        public void Push(T number)
        {
            vector.Add(number);
        }
        public void Pop()
        {
            vector.RemoveAt(vector.Count - 1);
        }
        public static T Subtract(T a, T b)
        {
            dynamic x = a, y = b;
            return x - y;
        }
        public int vecSize
        {
            get { return this.size; }
        }
        public void Print()
        {
            foreach (T number in vector)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Vector was created: {0}", vectorCreatingDate);   
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return vector.ToString();
        }
        public static bool operator != (Vector<T> one, Vector<T> two)
        {
            int Size = (one.size > two.size) ? one.size : two.size;
            int counter = 0;
            for (int i = 0; i < Size; i++)
            {
                if (one.vector.Contains(two.vector[i]))
                {
                    counter++;
                }
            }
            if (counter == Size)
            {
                return false;
            }
            return true;
        }
        public static bool operator == (Vector<T> one, Vector<T> two)
        {
            int Size = (one.size > two.size) ? one.size : two.size;
            int counter = 0;
            for (int i = 0; i < Size; i++)
            {
                if (one.vector.Contains(two.vector[i]))
                {
                    counter++;
                }
            }
            if (counter == Size)
            {
                return true;
            }
            return false;
        }
        public static bool operator > (Vector<T> one, T num) 
        {
            if (one.vector.Contains(num))
            {
                return true;
            }
            return false; 
        }
        public static bool operator < (Vector<T> one, T num) 
        {
            if (!one.vector.Contains(num))
            {
                return true;
            }
            return false; 
        }
        public static Vector<T> operator + (Vector<T> one, Vector<T> two)
        {
            int Size = (one.size > two.size) ? one.size : two.size;
            for (int i = 0; i < Size; i++)
            {
                one.vector.Add(two.vector[i]);
            }
            return new Vector<T>(one.vector);
        }
        public static Vector<T> operator - (Vector<T> one, Vector<T> two)
        {
            List<T> nums = new List<T>();
            int Size = (one.size > two.size) ? one.size : two.size;
            for (int i = 0; i < Size; i++)
            {
                nums.Add(Subtract(one.vector[i], two.vector[i]));
            }
            return new Vector<T>(nums);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string readPath= @"D:\work\study\OOP\lab8\input.txt";
            string writePath = @"D:\work\study\OOP\lab8\output.txt";
            List<double> nums1 = new List<double> { 23.5, 65.4, 32.1, 9.0012 };
            List<int> nums2 = new List<int> { 27, 64, 128, 256 };
            IVector<double> vector1 = new Vector<double>(nums1);
            IVector<int> vector2 = new Vector<int>(nums2);
            Vector<Button> vector3 = new Vector<Button>("accept");
            Vector<int> vector4 = new Vector<int>(nums2);
            try
            {
                vector1.Print();
                vector2.Print();
                vector3.Print();
            }
           catch (Exception exception)
           {
                Console.WriteLine("Error: " + exception.Message);
                Console.WriteLine("Method: " + exception.TargetSite);
                Console.WriteLine("Source: " + exception.Source);
           }
           finally
           {
               Console.WriteLine("\nAll errors was processed");
           }
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < vector4.vecSize; i ++)
                {
                    sw.Write(vector4[i] + " ");
                }
            }
            List<int> nums3 = new List<int>();
            using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    nums3.Add(Convert.ToInt32(sr.ReadLine()));
                }
            }
            Vector<int> vector5 = new Vector<int>(nums3);
            vector5.Print();
        }
    }
}

