using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    static class Math
    {
        public static (int, int) findMaxMin(Vector vec)
        {
            int max = vec[0];
            int min = vec[0];
            for (int i = 0; i < vec.vecSize; i++)
            {
                if (vec[i] > max)
                {
                    max = vec[i];
                }
                if (vec[i] < min)
                {
                    min = vec[i];
                }
            }
            return (min, max);
        }
        public static int Size(Vector vec)
        {
            return vec.vecSize;
        }
        public static string RemoveVowel(this string str)
        {
            char[] vowels = {'a', 'o', 'u', 'i', 'e', 'y'};
            for (int i = 0; i < str.Length; i++)
            {
                if (vowels.Contains(str[i]))
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }
        public static Vector RemoveFiveElements(this Vector vec)
        {
            for (int i = 0; i < vec.vecSize; i++)
            {
               vec.PopAtZero();
            }
            return vec;
        }
    }
    class Owner
    {
        public static readonly long Id;
        public string Name;
        public string Organization;
        static Owner()
        {
            Id = DateTime.Now.Ticks;
        }
        public Owner()
        {
            Name = "Vladislav";
            Organization = "BelSTU";        
        }
        public void GetData()
        {
            Console.WriteLine("ID: {0}\nName: {1}\nOrganization: {2}\n", Id, Name, Organization);
        }
    }
    class Vector
    {
        class Date
        {
            public string createDate;
            public Date()
            { 
                createDate = DateTime.Now.ToShortDateString();
            }
        } 
        private List<int> vector;
        private string strings;
        private int size;
        public string vectorCreatingDate;
        public Vector(string str)
        {
            Date time = new Date();
            Owner data = new Owner();
            data.GetData();
            vectorCreatingDate = time.createDate;
            strings = str;
            size = strings.Length;
        }
        public Vector(List<int> numbers)
        {
            Date time = new Date();
            Owner data = new Owner();
            data.GetData();
            vectorCreatingDate = time.createDate;
            vector = numbers;
            size = vector.Count;
        }
        public void Push(int number)
        {
            vector.Add(number);
        }
        public void Pop()
        {
            vector.RemoveAt(vector.Count - 1);
        }
        public void PopAtZero()
        {
            vector.RemoveAt(0);
        }
        public int vecSize
        {
            get { return this.size; }
        }
        public void Print()
        {
            foreach(int number in vector)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();
            Console.WriteLine("Vector was created: {0}", vectorCreatingDate);   
        }
        public int this [int number]
        {
            get { return vector[number]; }
            set { vector[number] = value; }
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
        public static bool operator != (Vector one, Vector two)
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
        public static bool operator == (Vector one, Vector two) // бесполезен
        {
            return true; 
        }
        public static bool operator > (Vector one, int num) 
        {
            if (one.vector.Contains(num))
            {
                return true;
            }
            return false; 
        }
        public static bool operator < (Vector one, int num) // бесполезен
        {
            return true; 
        }
        public static Vector operator + (Vector one, Vector two)
        {
            int Size = (one.size > two.size) ? one.size : two.size;
            for (int i = 0; i < Size; i++)
            {
                one.vector.Add(two.vector[i]);
            }
            return new Vector(one.vector);
        }
        public static Vector operator - (Vector one, Vector two)
        {
            List<int> nums = new List<int>();
            int Size = (one.size > two.size) ? one.size : two.size;
            for (int i = 0; i < Size; i++)
            {
                nums.Add(one.vector[i] - two.vector[i]);
            }
            return new Vector(nums);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers_1 = new List<int> {27, 32, 64, 128, 256};
            List<int> numbers_2 = new List<int> {-27, -32, -64, -128, -256};
            List<int> numbers_3 = new List<int> {27, 32, 64, 128, 256};
            string strings_1 = "hello darkness my old friend";
            Vector vector_1 = new Vector(numbers_1);
            Vector vector_2 = new Vector(numbers_2);
            Vector vector_3 = new Vector(numbers_3);
            Vector vector_4 = new Vector(strings_1);
            Console.WriteLine("Vectors are not equal? {0}", vector_1 != vector_2);
            Console.WriteLine("Vectors are not equal? {0}", vector_1 != vector_3);
            vector_1.Print();
            Console.WriteLine();
            vector_2.Print();
            Console.WriteLine("\nSubtraction\n");
            (vector_1 - vector_2).Print();  
            Console.WriteLine("\nAddition\n"); // Не сложение, но добавление.
            (vector_1 + vector_2).Print();
            vector_2.Push(13);
            Console.WriteLine("Vector contains {0}? {1}\n", 13, vector_2 > 13);
            int min = Math.findMaxMin(vector_1).Item1;
            int max = Math.findMaxMin(vector_1).Item2;
            Console.WriteLine("Maximum element: {0}\nMinumum element: {1}", max, min);
            Console.WriteLine("Vector's size: {0}\n", Math.Size(vector_1 + vector_2));
            Console.WriteLine("Primary string: {0}\nResult string: {1}\n", strings_1, strings_1.RemoveVowel());
            vector_1.Print();
            vector_1.RemoveFiveElements().Print();
        
        }
    }
}
