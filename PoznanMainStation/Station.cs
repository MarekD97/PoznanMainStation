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
        private int numberOfPlatforms;  //Liczba wygenerowanych peronów na stacji
        public TimeSpan stationTime = new TimeSpan(0, 0, 0); //Czas stacji
        static List<Platform> stationPlatforms = new List<Platform>();

        public List<Train> trainsToEnter = new List<Train>(); //pociągi, które czekają na wjazd
        static List<Train> trainsAtPlatforms = new List<Train>(); //pociągi na peronach

        public override void Update()
        {
            //Zadania stacji

            //Aktualizacja czasu
            Console.WriteLine("Czas stacji: {0}", stationTime);
            stationTime = stationTime.Add(new TimeSpan(0, 1, 0));

            //Sprawdzenie czy jakieś pociągi oczekują na wjazd
            if (trainsToEnter != null)
            {
                foreach (Train tr in trainsToEnter)
                {
                    //Jeśli preferowany peron przez pociąg jest dostępny (odwołanie IsFree() klasy Platform):
                    //Semafor - jeśli przejazd nie jest bolokowany przez inny pociąg - wjazd
                    // jeśli jest zablokowany - oczekiwanie     
                }
            }

            if (trainsAtPlatforms != null)
            {
                foreach (Train tr in trainsAtPlatforms)
                {
                    //Sprawdzenie, czy jakieś pociągi nie mają statusu wyjazdu
                    //Semafor - jeśli przejazd nie jest bolokowany przez inny pociąg - wyjazd
                    // jeśli jest zablokowany - oczekiwanie
                }
            }
        }

        public Station(int numberOfPlatforms)
        {
            this.numberOfPlatforms = numberOfPlatforms;
            for (int i = 0; i < this.numberOfPlatforms; i++)
            {
                stationPlatforms.Add(new Platform(i));
            }
        }

        //metoda używana przez pociągi w celu poinformowania stacji o swoim przyjeździe 
        public void TrainArrived(Train train)
        {
            trainsToEnter.Add(train);
        }
    }
}
