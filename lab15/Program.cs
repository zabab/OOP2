using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

namespace lab15
{
    public static class Processor
    {
        public static void GetAllProcessInfo()
        {
            StreamWriter fwriter = new StreamWriter("processes.txt", false, System.Text.Encoding.Default);
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    fwriter.WriteLine("ID: {0}\nName: {1}\nPriority: {2}\nStart time: {3}\nProcessor time: {4}",
                                    process.Id, process.ProcessName, process.BasePriority,
                                    Process.Start(Process.GetCurrentProcess().MainModule.FileName).StartTime,
                                    Process.Start(Process.GetCurrentProcess().MainModule.FileName).UserProcessorTime);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Message: {0}", exception.Message);
            }
            fwriter.Close();
        }
        public static void DomainResearch()
        {
            StreamWriter fwriter = new StreamWriter("domains.txt", false, System.Text.Encoding.Default);
            AppDomain domain = AppDomain.CurrentDomain;
            fwriter.WriteLine("Name: {0}", domain.FriendlyName);
            fwriter.WriteLine("Base Directory: {0}", domain.BaseDirectory);
            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                fwriter.WriteLine(asm.GetName().Name);
            }
            fwriter.Close();
        }
        public static void SecondaryDomain()
        {
            AppDomain secondaryDomain = AppDomain.CreateDomain("Secondary domain");
            secondaryDomain.AssemblyLoad += Domain_AssemblyLoad;
            secondaryDomain.DomainUnload += SecondaryDomain_DomainUnload;
            Console.WriteLine("Domain: {0}", secondaryDomain.FriendlyName);
            secondaryDomain.Load(new AssemblyName("lab14"));
            Assembly[] assemblies = secondaryDomain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                Console.WriteLine(asm.GetName().Name);
            }
            AppDomain.Unload(secondaryDomain);
        }
        public static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Domain uload");
        }
        public static void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Domain load");
        }
    }
    public static class Threader
    {
        public static void Consuption()
        {
            int n = 7;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
        public static void TreadInfo()
        {
            Thread thread = new Thread(new ThreadStart(Consuption));
            thread.Start();
            Thread.Sleep(3000);
            thread.Suspend();
            Console.WriteLine("Thread name: {0}", thread.Name);
            Console.WriteLine("Is run: {0}", thread.IsAlive);
            Console.WriteLine("Priority: {0}", thread.Priority);
            Console.WriteLine("State: {0}", thread.ThreadState);
            thread.Resume();
        }
        public static void Even(object x)
        {
            int n = (int)x;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine("Even: " + i);
                    Thread.Sleep(700);
                }   
            }
        }
        public static void Odd(object x)
        {
            int n = (int)x;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine("Odd: " + i);
                    Thread.Sleep(1300);
                }
            }
        }
        public static void Priority()
        {
            Thread first = new Thread(new ParameterizedThreadStart(Even));
            Thread second = new Thread(new ParameterizedThreadStart(Odd));
            second.Priority = ThreadPriority.Highest;
            first.Start(7);
            second.Start(7);
        }
        public static void EvenOdd()
        {
            Thread first = new Thread(new ParameterizedThreadStart(Even));
            Thread second = new Thread(new ParameterizedThreadStart(Odd));
            first.Start(7);
            Thread.Sleep(4000);
            if (first.ThreadState == System.Threading.ThreadState.Stopped)
            {
                second.Start(7);
            }
        }
    }
    public static class Clock
    {
       public static void Print(object state)
       {
           Console.Clear();
           Console.WriteLine("Current time:  {0}", DateTime.Now.ToLongTimeString());
       }
       public static void Time()
       {
            TimerCallback timeClock = new TimerCallback(Clock.Print);
            Timer time = new Timer(timeClock, null, 0, 1000);
       }
    }
    class Program
    {
        static SemaphoreSlim ThreadLock = new SemaphoreSlim(1,1);
        static Thread Odd;
        static Thread Even;
        static List<int> Numbers = new List<int>();
        static void Main(string[] args)
        {
            // Processor.GetAllProcessInfo();
            // Processor.DomainResearch();
            Processor.SecondaryDomain();
            // Threader.TreadInfo();
            // Threader.Priority();
            // Threader.EvenOdd();
            // for (int i = 0; i < 10; i++)
            // {
            //     Numbers.Add(i);
            // }
            // Odd = new Thread(PrintOdd);
            // Even = new Thread(PrintEven);
            // Odd.Name = "Odd";
            // Even.Name = "Even";
            // Odd.Start();
            // Even.Start();
            // Odd.Join();
            // Even.Join();
            // Clock.Time();
            // Console.ReadLine();
        }
        public static void PrintOdd()
        {
            while (Numbers.Count > 0)
            {
                ThreadLock.Wait();
                int x = Numbers[0];
                if (x % 2 != 0)
                {
                    Thread.Sleep(1200);
                    Console.WriteLine(Thread.CurrentThread.Name + ": " + x);
                    Numbers.RemoveAt(0);
                }
                ThreadLock.Release();
            }
        }

        public static void PrintEven()
        {
            while (Numbers.Count > 1)
            {
                ThreadLock.Wait();
                int x = Numbers[0];
                if (x % 2 == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(Thread.CurrentThread.Name + ": " + x);
                    Numbers.RemoveAt(0);
                }
                ThreadLock.Release();
            }
        }
    }
}
