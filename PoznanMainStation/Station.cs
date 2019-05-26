using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;

namespace PoznanMainStation
{
    class Station : Railway
    {
        int noOfPlatforms;
        public static Timer timer;
        public static TimeSpan stationTime = new TimeSpan(0, 0, 0);
        static TimeSpan oneMinute = new TimeSpan(0, 1, 0);
        static List<Platform> stationPlatforms = new List<Platform>();

        public static List<Train> trainsToEnter; //pociągi, które czekają na wjazd
        public static List<Train> trainsToLeave; //pociągi, które czekają na wyjazd
        static List<Train> trainsAtPlatforms; //pociągi na peronach

        public override void Update()
        {
            Console.WriteLine("Czas stacji: {0}", stationTime);
        }

        public Station(int platforms)
        {
            this.noOfPlatforms = platforms;
            SetTimer();
            for (int i = 0; i<noOfPlatforms; i++)
            {
                stationPlatforms.Add(new Platform(i));
            }
        }

        private static void SetTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //stacja ma co sekundę sprawdzać czy któryś z pociągów chce wjechać albo wyjechać  
            stationTime = stationTime.Add(oneMinute);         
        }
    }
}
