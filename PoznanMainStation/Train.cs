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
                EntryToThePlatform();
                this.station.TrainAtPlatform(this);
                Console.WriteLine("P{0} wjechał na peron {1}", this.id, this.actualPlatform.id);
                //Wjazd, wyładunek, załadunek
                Loading();
                //Wysłanie żądania do stacji o chęci wyjazdu ze stacji
                allowedToEnter = false;
                readyToLeave = true;
            }
            //Gdy odpowiedź jest pozytywna:
            if (readyToLeave)
            {
                //wyjazd
                Leave();
                readyToLeave = false;
            }
        }

        public Train(int id, Station station, TimeSpan arrival, TimeSpan departure, int passengers, int cap, int preferredPlatformID)
        {
            this.id = id;
            this.station = station;
            this.arrivalTime = arrival;
            this.departureTime = departure;
            this.numberOfPassengers = passengers;
            this.capacity = cap;
            this.readyToLeave = false;
            this.preferredPlatform = this.station.stationPlatforms[preferredPlatformID-1]; //do pociągu przypisany jest preferowany peron, w konstruktorze podaje się jego ID
            Console.WriteLine("P{0} stworzony", this.id);
        }

        
        void EntryToThePlatform()   //Wjazd na peron
        {
            Thread.Sleep(3000);

        }

        void Loading()     //wyładunek i załadunek
        {
            //przykładowo wysiada 1/4 pasażerów, 
            //zależność czasu wysiadania od l. pasażerów jest liniowa
            loadTime = numberOfPassengers * 30 / 4;
            Console.WriteLine("P{0} - Pasażerowie wysiadają", this.id);
            Thread.Sleep(loadTime);
            //wsiadanie podobnie, tylko liczbę pasażerów bierzemy z peronu
            loadTime = this.actualPlatform.GetPassengers() * 30 / 4;
            Console.WriteLine("P{0} - Pasażerowie wsiadają", this.id);
            Thread.Sleep(loadTime);
        }
        
        void Leave()
        {
            Thread.Sleep(3000);
        }

        public void IsAllowedToEnter()
        {
            this.allowedToEnter = true;
            this.actualPlatform.SetAvailability(false);
        }

        public Platform GetPreferredPlatform()
        {
            return this.preferredPlatform;
        }

        public void SetActualPlatform(Platform actual)
        {
            this.actualPlatform = actual;
        }
    }
}
