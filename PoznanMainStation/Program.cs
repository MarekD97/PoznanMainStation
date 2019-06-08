using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PoznanMainStation
{
    class Program
    {
        static List<IRunnable> runnables = new List<IRunnable>();
        static List<Thread> threads = new List<Thread>();
        static int NumberOfTrains = 10;
        static readonly Random random = new Random();
        

        public static int RandomNumber(int min, int max)
        {
            lock(random)
            {
                return random.Next(min, max);
            }
        }

        static void GenerateTrains(Station station)
        {
            int id;
            int passengers;
            int capacity;
            int timeAtPlatform = 15;
            int arrival = 1;
            int departure = arrival + timeAtPlatform;
            int prefPlatform;
            
            Train tr;
            for (int i=0; i < NumberOfTrains; i++)
            {
                id = RandomNumber(10000, 100000);
                capacity = RandomNumber(100, 400);
                prefPlatform = RandomNumber(1,station.stationPlatforms.Count()+1);
                do
                {
                    passengers = RandomNumber(50, 300);
                }
                while (passengers > capacity);
                tr = new Train(id, station, new TimeSpan(0, arrival, 0), new TimeSpan(0, departure, 0), passengers, capacity, prefPlatform);
                runnables.Add(tr);
                arrival += RandomNumber(1, 5);
                departure = arrival + timeAtPlatform;
            }
        }

        static void GenerateRunnables()
        {
            Station Poznan = new Station(6, "Poznań Główny");
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