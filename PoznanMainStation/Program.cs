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
            Station Poznan = new Station(1, "Poznań Główny");
            runnables.Add(Poznan);
            Train pociag1 = new Train(0, Poznan, new TimeSpan(0, 2, 0), new TimeSpan(0, 5, 0), 200, 300, 1);
            Train pociag2 = new Train(1, Poznan, new TimeSpan(0, 8, 0), new TimeSpan(0, 12, 0), 200, 300, 1);
            Train pociag3 = new Train(2, Poznan, new TimeSpan(0, 13, 0), new TimeSpan(0, 19, 0), 200, 300, 1);
            runnables.Add(pociag1);
            runnables.Add(pociag2);
            runnables.Add(pociag3);
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