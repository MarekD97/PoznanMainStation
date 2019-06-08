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
        

        static void GenerateTrains(Station station)
        {
            Train pociag1 = new Train(18170, station, new TimeSpan(0, 3, 0), new TimeSpan(0, 7, 0), 200, 300, 1);
            Train pociag2 = new Train(73102, station, new TimeSpan(0, 6, 0), new TimeSpan(0, 11, 0), 200, 300, 1);
            Train pociag3 = new Train(71110, station, new TimeSpan(0, 4, 0), new TimeSpan(0, 12, 0), 200, 300, 1);
            runnables.Add(pociag1);
            runnables.Add(pociag2);
            runnables.Add(pociag3);
        }

        static void GenerateRunnables()
        {
            Station Poznan = new Station(5, "Poznań Główny");
            runnables.Add(Poznan);
            GenerateTrains(Poznan);            
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