using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace PoznanMainStation
{
    class Train : Railway
    {
        int id;
        TimeSpan arrivalTime;            //Czas przyjazdu pociągu (powiedzmy na razie że czas liczymy od startu symulacji w minutach)
        TimeSpan departureTime;          //Czas odjazdu pociągu
        int numberOfPassengers;     //Ilość pasażerów w pociągu
        int loadTime;                   //czas potrzebny na rozładunek i załadunek
        int capacity;               //Ładowność/Maksymalna liczba pasażerów w pociągu
        Platform preferredPlatform;
        Platform actualPlatform;
        bool allowedToEnter;
        bool readyToLeave;
        Station station;

        public override void Update()
        {
            //Zadania pociągu

            //Wysłanie żądania do stacji o chęci wjazdu na peron
            if (this.arrivalTime == this.station.stationTime)
            {
                this.station.TrainArrived(this);
                Console.WriteLine("Przyjechał pociąg {0}", this.id);
            }
            // Gdy odpowiedź jest pozytywna:
            if (allowedToEnter)
            {
                //na razie pociąg wjeżdża tam gdzie chce, finalnie stacja będzie mu przydzielać peron
                EntryToThePlatform(preferredPlatform);
                //Wjazd, wyładunek, załadunek
                Loading();
                //Wysłanie żądania do stacji o chęci wyjazdu ze stacji
                readyToLeave = true;
            }
            //Gdy odpowiedź jest pozytywna:
            if (readyToLeave)
            {
                //wyjazd
                Leave();
            }
                


        }

        public Train(int id, Station station, TimeSpan arrival, TimeSpan departure, int passengers, int cap)
        {
            this.id = id;
            this.station = station;
            this.arrivalTime = arrival;
            this.departureTime = departure;
            this.numberOfPassengers = passengers;
            this.capacity = cap;
            this.readyToLeave = false;
            //this.preferredPlatform = preferredPlatform;
            Console.WriteLine("Pociąg {0} stworzony", this.id);
        }

        
        void EntryToThePlatform(Platform platform)   //Wjazd na peron
        {
            Thread.Sleep(3000);
            this.actualPlatform = platform;
        }

        void Loading()     //wyładunek i załadunek
        {
            //przykładowo wysiada 1/4 pasażerów, 
            //zależność czasu wysiadania od l. pasażerów jest liniowa
            loadTime = numberOfPassengers * 30 / 4;
            Console.WriteLine("Pasażerowie wysiadają");
            Thread.Sleep(loadTime);
            Console.WriteLine("Pasażerowie wsiadają");
            //loadTime = 
        }
        
        void Leave()
        {
            Thread.Sleep(3000);
        }

    }
}
