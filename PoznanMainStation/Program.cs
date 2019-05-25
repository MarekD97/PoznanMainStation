using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PoznanMainStation
{
    class Program
    {
        public static List<IRunnable> runnables = new List<IRunnable>();
        public static List<Thread> threads = new List<Thread>();
        static int N = 5; 

        static void GenerateRunnables()
        {
            for (int i = 0; i<N; i++)
            {

            }
            
        }
        
        static void RunThreads()
        {
            foreach (IRunnable a in runnables)
            {
                threads.Add(new Thread(new ThreadStart(a.Run)));
            }
            foreach (Thread t in threads)
            {
               t.Start();
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            GenerateRunnables();
            RunThreads();
            Console.WriteLine("test");
            App.Main();
        }
    }
}
