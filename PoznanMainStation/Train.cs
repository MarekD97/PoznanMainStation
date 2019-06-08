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
        int capacity;                   //Ładowność/Maksymalna liczba pasażerów w pociągu
        Platform preferredPlatform;     //Preferowany peron
        Platform actualPlatform;        //Aktualny peron
        public bool allowedToEnter;            //Zezwolenie na wjazd
        bool readyToLeave;              //Gotowy do odjazdu
        public bool allowedToLeave;            //Zezwolenie na wyjazd
        bool arrived;
        Station station;

        public override void Update()
        {
            if (!arrived)
            {
                if (arrivalTime == station.stationTime)
                {
                    lock (SyncObject)
                    {
                        //Wysłanie żądania do stacji o chęci wjazdu na peron
                        station.TrainArrived(this);
                        arrived = true;
                    }
                    Screen.AddTrainArrival(id, preferredPlatform.id);
                }
            }
            // Gdy odpowiedź jest pozytywna:
            if (allowedToEnter)
            {
                EntryToThePlatform();
                station.TrainAtPlatform(this);
                //Wjazd, wyładunek, załadunek
                Loading();
                allowedToEnter = false;
                readyToLeave = true;
            }
            //Gdy odpowiedź jest pozytywna:
            if (allowedToLeave)
            {
                if (station.stationTime >= departureTime)
                {
                    //wyjazd
                    Screen.AddTrainDeparture(id, actualPlatform.id);
                    Leave();
                    readyToLeave = false;
                    allowedToLeave = false;
                }                
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
            this.arrived = false;
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
            //wsiadanie
            int gettingOff = numberOfPassengers * 3 / 10;
            numberOfPassengers -= gettingOff;
            int loadTime = gettingOff * 100 / 4;
            Screen.AddTrainAtStation(this.id, this.actualPlatform.id);
            Thread.Sleep(loadTime);

            //wsiadanie podobnie, tylko liczbę pasażerów bierzemy z peronu
            int gettingOn = actualPlatform.numberOfPassengers * 7 / 10;
            actualPlatform.numberOfPassengers -= gettingOn;
            loadTime = gettingOn * 100 / 4;
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
