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
        TimeSpan arrivalTime;           //Czas przyjazdu pociągu (powiedzmy na razie że czas liczymy od startu symulacji w minutach)
        TimeSpan departureTime;         //Czas odjazdu pociągu
        int numberOfPassengers;         //Ilość pasażerów w pociągu
        int loadTime;                   //czas potrzebny na rozładunek i załadunek
        int capacity;                   //Ładowność/Maksymalna liczba pasażerów w pociągu
        Platform preferredPlatform;     //Preferowany peron
        Platform actualPlatform;        //Aktualny peron
        public bool allowedToEnter;            //Zezwolenie na wjazd
        bool readyToLeave;              //Gotowy do odjazdu
        public bool allowedToLeave;            //Zezwolenie na wyjazd
        Station station;

        public override void Update()
        {
            //Wysłanie żądania do stacji o chęci wjazdu na peron
            if (arrivalTime == station.stationTime)
            {
                station.TrainArrived(this);
                //Console.WriteLine("\tP{0} : --> przyjazd", this.id);
                Screen.AddTrainArrival(id, preferredPlatform.id);
            }
            // Gdy odpowiedź jest pozytywna:
            if (allowedToEnter)
            {
                //na razie pociąg wjeżdża tam gdzie chce, finalnie stacja będzie mu przydzielać peron
                EntryToThePlatform();
                station.TrainAtPlatform(this);
                //Console.WriteLine("\tP{0} : ==> wjazd na peron {1}", this.id, this.actualPlatform.id);
                //Wjazd, wyładunek, załadunek
                Loading();
                //Wysłanie żądania do stacji o chęci wyjazdu ze stacji
                allowedToEnter = false; //nie jestem pewien czy pociąg powinien sam to ustawiać
                readyToLeave = true;
            }
            //Gdy odpowiedź jest pozytywna:
            else if (allowedToLeave)
            {
                //wyjazd
                //pociąg powinien jeszcze czekać na swoją godzinę odjazdu
                if(station.stationTime >= departureTime)
                {
                    Screen.AddTrainDeparture(id, actualPlatform.id);
                    Leave();
                    readyToLeave = false;
                    allowedToLeave = false; 
                }
                //Console.WriteLine("\tP{0} : <== odjazd z peronu {1}", this.id, this.actualPlatform.id);
                
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
            this.allowedToEnter = false;
            this.allowedToLeave = false;
            this.readyToLeave = false;
            //Console.WriteLine("\tP{0} : pociąg stworzony", this.id);
        }

        
        void EntryToThePlatform()   //Wjazd na peron
        {
            station.m_entry.WaitOne();
            Thread.Sleep(3000);
            station.m_entry.ReleaseMutex();
        }

        void Loading()     //wyładunek i załadunek
        {
            //przykładowo wysiada 1/4 pasażerów, 
            //zależność czasu wysiadania od l. pasażerów jest liniowa
            loadTime = numberOfPassengers * 30 / 4;
            //Console.WriteLine("\tP{0} : Pasażerowie wysiadają", this.id);
            Screen.AddTrainAtStation(this.id, this.actualPlatform.id);
            Thread.Sleep(loadTime);
            //wsiadanie podobnie, tylko liczbę pasażerów bierzemy z peronu
            loadTime = this.actualPlatform.GetPassengers() * 30 / 4;
            //Console.WriteLine("\tP{0} : Pasażerowie wsiadają", this.id);
            Thread.Sleep(loadTime);
        }
        
        void Leave()
        {
            station.m_exit.WaitOne();
            Thread.Sleep(3000);
            station.m_exit.ReleaseMutex();
            this.actualPlatform.SetAvailability(true);
            //Console.WriteLine("\tP{0} : <-- odjechał", this.id);
            Screen.RemoveTrain(this.id, this.actualPlatform.id);
        }

        public bool IsAllowedToEnter()
        {
            this.allowedToEnter = true;
            this.actualPlatform.SetAvailability(false);
            return true;
        }

        public bool IsAllowedToLeave()
        {
            this.allowedToLeave = true;
            return true;
        }

        public Platform GetPreferredPlatform()
        {
            return this.preferredPlatform;
        }

        public void SetActualPlatform(Platform actual)
        {
            this.actualPlatform = actual;
        }

        public bool GetReadyToLeave()
        {
            return readyToLeave;
        }
    }
}
