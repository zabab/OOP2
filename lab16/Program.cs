using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab16
{
    public class Vector
    {
        Random random = new Random();
        public List<int> vector = new List<int>();
        public Vector(int n)
        {
            for (int i = 0; i < n; i++)
            {
               vector.Add(random.Next(n));
            }
        }
        public int Length
        {
            get { return vector.Count; }
        }
        public int this [int number]
        {
            get { return vector[number]; }
            set { vector[number] = value; }
        }
        public void Print()
        {
            foreach (var number in vector)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }
        public int Sum()
        {
            int sum = 0;
            for (int i = 0; i < vector.Count; i++)
            {
                sum += vector[i];
            }
            return sum;
        }
        public void Sort()
        {
            vector.Sort();
        }
        public static Vector operator * (Vector vec, int n)
        {
            for (int i = 0; i < vec.vector.Count; i++)
            {
                vec.vector[i] *= n;
            }
            return vec;
        }
    }
    public static class Tasker
    {
        public static void GetTask(int size, int num)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Vector vector = new Vector(size);
            // vector.Print();
            Task task = Task.Factory.StartNew(() => vector = vector * num);
            Console.WriteLine("Status: {0}", task.Status);
            task.Wait();
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
            // vector.Print();
        }
        public static async Task FourSum(Vector one, Vector two, Vector three)
        {
            Task<int> task1 = new Task<int>(() => one.Sum());
            task1.Start();
            Task<int> task2 = new Task<int>(() => two.Sum());
            task2.Start();
            Task<int> task3 = new Task<int>(() => three.Sum());
            task3.Start();
            Task<Vector> task4 = new Task<Vector>(() => new Vector(task1.Result + task2.Result + task3.Result));
            Task task5 = task4.ContinueWith(Display);
            task4.Start();
            task5.Wait();
            Console.WriteLine(task4.Result.Sum());
        }
        public static void Display(Task task)
        {
            Console.WriteLine("ID current: {0}", Task.CurrentId);
            Console.WriteLine("ID before: {0}", task.Id);
            Thread.Sleep(3000);
        }
    }
    public static class Paralleler
    {
        public static void For()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Generate(10000);
            watch.Stop();
            Console.WriteLine("Common loop: {0}", watch.Elapsed);
            watch.Start();
            Parallel.For(1, 10000, Generate);
            watch.Stop();
            Console.WriteLine("Parallel loop: {0}", watch.Elapsed);
        }
        public static void ForEach()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (int vec in new List<int>() {10000, 10000})
            {
                Generate(vec);
            }
            watch.Stop();
            Console.WriteLine("Common loop: {0}", watch.Elapsed);
            watch.Start();
            ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 10000, 10000 }, Generate);
            watch.Stop();
            Console.WriteLine("Parallel loop: {0}", watch.Elapsed);
        }
        public static void Generate(int n)
        {
            Vector vector = new Vector(n);
        } 
        public static void DoubleTask(int n)
        {
            Parallel.Invoke(Display, () => 
                                    {
                                        Console.WriteLine("Current task: {0}", Task.CurrentId);
                                        Thread.Sleep(3000);
                                        Generate(n);
                                    },
                                    () => Generate(n));
        }
        private static void Display()
        {
            Console.WriteLine("Current task: {0}", Task.CurrentId);
            Thread.Sleep(3000);
        }
    }
    public static class Store
    {
        public static BlockingCollection<int> store;

        public static void Producer()
        {
            for (int i = 1; i <= 10; i++)
            {
                store.Add(i);
                Console.WriteLine("Producer's work: " + i);
            }
            store.CompleteAdding();
        }

        public static void Consumer()
        {
            int i;
            while (!store.IsCompleted)
            {
                if (store.TryTake(out i))
                {
                    Console.WriteLine("Consumer's work: " + i);
                }
            }
        }
        public static void Work()
        {
            store = new BlockingCollection<int>(5);
            Task Pr = new Task(Producer);
            Task Cn = new Task(Consumer);
            Pr.Start();
            Cn.Start();
            try
            {
                Task.WaitAll(Cn, Pr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Cn.Dispose();
                Pr.Dispose();
                store.Dispose();
            }
        }

    }
    class Program
    {
        static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        static CancellationToken token = cancelTokenSource.Token;
        static void Main(string[] args)
        {
            Tasker.GetTask(1000000, 2);
            Tasker.GetTask(1000000, 4);
            Tasker.GetTask(1000000, 36);
            Task task = new Task(() => 
            {
                Tasker.GetTask(100000, 64);
                if (token.IsCancellationRequested)
                {
                    return;
                }
            });
            Tasker.FourSum(new Vector(100), new Vector(20), new Vector(30)).GetAwaiter().GetResult();
            Paralleler.For();
            Paralleler.ForEach();
            Paralleler.DoubleTask(100000);
            Store.Work();
            Task t = DisplayResultAsync();
            t.Wait();
        }
        static async Task DisplayResultAsync()
        {
            int result = await FactorialAsync(10);
            Thread.Sleep(3000);
            Console.WriteLine("Factorial of number {0} equal {1}", 10, result);
        }
        static Task<int> FactorialAsync(int x)
        {
            int result = 1;
            return Task.Run(() =>
            {
                for (int i = 1; i <= x; i++)
                {
                    result *= i;
                }
                return result;
            });
        }
    }
}
