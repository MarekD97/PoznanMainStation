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
            Station Poznan = new Station(3);
            Train pociag1 = new Train(0, new TimeSpan(8, 0, 0), new TimeSpan(8, 30, 0), 200, 300);
            runnables.Add(Poznan);
            runnables.Add(pociag1);
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

        static void Main(string[] args)
        {
            GenerateRunnables();
            RunThreads();
        }

    }
}